using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;

namespace Problem.CarrierAssignment;

public class CarrierAssignmentResolver : ProblemResolver<FenceTransportInputData, CarrierAssignmentOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    
    public override CarrierAssignmentOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        CarrierAssignmentFirstSnapshotCreator creator = new(data);
        GraphData graphData = creator.CreateFirstSnapshot();
        problemRecreationCommands = commands;
        int verticesCount = graphData.Vertices.Count;
        return PairCreator(graphData, verticesCount - 2, verticesCount - 1);
    }

    public CarrierAssignmentOutput PairCreator(GraphData network, int source, int sink)
    {
        var pairs = new CarrierAssignmentOutput();
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(source, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(sink, GraphStates.Special));
        int[] parent = new int[network.Vertices.Count];
        while (BFS(network, source, sink, parent))
        {
            int pathFlow = int.MaxValue;
            for (int vertexIndex = sink; vertexIndex != source; vertexIndex = parent[vertexIndex])
            {

                int previousIndex = parent[vertexIndex];
                foreach (GraphEdge edge in network.Edges)
                {
                    if (edge.From == network.Vertices[previousIndex] && edge.To == network.Vertices[vertexIndex])
                    {
                        if (edge.Throughput != null)
                            pathFlow = Math.Min(pathFlow, edge.Throughput.Capacity - edge.Throughput.Flow);
                        ChangeGraphState(network, edge, previousIndex, pathFlow);
                        break;
                    }
                }
            }
            for (int vertexIndex = sink; vertexIndex != source; vertexIndex = parent[vertexIndex])
            {
                int previousIndex = parent[vertexIndex];
                foreach (GraphEdge edge in network.Edges)
                {
                    if (edge.From == network.Vertices[previousIndex]
                     && edge.To == network.Vertices[vertexIndex])
                    {
                        if (edge.Throughput != null) 
                            edge.Throughput.Flow += pathFlow;
                        if (pathFlow == 1)
                            if (previousIndex != source && vertexIndex != sink)
                                pairs.Pairs.Add(new Pair(previousIndex, vertexIndex));
                        break;
                    }
                }
            }
        }
        return pairs;
    }
    private bool BFS(GraphData network, int source, int sink, int[] parent)
    {
        bool[] visited = new bool[network.Vertices.Count];
        Queue<int> queue = new();
        queue.Enqueue(source);
        visited[source] = true;
        parent[source] = -1;
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (GraphEdge edge in network.Edges)
            {
                if (IsValidEdge(edge, current, visited, network))
                {
                    int to = network.Vertices.IndexOf(edge.To);
                    queue.Enqueue(to);
                    parent[to] = current;
                    visited[to] = true;
                }
            }
        }
        return visited[sink];
    }
    public void ChangeGraphState(GraphData network, GraphEdge edge, int index, int pathFlow){
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(network.Edges.IndexOf(edge), GraphStates.Active));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
        if (edge.Throughput != null)
            problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(network.Edges.IndexOf(edge),
                new GraphThroughput(edge.Throughput.Flow + pathFlow, edge.Throughput.Capacity)));
        problemRecreationCommands?.NextStep();
    }

    private bool IsValidEdge(GraphEdge edge, int current, bool[] visited, GraphData network)
    {
        return edge.Throughput != null
               && edge.From == network.Vertices[current]
               && edge.Throughput.Capacity > edge.Throughput.Flow
               && !visited[network.Vertices.IndexOf(edge.To)];
    }
}

