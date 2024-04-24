using GraphDrawer;

namespace ProblemDrawer;

class ProblemDrawerCommands<TDrawerData> where TDrawerData : GraphData
{
    private List<ProblemDrawerCommand<TDrawerData>> _commands = [];

    public void Add(ProblemDrawerCommand<TDrawerData> command)
    {
        _commands.Add(command);
    }

}