using Drawer.GraphDrawer;
using ElectronNET.API.Entities;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.FenceTransport;

public class FenceTransportResolver : ProblemResolver<FenceTransportInputData, FenceTransportOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    private int _factoryIndex;
    public override FenceTransportOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        List<int> ConvexHullEdgesIndexes = AddHullEdges(data.Vertices[data.FactoryIndex], data.Vertices, data.Edges, data.ConvexHullOutput!.HullIndexes!);
        
        FenceTransportOutput output = new();
        _factoryIndex = data.FactoryIndex;
        problemRecreationCommands = commands;
        int hoursCount = 0;
        List<Carrier> carriers = CreateCarriers(data);
        if (carriers.Count == 0) {
            output.HoursToBuild = -1;
            return output;
        }

        data.Vertices[data.FactoryIndex].Value = carriers.Count;
        var firstVertex = GetUnfinishedVertieces(data).First();
        while(GetUnfinishedVertieces(data).Count > 0) {
            bool moved = false;
            foreach(var carrier in carriers){
                switch (carrier.State)
                {
                    case CarrierState.Unassigned:
                        AssignCarrierVertexToBuild(carrier, data);
                    break;
                    case CarrierState.Delivering:
                        if (carrier.CurrentRoute.Count > 0){
                            MoveCarrierOnGraph(carrier);
                            moved = true;
                        } else {
                            var unfinishedEdges = GetUnfifinishedEdgesConnectedToVertex(carrier.Position, data);
                            if (unfinishedEdges.Count > 0) {
                                ProblemEdge edgeToVisit = unfinishedEdges.OrderByDescending(e => e.Throughput?.Capacity - e.Throughput?.Flow).FirstOrDefault()!;
                                carrier.CurrentRoute.Enqueue(GetSecondEndOfEdge(carrier.Position, edgeToVisit, data.Vertices)!);
                                carrier.EdgeToBuild = edgeToVisit; 
                                carrier.State = CarrierState.Building;
                            }else {
                                carrier.State = CarrierState.Unassigned;
                                AssignCarrierVertexToBuild(carrier, data);
                            }
                        }
                    break;
                    case CarrierState.Building:
                        MoveCarrierOnGraph(carrier);
                        moved = true;
                        carrier.Deliver();
                        problemRecreationCommands.Add(new ChangeEdgeFlowCommand(carrier!.EdgeToBuild!.Id, new GraphThroughput(carrier!.EdgeToBuild!.Throughput!.Flow, carrier.EdgeToBuild.Throughput.Capacity)));
                        
                        if (carrier!.Load > 0){
                            AssignCarrierVertexToBuild(carrier, data);
                        }
                        else {
                            ReturnCarrierToFactory(carrier, data);
                        }
                        break;
                    case CarrierState.Reffiling:
                        if (carrier.CurrentRoute.Count > 0) {
                            MoveCarrierOnGraph(carrier);
                            moved = true;
                        }else {
                            carrier.Refill();
                            AssignCarrierVertexToBuild(carrier, data);                            
                        }
                        break;
                }

            }
            if (moved)
            {
                problemRecreationCommands.NextStep();
                hoursCount++;
            }
        }
        output.HoursToBuild = hoursCount;
        return output;
    }

    private List<Carrier> CreateCarriers(FenceTransportInputData data) {
        List<Carrier> carriers = [];
        for(int i = 0; i < (data.CarrierAssignmentOutput?.Pairs.Count ?? 0); i++)
            carriers.Add(new Carrier(i, data.Vertices[data.FactoryIndex]));
        return carriers;
    }
    private void MoveCarrierOnGraph(Carrier carrier){
        carrier.Position.Value -= 1;
        problemRecreationCommands?.Add(new ChangeVertexValueCommand(carrier.Position.Id, carrier.Position.Value.ToString()!));
        AddCarriersImageToVertexIf(carrier.Position.Value != 0, carrier.Position.Id);
        
        carrier.MoveTo(carrier.CurrentRoute.Dequeue());
        carrier.Position.Value = (carrier.Position.Value ?? 0) + 1;
        problemRecreationCommands?.Add(new ChangeVertexValueCommand(carrier.Position.Id, carrier.Position.Value.ToString()!));

        AddCarriersImageToVertexIf(carrier.Position.Value != 0, carrier.Position.Id);
    }

    private void AddCarriersImageToVertexIf(bool expression, int index)
    {
        if (expression && _factoryIndex != index)
            problemRecreationCommands?.Add(new ChangeVertexImageCommand(index, GraphVertexImages.PlaszczakiFence));
        else
        {
            if( _factoryIndex != index)
                problemRecreationCommands?.Add(new RemoveVertexImageCommand(index));
        }
            
    }

    private void ReturnCarrierToFactory(Carrier carrier, FenceTransportInputData data)
    {
        var factoryVertex = data.Vertices[data.FactoryIndex];
        var path = FindShortestPathToVertex(carrier.Position, factoryVertex, data);
        foreach (var vertex in path) carrier.CurrentRoute.Enqueue(vertex);
        carrier.State = CarrierState.Reffiling;
    }

    private void AssignCarrierVertexToBuild(Carrier carrier, FenceTransportInputData data)
    {
        var furthestVertex = FindFurthestUnfinishedFenceVertex(carrier.Position, data);
        var path = FindShortestPathToVertex(carrier.Position, furthestVertex, data);
        foreach(var vertex in path) carrier.CurrentRoute.Enqueue(vertex);
        carrier.State = CarrierState.Delivering;
    }
    private ProblemVertex? GetSecondEndOfEdge(ProblemVertex current, ProblemEdge edge, List<ProblemVertex> vertices) {
        if (edge.From == current.Id) 
            return vertices.First(v => v.Id == edge.To);
        if (edge.To == current.Id)
            return vertices.First(v => v.Id == edge.From);
        return null;
    }

        private List<int> AddHullEdges(ProblemVertex FactoryVertex, List<ProblemVertex> vertices, 
        List<ProblemEdge> edges, List<int> HullIndexes)
    {
        List<int> HullEdgesIndexes = new();
        for (int i = 0; i < HullIndexes.Count; i++)
        {
            int from = HullIndexes[i];
            int to = HullIndexes[(i + 1) % HullIndexes.Count];
            HullEdgesIndexes.Add(edges.Count);
            edges.Add(new(edges.Count, from, to, new(0, CalculateLengthBetweenHullVerticies(vertices[from], vertices[to]))));
        }
        return HullEdgesIndexes;

    }
    
    private int CalculateLengthBetweenHullVerticies(ProblemVertex vertex1, ProblemVertex vertex2)
    {
        int x1 = vertex1.X.GetValueOrDefault();
        int y1 = vertex1.Y.GetValueOrDefault();
        int x2 = vertex2.X.GetValueOrDefault();
        int y2 = vertex2.Y.GetValueOrDefault();
        return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }

    private List<ProblemVertex> GetUnfinishedVertieces(FenceTransportInputData data)
    {
        List<ProblemVertex> toReturn = [];
        foreach(var vertex in GetConvexHullVertices(data)) {
            if (GetUnfifinishedEdgesConnectedToVertex(vertex, data).Count > 0)
                toReturn.Add(vertex);
        }
        return toReturn;
    }

    private List<ProblemVertex> GetConvexHullVertices(FenceTransportInputData data) {
        return data.Vertices.Where(v => data.ConvexHullOutput?.HullIndexes?.Contains(v.Id) ?? false).ToList();
    }

    private List<ProblemEdge> GetUnfifinishedEdgesConnectedToVertex(ProblemVertex vertex, FenceTransportInputData data) {
        List<ProblemEdge> toReturn = [];
        foreach (var edge in data.Edges) {
            if (!((vertex.Id == edge.From) || (vertex.Id == edge.To)))
                continue;
            
            if (!(edge.Throughput is not null &&
                    edge.Throughput.Flow != edge.Throughput.Capacity))
                continue;
            
            toReturn.Add(edge);
        }
        return toReturn;
    }
    private ProblemVertex FindFurthestUnfinishedFenceVertex(ProblemVertex FactoryVertex, FenceTransportInputData data)
    {
        var HullIndexes = data.ConvexHullOutput!.HullIndexes!;
        var vertices = data.Vertices;
        List<int> distances = GetDistancesToEachVertex(FactoryVertex, data);
        int maxDistance = 0;
        int maxIndex = 0;
        List<ProblemVertex> UnfinishedVertices = GetUnfinishedVertieces(data);
        for (int i = 0; i < HullIndexes.Count; i++)
        {
            if (distances[HullIndexes[i]] > maxDistance && UnfinishedVertices.Contains(vertices[HullIndexes[i]]))
            {
                maxDistance = distances[HullIndexes[i]];
                maxIndex = HullIndexes[i];
            }
        }
        return vertices[maxIndex];
    }

    private List<int> GetDistancesToEachVertex(ProblemVertex start, FenceTransportInputData data)
    {
        List<ProblemVertex> vertices = data.Vertices;
        List<ProblemEdge> edges = data.Edges;
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
    private List<ProblemVertex> FindShortestPathToVertex(ProblemVertex start, ProblemVertex end, FenceTransportInputData data)
    {
        List<int> distances = GetDistancesToEachVertex(start, data);
        List<ProblemVertex> path = new List<ProblemVertex>();
        ProblemVertex current = end;
        while (current != start)
        {
            path.Add(current);
            foreach (ProblemEdge edge in data.Edges)
            {
                if (edge.From == current.Id && distances[edge.To] == distances[current.Id] - 1)
                {
                    current = data.Vertices[edge.To];
                    break;
                }
                if (edge.To == current.Id && distances[edge.From] == distances[current.Id] - 1)
                {
                    current = data.Vertices[edge.From];
                    break;
                }
            }
        }
        path.Add(start);

        path.Reverse();
        return path;
    }
}