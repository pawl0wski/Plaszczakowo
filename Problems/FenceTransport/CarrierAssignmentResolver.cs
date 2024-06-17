using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.Images;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.Problems.FenceTransport.Output;
using Plaszczakowo.ProblemVisualizer.Commands;

namespace Plaszczakowo.Problems.FenceTransport;

public class CarrierAssignmentResolver : ProblemResolver<FenceTransportInputData, CarrierAssignmentOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    public override CarrierAssignmentOutput Resolve(FenceTransportInputData data,
        ref ProblemRecreationCommands<GraphData> commands)
    {
        CarrierAssignmentFirstSnapshotCreator creator = new(data);
        var graphData = creator.CreateFirstSnapshot();
        problemRecreationCommands = commands;
        var verticesCount = graphData.Vertices.Count;
        return PairCreator(graphData, verticesCount - 2, verticesCount - 1, data);
    }

    public CarrierAssignmentOutput PairCreator(GraphData network, int source, int sink, FenceTransportInputData data)
    {
        var pairs = new CarrierAssignmentOutput();
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(source, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(sink, GraphStates.Special));
        var parent = new int[network.Vertices.Count];
        while (DFS(network, source, sink, parent))
        {
            var pathFlow = int.MaxValue;
            for (var vertexIndex = sink; vertexIndex != source; vertexIndex = parent[vertexIndex])
            {
                var previousIndex = parent[vertexIndex];
                foreach (var edge in network.Edges)
                    if (edge.From == network.Vertices[previousIndex] && edge.To == network.Vertices[vertexIndex])
                    {
                        if (edge.Throughput != null)
                            pathFlow = Math.Min(pathFlow, edge.Throughput.Capacity - edge.Throughput.Flow);
                        break;
                    }
            }

            for (var vertexIndex = sink; vertexIndex != source; vertexIndex = parent[vertexIndex])
            {
                var previousIndex = parent[vertexIndex];
                foreach (var edge in network.Edges)
                    if (edge.From == network.Vertices[previousIndex] && edge.To == network.Vertices[vertexIndex])
                    {
                        if (edge.Throughput != null)
                            edge.Throughput.Flow += pathFlow;
                        if (pathFlow == 1)
                            if (previousIndex != source && vertexIndex != sink)
                                pairs.Pairs.Add(new Pair(previousIndex, vertexIndex));
                        break;
                    }
                DrawGraph(network, data);
            }
        }

        return pairs;
    }

    private bool DFS(GraphData network, int source, int sink, int[] parent)
    {
        var visited = new bool[network.Vertices.Count];
        Stack<int> stack = new();
        stack.Push(source);
        visited[source] = true;
        parent[source] = -1;
        while (stack.Count > 0)
        {
            var current = stack.Pop();

            foreach (var edge in network.Edges)
                if (IsValidEdge(edge, current, visited, network))
                {
                    var to = network.Vertices.IndexOf(edge.To);
                    stack.Push(to);
                    parent[to] = current;
                    visited[to] = true;

                    if (to == sink) return true;
                }
        }

        return visited[sink];
    }
    private bool IsValidEdge(GraphEdge edge, int current, bool[] visited, GraphData network)
    {
        return edge.Throughput != null
               && edge.From == network.Vertices[current]
               && edge.Throughput.Capacity > edge.Throughput.Flow
               && !visited[network.Vertices.IndexOf(edge.To)];
    }
    private void DrawGraph(GraphData graphData, FenceTransportInputData data)
    {
        problemRecreationCommands?.Add(new ResetGraphStateCommand());
        foreach (var edge in graphData.Edges)
            if (edge.Throughput?.Flow == 1)
            {
                problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(graphData.Edges.IndexOf(edge),
                    new GraphThroughput(edge.Throughput.Flow, edge.Throughput.Capacity)));
                problemRecreationCommands?.Add(new ChangeEdgeStateCommand(graphData.Edges.IndexOf(edge), GraphStates.Active));
                var from = graphData.Vertices.IndexOf(edge.From);
                var to = graphData.Vertices.IndexOf(edge.To);
                problemRecreationCommands?.Add(new ChangeVertexStateCommand(from,
                    GraphStates.Active));
                problemRecreationCommands?.Add(new ChangeVertexStateCommand(to,
                    GraphStates.Active));
                if (from >= 0 && from < data.FrontCarrierNumber)
                    problemRecreationCommands?.Add(new ChangeVertexImageCommand(from,
                        GraphVertexImages.FrontCarrierActiveImage));
                else if (from >= data.FrontCarrierNumber && from < data.FrontCarrierNumber + data.RearCarrierNumber)
                    problemRecreationCommands?.Add(
                        new ChangeVertexImageCommand(from, GraphVertexImages.RearCarrierActiveImage));
                if (to >= 0 && to < data.FrontCarrierNumber)
                    problemRecreationCommands?.Add(new ChangeVertexImageCommand(to,
                        GraphVertexImages.FrontCarrierActiveImage));
                else if (to >= data.FrontCarrierNumber && to < data.FrontCarrierNumber + data.RearCarrierNumber)
                    problemRecreationCommands?.Add(
                        new ChangeVertexImageCommand(to, GraphVertexImages.RearCarrierActiveImage));
            }
            else if (edge.Throughput?.Flow == 0)
            {
                problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(graphData.Edges.IndexOf(edge),
                    new GraphThroughput(edge.Throughput.Flow, edge.Throughput.Capacity)));
                problemRecreationCommands?.Add(new ChangeEdgeStateCommand(graphData.Edges.IndexOf(edge), GraphStates.Inactive));
                var from = graphData.Vertices.IndexOf(edge.From);
                var to = graphData.Vertices.IndexOf(edge.To);
                problemRecreationCommands?.Add(new ChangeVertexStateCommand(from,
                    GraphStates.Inactive));
                problemRecreationCommands?.Add(new ChangeVertexStateCommand(to,
                    GraphStates.Inactive));
                if (from >= 0 && from < data.FrontCarrierNumber)
                    problemRecreationCommands?.Add(new ChangeVertexImageCommand(from,
                        GraphVertexImages.FrontCarrierInactiveImage));
                else if (from >= data.FrontCarrierNumber && from < data.FrontCarrierNumber + data.RearCarrierNumber)
                    problemRecreationCommands?.Add(
                        new ChangeVertexImageCommand(from, GraphVertexImages.RearCarrierInactiveImage));
                if (to >= 0 && to < data.FrontCarrierNumber)
                    problemRecreationCommands?.Add(new ChangeVertexImageCommand(to,
                        GraphVertexImages.FrontCarrierInactiveImage));
                else if (to >= data.FrontCarrierNumber && to < data.FrontCarrierNumber + data.RearCarrierNumber)
                    problemRecreationCommands?.Add(
                        new ChangeVertexImageCommand(to, GraphVertexImages.RearCarrierInactiveImage));
            }
        DrawSourceAndSink(graphData);
        problemRecreationCommands?.NextStep();
    }

    private void DrawSourceAndSink(GraphData graphData)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(graphData.Vertices.Count - 2, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(graphData.Vertices.Count - 1, GraphStates.Special));
    }

}