using GraphDrawer;
using ProblemVisualizer;

namespace Problem;

/*
    Rozwiązanie problemu zwracane przez ProblemResolver
*/
public abstract class ProblemResults
{
    public List<ProblemVisualizerCommands<GraphData>> ListOfCommands;

    protected ProblemResults()
    {
        ListOfCommands = [];
        NextStep();
    }
    
    public void AddCommand(ProblemVisualizerCommand<GraphData> command)
    {
        ListOfCommands.Last().Enqueue(command);
    }

    public void NextStep()
    {
        ListOfCommands.Add(new ProblemVisualizerCommands<GraphData>());
    }
}