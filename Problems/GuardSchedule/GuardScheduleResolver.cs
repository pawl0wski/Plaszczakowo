using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemVisualizer.Commands;

namespace Problem.GuardSchedule;

public class GuardScheduleResolver
    : ProblemResolver<GuardScheduleInputData, GuardScheduleOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    public override GuardScheduleOutput Resolve(GuardScheduleInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        GuardScheduleOutput output = new();
        problemRecreationCommands = commands;

        output = IteratePath(data.Plaszczaki, data.Pathway, output);

        return output;
    }

    private GuardScheduleOutput IteratePath(List<Plaszczak> plaszczaki, Pathway path, GuardScheduleOutput output)
    {
        plaszczaki.Sort();
        int plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(path.MaxVertexValue) == false) 
                break;

            for (int vertexIndex = 0; vertexIndex < path.Vertices.Count; vertexIndex++)
            {
                UpdatePosition(p, path, vertexIndex);

                problemRecreationCommands?.NextStep();

                EnoughEnergyOrSteps(p, path.MaxPossibleSteps);

                Resting(p);

                problemRecreationCommands?.Add(new ChangeEdgeStateCommand(vertexIndex, GraphStates.Active));
                problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertexIndex, GraphStates.Active));

                p.Steps++;
                p.Energy -= p.NextVertexValue;
                p.CurrentVertexIndex = vertexIndex;
            }

            output.Plaszczaki.Add(p);
            plaszczakIndex++;
        }

        return output;
    }

    private static void UpdatePosition(Plaszczak p, Pathway path, int vertexIndex)
    {
        if (vertexIndex == 0)
        {
            p.PreviousVertexValue = path.Vertices[path.Vertices.Count - 1];
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = path.Vertices[vertexIndex + 1];
        }
        else if (vertexIndex == path.Vertices.Count - 1)
        {
            p.PreviousVertexValue = path.Vertices[vertexIndex - 1];
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = path.Vertices[0];
        }
        else
        {
            p.PreviousVertexValue = path.Vertices[vertexIndex - 1];
            p.CurrentVertexValue = path.Vertices[vertexIndex];
            p.NextVertexValue = path.Vertices[vertexIndex + 1];
        }
    }

    private static void EnoughEnergyOrSteps(Plaszczak p, int maxSteps)
    {
        if (p.Energy < p.NextVertexValue || p.Steps == maxSteps - 1)
        {
            if (!(p.CurrentVertexValue < p.PreviousVertexValue))
            {
                ListenMelody(p);
            }
        }
    }

    private static void ListenMelody(Plaszczak p)
    {
        p.Steps = 0;
        p.Energy = p.MaxEnergy;
        p.Melody++;
    }

    private static void Resting(Plaszczak p)
    {
        if (p.CurrentVertexValue < p.PreviousVertexValue)
        {
            p.Steps = 0;
            p.Energy = p.MaxEnergy;
        }
    }
}

