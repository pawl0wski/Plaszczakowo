namespace Problem.CarrierAssignment;

public class CarrierAssignmentResolver :
    ProblemResolver<CarrierAssignmentInputData, CarrierAssignmentOutputStep>
{
    List<CarrierAssignmentOutputStep> results = new();
    public Edge Connect(int from, int to)
    {
        return new Edge(from, to, 0, 1);
    }
    private bool BFS(CarrierAssignmentOutputStep network, int source, int sink)
    {
        bool[] visited = new bool[network.Vertices.Count];
        Queue<int> queue = new();
        queue.Enqueue(source);
        visited[source] = true;
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (var edge in network.Edges)
            {
                if (edge.From == current && edge.Capacity > edge.Flow && !visited[edge.To])
                {
                    queue.Enqueue(edge.To);
                    visited[edge.To] = true;
                }
            }
        }
        return visited[sink];
    }

    public void PairCreator(CarrierAssignmentOutputStep network, int source, int sink)
    {
        
        while (BFS(network, source, sink))
        {
            int current = source;
            int minCapacity = 1;
            while (current != sink)
            {
                bool foundEdge = false;
                foreach (var edge in network.Edges)
                {
                    if (edge.From == current && edge.Capacity > edge.Flow)
                    {
                        minCapacity = Math.Min(minCapacity, edge.Capacity - edge.Flow);
                        current = edge.To;
                        foundEdge = true;
                        break;
                    }
                }
                if (!foundEdge)
                {
                    break;
                }
            }
            current = source;
            while (current != sink)
            {
                bool foundEdge = false;
                for (int i = 0; i < network.Edges.Count; i++)
                {
                    if (network.Edges[i].From == current && network.Edges[i].Capacity > network.Edges[i].Flow)
                    {
                        network.Edges[i].Flow += minCapacity;
                        current = network.Edges[i].To;
                        results.Add(network);
                        foundEdge = true;
                        break;
                    }
                }
                if (!foundEdge)
                {
                    break;
                }
            }
        }
            Console.WriteLine("Min capacity: ");
    }
    
    
    public override List<CarrierAssignmentOutputStep> Resolve(CarrierAssignmentInputData data)
    {
        List<int> Verticies = data.Carriers;
        int SetSize1 = data.FrontCarrierNumber;
        int SetSize2 = data.RearCarrierNumber;
        int TotalNodes = SetSize1 + SetSize2 + 2; // +2 for source and sink
        List<Edge> Relations = data.Relations;
        int Source = 0;
        int Sink = TotalNodes - 1;
        Verticies.Add(Source);
        Verticies.Add(Sink);
        List<Edge> Edges = new List<Edge>();
        foreach (var relation in Relations)
        {
            Edges.Add(Connect(relation.From, relation.To));
        }
        for (int i = 0; i < SetSize1; i++)
        {
            Edges.Add(Connect(Verticies[TotalNodes - 2], i));
        }
        for (int i = SetSize1; i < SetSize1 + SetSize2; i++)
        {
            Edges.Add(Connect(i, Verticies[TotalNodes - 1]));
        }
        CarrierAssignmentOutputStep network = new(Verticies, Edges);
        results.Add(network);
        PairCreator(network, Source, Sink);
        return results;
    }
}
