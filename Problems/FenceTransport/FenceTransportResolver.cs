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
        for (int i = 0; i < data.ConvexHullOutput!.HullIndexes!.Count; i++)
        {
            int id = data.Edges.Count;
            int from = data.ConvexHullOutput.HullIndexes[i];
            int to = data.ConvexHullOutput.HullIndexes[(i + 1) % data.ConvexHullOutput.HullIndexes.Count];
            data.Edges.Add(new(id, from, to, new(0, CalculateLength(data.Vertices[from], data.Vertices[to]))));
        }
        FenceTransportOutput output = new();
        problemRecreationCommands = commands;

        
        ProblemVertex FactoryVertex = data.Vertices[data.FactoryIndex];
        List<int> FinishedEdges = new();


        return output;
    }
    private int CalculateLength(ProblemVertex vertex1, ProblemVertex vertex2)
    {
        int x1 = vertex1.X.GetValueOrDefault();
        int y1 = vertex1.Y.GetValueOrDefault();
        int x2 = vertex2.X.GetValueOrDefault();
        int y2 = vertex2.Y.GetValueOrDefault();
        return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
    private void CarryFence(ProblemVertex FactoryVertex, List<ProblemVertex> vertices, List<ProblemEdge> edges, List<int> HullIndexes, List<int> FinishedEdges)
    {
        int currentVertex = FactoryVertex.Id;
        ProblemVertex furthestVertex = FindFurthestFenceVertex(FactoryVertex, vertices, edges, HullIndexes, FinishedEdges);
        List<int> pathIndexes = FindPathToVertex(FactoryVertex, furthestVertex, vertices, edges, HullIndexes, FinishedEdges);
        for (int i = 0; i < pathIndexes.Count - 1; i++)
        {
            foreach (var edge in edges)
            {
                if (edge.From == pathIndexes[i] && edge.To == pathIndexes[i + 1])
                {
                    currentVertex = edge.To;
                }
            }
        }
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
    private List<int> FindPathToVertex(ProblemVertex start, ProblemVertex end, List<ProblemVertex> vertices, List<ProblemEdge> edges, List<int> HullIndexes, List<int> FinishedEdges)
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
            distances.Add(vertices.Count + 1);
        }
        distances[start.Id] = 0;
        Queue<ProblemVertex> queue = new();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            ProblemVertex current = queue.Dequeue();
            foreach (var edge in edges)
            {
                if (edge.From == current.Id)
                {
                    if (distances[edge.To] == vertices.Count + 1)
                    {
                        distances[edge.To] = distances[edge.From] + 1;
                        queue.Enqueue(vertices[edge.To]);
                    }
                }
            }
        }
        return distances;
    }
    
}

