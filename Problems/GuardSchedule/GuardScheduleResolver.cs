using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemResolver.Graph;
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

        output = IteratePath(data, output);

        return output;
    }

    private GuardScheduleOutput IteratePath(in GuardScheduleInputData inputData, GuardScheduleOutput output)
    {
        var maxVertexValue = inputData.Vertices.Max((v) => v.Value)!.Value;
        var plaszczaki = inputData.Plaszczaki;
        
        plaszczaki.Sort();
        var plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(maxVertexValue) == false) 
                break;

            for (int vertexIndex = 0; vertexIndex < inputData.Vertices.Count; vertexIndex++)
            {
                UpdatePosition(p, inputData.Vertices, vertexIndex);

                problemRecreationCommands?.NextStep();

                EnoughEnergyOrSteps(p, inputData.MaxPossibleSteps);

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

    private static void UpdatePosition(Plaszczak p, List<ProblemVertex> vertices, int vertexIndex)
    {
        if (vertexIndex == 0)
        {
            p.PreviousVertexValue = vertices[vertices.Count - 1].Id;
            p.CurrentVertexValue = vertices[vertexIndex].Id;
            p.NextVertexValue = vertices[vertexIndex + 1].Id;
        }
        else if (vertexIndex == vertices.Count - 1)
        {
            p.PreviousVertexValue = vertices[vertexIndex - 1].Id;
            p.CurrentVertexValue = vertices[vertexIndex].Id;
            p.NextVertexValue = vertices[0].Id;
        }
        else
        {
            p.PreviousVertexValue = vertices[vertexIndex - 1].Id;
            p.CurrentVertexValue = vertices[vertexIndex].Id;
            p.NextVertexValue = vertices[vertexIndex + 1].Id;
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

