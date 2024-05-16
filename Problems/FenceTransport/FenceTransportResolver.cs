
using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemResolver.Graph;

namespace Problem.FenceTransport;

public class FenceTransportResolver : ProblemResolver<FenceTransportInputData, FenceTransportOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    public override FenceTransportOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        List<int> ConvexHullEdgesIndexes = AddHullEdges(data.Vertices[data.FactoryIndex], data.Vertices, data.Edges, data.ConvexHullOutput!.HullIndexes!);
        FenceTransportOutput output = new();
        problemRecreationCommands = commands;
        List<Carrier> carriers = [];
        for(int i = 0; i < (data.CarrierAssignmentOutput?.Pairs.Count() ?? 0); i++)
            carriers.Add(new Carrier(i, data.Vertices[data.FactoryIndex]));

        var firstVertex = GetUnfinishedVertieces(data).First();
        while(GetUnfinishedVertieces(data).Count > 0) {
            Console.WriteLine($"Tyle do roboty jeszcze {GetUnfinishedVertieces(data).Count}");
            foreach(var carrier in carriers){
                Console.WriteLine(firstVertex);
                Console.WriteLine("Zajmuje się kolejnym pacjentem");
                switch (carrier.State)
                {
                    case CarrierState.Unassigned:
                        Console.WriteLine($"Przydzielam robote {carrier.Id}");
                        AssignCarrierVertexToBuild(carrier, data);
                        Console.WriteLine($"Secc {firstVertex}");
                    break;
                    case CarrierState.Delivering:
                        if (carrier.CurrentRoute.Count > 0){
                            Console.WriteLine($"Siema. Tutaj {carrier.Id}. Ide sobie :) jestem na {carrier.Position.Id} a chce iść do {carrier.CurrentRoute.Last().Id}");
                            carrier.MoveTo(carrier.CurrentRoute.Dequeue());
                        } else {
                            Console.WriteLine($"Jestem już na miejscu. Zaraz będzie budowanko. {carrier.Id}");
                            var unfinishedEdges = GetUnfifinishedEdgesConnectedToVertex(carrier.Position, data);
                            if (unfinishedEdges.Count > 0) {
                                ProblemEdge edgeToVisit = unfinishedEdges.OrderByDescending(e => e.Throughput?.Capacity - e.Throughput?.Flow).FirstOrDefault()!;
                                carrier.CurrentRoute.Enqueue(GetSecondEndOfEdge(carrier.Position, edgeToVisit, data.Vertices)!);
                                carrier.EdgeToBuild = edgeToVisit; 
                                carrier.State = CarrierState.Building;
                            }else {
                                Console.WriteLine($"Ojojoj. Ktoś mi już zabrał parace :C {carrier.Id}");
                                carrier.State = CarrierState.Unassigned;
                                AssignCarrierVertexToBuild(carrier, data);
                            }
                        }
                    break;
                    case CarrierState.Building:
                        carrier.Position = carrier.CurrentRoute.Dequeue();
                        carrier.Deliver();
                        Console.WriteLine($"Budu budu :D {carrier.Id} ja żem wybudować {carrier.EdgeToBuild?.Throughput?.Flow} / {carrier?.EdgeToBuild?.Throughput?.Capacity}");
                        
                        if (carrier.Load > 0){
                            AssignCarrierVertexToBuild(carrier, data);
                        }
                        else {
                            ReturnCarrierToFactory(carrier, data);
                        }
                    break;
                    case CarrierState.Reffiling:
                        if (carrier.CurrentRoute.Count > 0) {
                            Console.WriteLine($"Ide po paliwko :) {carrier.Id}");
                            carrier.MoveTo(carrier.CurrentRoute.Dequeue());
                        }else {
                            Console.WriteLine($"Tsssssk... Otwieram browara {carrier.Id}");
                            carrier.Refill();
                            AssignCarrierVertexToBuild(carrier, data);                            
                        }
                    break;
                }

            }
            Console.WriteLine($"Tyle do roboty jeszcze {GetUnfinishedVertieces(data).Count}");
        }
        return output;
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
        Console.WriteLine($"Przydzielam {carrier.Id} do {furthestVertex.Id}");
        Console.WriteLine($"Droga {string.Join(" -> ", path.Select(v => v.Id))}");
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
        var vertices = data.Vertices;
        var edges = data.Edges;
    
        // Initialize the distances array with int.MaxValue for all vertices
        var distances = new int[vertices.Count];
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = int.MaxValue;
        }
    
        // Initialize the previous vertex array to keep track of the path
        var previous = new int[vertices.Count];
    
        // The distance from the start vertex to itself is 0
        distances[start.Id] = 0;
    
        // Create a queue for the vertices to visit
        var queue = new Queue<ProblemVertex>();
        queue.Enqueue(start);
    
        // BFS algorithm
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
    
            foreach (var edge in edges)
            {
                var neighborId = edge.From == current.Id ? edge.To : edge.From;
    
                if (distances[neighborId] == int.MaxValue)
                {
                    queue.Enqueue(vertices[neighborId]);
                    distances[neighborId] = distances[current.Id] + 1;
                    previous[neighborId] = current.Id;
                }
            }
        }
    
        // If the end vertex is not reachable from the start vertex
        if (distances[end.Id] == int.MaxValue)
        {
            return new List<ProblemVertex>();
        }
    
        // Build the shortest path from start to end by following the previous vertices
        var path = new List<ProblemVertex>();
        for (var vertex = end; vertex != null; vertex = previous[vertex.Id] == start.Id ? null : vertices[previous[vertex.Id]])
        {
            path.Add(vertex);
        }
    
        // Reverse the path to get it from start to end and return it
        path.Reverse();
        return path;
    }
}