using GraphDrawer;
using ProblemVisualizer.Commands;

namespace Problem.Demo;

public class DemoResolver : ProblemResolver<DemoInputData, DemoProblemResults>
{
    public override DemoProblemResults Resolve(DemoInputData data)
    {
        var output = new DemoProblemResults();
        for (int i = 0; i < data.Edges; i++)
        {
            output.AddCommand(new ChangeEdgeStateCommand(i, GraphStates.Highlighted));
            if (i > 0) 
                output.AddCommand(new ChangeEdgeStateCommand(i-1, GraphStates.Active));
            output.NextStep();
        }

        return output;
    }
}