using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;

namespace Problem.FenceTransport;

public class CarrierAssignmentResolver : ProblemResolver<FenceTransportInputData, CarrierAssignmentOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    public override CarrierAssignmentOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        CarrierAssignmentFirstSnapshotCreator creator = new(data);
        GraphData graphData = creator.CreateFirstSnapshot();
        problemRecreationCommands = commands;
        int verticesCount = graphData.Vertices.Count;
        return PairCreator(graphData, verticesCount - 2, verticesCount - 1, data);
    }

    public CarrierAssignmentOutput PairCreator(GraphData network, int source, int sink, FenceTransportInputData data)
    {
        var pairs = new CarrierAssignmentOutput();
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(source, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(sink, GraphStates.Special));
        int[] parent = new int[network.Vertices.Count];
        while (DFS(network, source, sink, parent))
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
                        ChangeGraphState(network, edge, previousIndex, pathFlow, data);
                        break;
                    }
                }
            }
            for (int vertexIndex = sink; vertexIndex != source; vertexIndex = parent[vertexIndex])
            {
                int previousIndex = parent[vertexIndex];
                foreach (GraphEdge edge in network.Edges)
                {
                    if (edge.From == network.Vertices[previousIndex] && edge.To == network.Vertices[vertexIndex])
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

    private bool DFS(GraphData network, int source, int sink, int[] parent)
    {
        bool[] visited = new bool[network.Vertices.Count];
        Stack<int> stack = new();
        stack.Push(source);
        visited[source] = true;
        parent[source] = -1;
        while (stack.Count > 0)
        {
            int current = stack.Pop();

            foreach (GraphEdge edge in network.Edges)
            {
                if (IsValidEdge(edge, current, visited, network))
                {
                    int to = network.Vertices.IndexOf(edge.To);
                    stack.Push(to);
                    parent[to] = current;
                    visited[to] = true;

                    if (to == sink)
                    {
                        return true;
                    }
                }
            }
        }
        return visited[sink];
    }

    public void ChangeGraphState(GraphData network, GraphEdge edge, int index, int pathFlow, FenceTransportInputData data)
    {
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(network.Edges.IndexOf(edge), GraphStates.Active));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
        if (index >= 0 && index < data.FrontCarrierNumber)
            problemRecreationCommands?.Add(new ChangeVertexImageCommand(index, GraphVertexImages.FrontCarrierActiveImage));
        else if (index >= data.FrontCarrierNumber && index < data.FrontCarrierNumber + data.RearCarrierNumber)
            problemRecreationCommands?.Add(new ChangeVertexImageCommand(index, GraphVertexImages.RearCarrierActiveImage));
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
