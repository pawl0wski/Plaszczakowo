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
        DrawSourceAndSink(network);

        Dictionary<GraphVertex, GraphEdge> parentMap;
        while (DFS(network, source, sink, out parentMap))
        {
            int pathFlow = int.MaxValue;
            List<GraphVertex> path = new List<GraphVertex>();
            for (var v = network.Vertices[sink]; v != network.Vertices[source]; v = parentMap[v].From)
            {
                path.Add(v);
            }
            path.Add(network.Vertices[source]);
            path.Reverse();

            foreach (var v in path)
            {
                if (v == network.Vertices[source]) continue;

                var edge = parentMap[v];
                pathFlow = Math.Min(pathFlow, edge.Throughput!.Capacity - edge.Throughput.Flow);
            }

            foreach (var v in path)
            {
                if (v == network.Vertices[source]) continue;

                var edge = parentMap[v];
                edge.Throughput!.Flow += pathFlow;

                var reverseEdge = GetEdge(edge.To, edge.From, network);
                if (reverseEdge == null)
                {
                    reverseEdge = new GraphEdge(edge.To, edge.From, null, new GraphThroughput(0, pathFlow));
                    network.Edges.Add(reverseEdge);
                }
                reverseEdge.Throughput!.Flow -= pathFlow;

                DrawGraph(network, data);
            }
        }
        GetPairs(network, out pairs);
        return pairs;
    }

    private bool DFS(GraphData network, int source, int sink, out Dictionary<GraphVertex, GraphEdge> parentMap)
    {
        parentMap = new();
        HashSet<GraphVertex> visited = new();
        Stack<GraphVertex> stack = new();
    
        stack.Push(network.Vertices[source]);
        visited.Add(network.Vertices[source]);
    
        while (stack.Count > 0)
        {
            var current = stack.Pop();
    
            foreach (var edge in network.Edges.Where(e => e.From == current))
            {
                var neighbour = edge.To;
                if (IsValidEdge(edge, neighbour, visited, network))
                {
                    parentMap[neighbour] = edge;
                    if (neighbour == network.Vertices[sink])
                        return true;
    
                    visited.Add(neighbour);
                    stack.Push(neighbour);
                }
            }
        }
    
        return false;
    }
    
    private bool IsValidEdge(GraphEdge edge, GraphVertex neighbour, HashSet<GraphVertex> visited, GraphData network)
    {
        return edge.Throughput != null
               && edge.Throughput.Capacity > edge.Throughput.Flow
               && !visited.Contains(neighbour);
    }

    private GraphEdge GetEdge(GraphVertex from, GraphVertex to, GraphData network)
    {
        return network.Edges.FirstOrDefault(edge => edge.From == from && edge.To == to);
    }

    private void GetPairs(GraphData network, out CarrierAssignmentOutput output)
    {
        output = new();
        foreach (var edge in network.Edges)
        {
            if (edge.Throughput?.Flow == 1
                && network.Vertices.IndexOf(edge.From) < network.Vertices.Count - 2
                && network.Vertices.IndexOf(edge.To) < network.Vertices.Count - 2)
            {
                var from = network.Vertices.IndexOf(edge.From);
                var to = network.Vertices.IndexOf(edge.To);
                output.Pairs.Add(new Pair(from, to));
            }
        }
    }

    private void DrawGraph(GraphData graphData, FenceTransportInputData data)
    {
        problemRecreationCommands?.Add(new ResetGraphStateCommand());
        foreach (var edge in graphData.Edges)
            if (edge.Throughput?.Flow == 1)
            {
                ChangeEdge(graphData.Edges.IndexOf(edge), edge);

                var from = graphData.Vertices.IndexOf(edge.From);
                var to = graphData.Vertices.IndexOf(edge.To);
                ActivateVertex(from);
                ActivateVertex(to);

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
                ChangeEdge(graphData.Edges.IndexOf(edge), edge);
            }

        DrawSourceAndSink(graphData);
        problemRecreationCommands?.NextStep();
    }

    private void DrawSourceAndSink(GraphData graphData)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(graphData.Vertices.Count - 2, GraphStates.Special));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(graphData.Vertices.Count - 1, GraphStates.Special));
    }

    private void ActivateVertex(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
    }

    private void ChangeEdge(int index, GraphEdge edge)
    {
        problemRecreationCommands?.Add(new ChangeEdgeFlowCommand(index,
            new GraphThroughput(edge.Throughput!.Flow, edge.Throughput.Capacity)));
        if (edge.Throughput!.Flow == 1)
            problemRecreationCommands?.Add(new ChangeEdgeStateCommand(index, GraphStates.Active));
        else
            problemRecreationCommands?.Add(new ChangeEdgeStateCommand(index, GraphStates.Inactive));
    }
}