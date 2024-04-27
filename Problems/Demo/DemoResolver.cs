namespace Problem.Demo;

using GraphDrawer;
using ProblemVisualizer.Commands;
using ProblemResolver;


public class DemoResolver : ProblemResolver<DemoInputData, DemoProblemOutput, GraphData>
{
    public override DemoProblemOutput Resolve(DemoInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        var output = new DemoProblemOutput();
        for (int i = 0; i < data.Edges; i++)
        {
            commands.Add(new ChangeEdgeStateCommand(i, GraphStates.Highlighted));
            if (i > 0) 
                commands.Add(new ChangeEdgeStateCommand(i-1, GraphStates.Active));
            commands.NextStep();
        }

        output.HighlightedEdges = data.Edges;
        return output;
    }
}