using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.Demo;

using GraphDrawer;
using ProblemResolver;


public class DemoResolver : ProblemResolver<DemoInputData, DemoProblemOutput, GraphData>
{

    private List<ProblemEdge> _nonVisitedEdges = [];
    public override DemoProblemOutput Resolve(DemoInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        var output = new DemoProblemOutput();
        _nonVisitedEdges.AddRange(data.Edges);
        Stack<ProblemEdge> edges = [];

        var currentVertex = data.Vertices.First();
        commands.Add(new ChangeVertexStateCommand(currentVertex.Id, new GraphStateActive()));
        AddAllEdgesToStack(edges, currentVertex, _nonVisitedEdges, ref commands);
        
            while (edges.Count != 0)
            {
                var currentEdge = edges.Pop();
                commands.Add(new ChangeEdgeStateCommand(currentEdge.Id, GraphStates.Active));
                currentVertex = data.Vertices[currentEdge.To];
                commands.Add(new ChangeVertexStateCommand(currentVertex.Id, GraphStates.Active));
                commands.NextStep();
                
                AddAllEdgesToStack(edges, currentVertex, _nonVisitedEdges, ref commands);
            }
        
        return output;
    }

    private List<ProblemEdge> GetAllEdgesWithVertex(ProblemVertex vertex, List<ProblemEdge> edges)
    {
        List<ProblemEdge> edgesToOutput = [];
        foreach (var edge in edges.ToList())
        {
            if (edge.From == vertex.Id)
            {
                edges.Remove(edge);
                edgesToOutput.Add(edge);
            }
        }
        return edgesToOutput;
    }

    private void AddAllEdgesToStack(
        Stack<ProblemEdge> edgesStack,
        ProblemVertex vertex,
        List<ProblemEdge> edges,
        ref ProblemRecreationCommands<GraphData> commands
        )
    {
        foreach (var edge in GetAllEdgesWithVertex(vertex, edges))
        {
            commands.Add(new ChangeEdgeStateCommand(edge.Id, GraphStates.Highlighted));
            edgesStack.Push(edge);
            commands.NextStep();
        }
    }
}