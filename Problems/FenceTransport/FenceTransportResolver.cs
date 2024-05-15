using Drawer.GraphDrawer;
using ElectronNET.API.Entities;
using Problem.FenceTransport;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.FenceTransport;

public class FenceTransportResolver : ProblemResolver<FenceTransportInputData, FenceTransportOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    public override FenceTransportOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        AddHullEdges(data.Vertices[data.FactoryIndex], data.Vertices, data.Edges, data.ConvexHullOutput!.HullIndexes!);
        
        FenceTransportOutput output = new();
        problemRecreationCommands = commands;

        ProblemVertex FactoryVertex = data.Vertices[data.FactoryIndex];
        List<Carrier> carriers = CreateCarriers(data.CarrierAssignmentOutput!.Pairs.Count, FactoryVertex.Id);
        
        List<int> FinishedEdges = new();
        CarryFence(FactoryVertex, data.Vertices, data.Edges, data.ConvexHullOutput!.HullIndexes!, FinishedEdges, carriers);


        return output;
    }
    private void AddHullEdges(ProblemVertex FactoryVertex, List<ProblemVertex> vertices, 
        List<ProblemEdge> edges, List<int> HullIndexes)
    {
        for (int i = 0; i < HullIndexes.Count; i++)
        {
            int from = HullIndexes[i];
            int to = HullIndexes[(i + 1) % HullIndexes.Count];
            edges.Add(new(edges.Count, from, to, new(0, CalculateLengthBetweenHullVerticies(vertices[from], vertices[to]))));
        }
    }
    private int CalculateLengthBetweenHullVerticies(ProblemVertex vertex1, ProblemVertex vertex2)
    {
        int x1 = vertex1.X.GetValueOrDefault();
        int y1 = vertex1.Y.GetValueOrDefault();
        int x2 = vertex2.X.GetValueOrDefault();
        int y2 = vertex2.Y.GetValueOrDefault();
        return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
    private List<Carrier> CreateCarriers(int count, int FactoryIndex)
    {
        List<Carrier> carriers = new();
        for (int i = 0; i < count; i++)
        {
            carriers.Add(new(i, FactoryIndex));
        }
        return carriers;
    }
    private void CarryFence(ProblemVertex FactoryVertex, List<ProblemVertex> vertices, List<ProblemEdge> edges, 
        List<int> HullIndexes, List<int> FinishedEdges, List<Carrier> carriers)
    {
        ProblemVertex furthestVertex = FindFurthestFenceVertex(FactoryVertex, vertices, edges, HullIndexes, FinishedEdges);
        Console.WriteLine("furthestVertex " + furthestVertex.Id);
        int builtEdgeId = -1;
        int availableCarriersCount = carriers.Count;
        int furtherVertexNeededSections = 0;
        foreach (ProblemEdge edge in edges)
        {
            if (edge.To == furthestVertex.Id)
            {   
                builtEdgeId = edge.Id;
                furtherVertexNeededSections = edge.Throughput!.Capacity - edge.Throughput.Flow;
                break;
            }
        }

        int carriersNeeded = 0;
        if (furtherVertexNeededSections > 0)
            carriersNeeded = (int)Math.Ceiling((double)furtherVertexNeededSections / 100);

        List<int> pathIndexes = FindShortestPathToVertex(FactoryVertex, furthestVertex, vertices, edges);
        if (availableCarriersCount >= carriersNeeded)
        {
            availableCarriersCount -= carriersNeeded;
            for (int i = 1; i < pathIndexes.Count; i++)
            {
                for (int j = 0; j < carriersNeeded; j++)
                {
                    if (pathIndexes[i] == furthestVertex.Id)
                    {
                        AddCarriedValueToHullEdges(edges[builtEdgeId], carriers[j]);
                    }
                    carriers[j].MoveTo(pathIndexes[i]);
                }
            }
        }
        else if (availableCarriersCount < carriersNeeded && availableCarriersCount > 0)
        {
            for (int i = 1; i < pathIndexes.Count; i++)
            {
                for (int j = availableCarriersCount - 1; j < carriers.Count; j++)
                {
                    if (pathIndexes[i] == furthestVertex.Id)
                    {
                        AddCarriedValueToHullEdges(edges[builtEdgeId], carriers[j]);
                    }
                    carriers[j].MoveTo(pathIndexes[i]);
                }
            }
        }
        List<List<int>> paths = new();
        Console.WriteLine("pathIndexes " + string.Join(", ", pathIndexes));
        Console.WriteLine("carriers " + carriers.Count);
        Console.WriteLine("carriers needed " + carriersNeeded);
        Console.WriteLine("edge flow " + edges[builtEdgeId].Throughput.Flow);
        problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(builtEdgeId, new(edges[builtEdgeId].Throughput.Flow, edges[builtEdgeId].Throughput.Capacity)));
        problemRecreationCommands?.NextStep();
    }
    private void AddCarriedValueToHullEdges(ProblemEdge edge, Carrier carrier)
    {
        if (edge == null || carrier == null)
        {
            Console.WriteLine("edge or carrier is null");
            return;
        }
        if (edge.Throughput?.Flow + carrier.Load <= edge.Throughput?.Capacity)
        {
            edge.Throughput!.Flow += carrier.Load;
            Console.WriteLine("edge flow2 " + edge.Throughput.Flow);
            carrier.Deliver(carrier.Load);
        }
        else
        {
            int remaining = edge.Throughput!.Capacity - edge.Throughput.Flow;
            edge.Throughput.Flow = edge.Throughput.Capacity;
            Console.WriteLine("edge flow3 " + edge.Throughput.Flow);
            carrier.Deliver(remaining);
        }
    }
    private void ReturnToFactory()
    {
    }
    private void AllocateCarriersToHullEdges()
    {
    }
    private ProblemVertex FindFurthestFenceVertex(ProblemVertex FactoryVertex, List<ProblemVertex> vertices, List<ProblemEdge> edges, List<int> HullIndexes, List<int> FinishedEdges)
    {
        List<int> distances = BFS(FactoryVertex, vertices, edges);
        int maxDistance = 0;
        int maxIndex = 0;
        for (int i = 0; i < HullIndexes.Count; i++)
        {
            if (distances[HullIndexes[i]] > maxDistance && !FinishedEdges.Contains(HullIndexes[i]))
            {
                maxDistance = distances[HullIndexes[i]];
                maxIndex = HullIndexes[i];
            }
        }
        return vertices[maxIndex];
    }
    private List<int> FindShortestPathToVertex(ProblemVertex start, ProblemVertex end, List<ProblemVertex> vertices, List<ProblemEdge> edges)
    {
        List<int> distances = BFS(start, vertices, edges);
        List<int> path = new();
        int current = end.Id;
        while (current != start.Id)
        {
            path.Add(current);
            foreach (var edge in edges)
            {
                if (edge.To == current && distances[edge.From] == distances[current] - 1)
                {
                    current = edge.From;
                    break;
                }
            }
        }
        path.Add(start.Id);
        path.Reverse();
        return path;
    }
    private List<int> BFS(ProblemVertex start, List<ProblemVertex> vertices, List<ProblemEdge> edges)
    {
        List<int> distances = new();
        for (int i = 0; i < vertices.Count; i++)
        {
            distances.Add(int.MaxValue);
        }
        Queue<ProblemVertex> queue = new();
        queue.Enqueue(start);
        distances[start.Id] = 0;
        while (queue.Count > 0)
        {
            ProblemVertex current = queue.Dequeue();
            foreach (ProblemEdge edge in edges)
            {
                if (edge.From == current.Id && distances[edge.To] == int.MaxValue)
                {
                    distances[edge.To] = distances[edge.From] + 1;
                    queue.Enqueue(vertices[edge.To]);
                }
                if (edge.To == current.Id && distances[edge.From] == int.MaxValue)
                {
                    distances[edge.From] = distances[edge.To] + 1;
                    queue.Enqueue(vertices[edge.From]);
                }
            }
            
        }
        return distances;
    }
}