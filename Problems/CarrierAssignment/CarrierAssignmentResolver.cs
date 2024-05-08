using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.CarrierAssignment;

public class CarrierAssignmentResolver : ProblemResolver<CarrierAssignmentInputData, CarrierAssignmentOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    
    public override CarrierAssignmentOutput Resolve(CarrierAssignmentInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        FirstCarrierAssignmentSnapshotCreator creator = new(data);
        GraphData graphData = creator.CreateFirstSnapshot();
        var output = new CarrierAssignmentOutput();
        problemRecreationCommands = commands;

        PairCreator(graphData, graphData.Vertices.Count - 2, graphData.Vertices.Count - 1, out List<GraphEdge> pairs);
        output.Pairs = pairs;
        return output;
    }

    public void PairCreator(GraphData network, int source, int sink, out List<GraphEdge> pairs)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(source, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(sink, GraphStates.Special));
        int[] parent = new int[network.Vertices.Count];
        pairs = new();
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
                    if (edge.From == network.Vertices[previousIndex] && edge.To == network.Vertices[vertexIndex])
                    {
                        edge.Throughput.Flow += pathFlow;
                        if (pathFlow == 1)
                            pairs.Add(edge);
                        break;
                    }
                }
            }
        }
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
                if (edge.From == network.Vertices[current] && edge.Throughput.Capacity > edge.Throughput.Flow && !visited[network.Vertices.IndexOf(edge.To)])
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
        problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(network.Edges.IndexOf(edge), new GraphThroughput(edge.Throughput.Flow + pathFlow, edge.Throughput.Capacity)));
        problemRecreationCommands?.NextStep();
    }
}

