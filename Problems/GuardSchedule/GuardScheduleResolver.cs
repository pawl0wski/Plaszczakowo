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
        int verticesCount = inputData.Vertices.Count;
        int xCoordinateForText = inputData.Vertices[inputData.Vertices.Count / 3].X + 150 ?? 0;

        plaszczaki.Sort();
        var plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(maxVertexValue) == false) 
                break;

            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
            {
                UpdatePosition(p, inputData.Vertices, vertexIndex);

                EnoughEnergyOrSteps(p, inputData.MaxPossibleSteps);

                Resting(p);

                ChangePlaszczakText(p, xCoordinateForText);

                ChangeGraphColor(vertexIndex);

                p.Steps++;
                p.Energy -= p.NextVertexValue;
                p.CurrentVertexIndex = vertexIndex;
            }

            problemRecreationCommands?.Add(new ResetGraphStateCommand());

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

    private void EnoughEnergyOrSteps(Plaszczak p, int maxSteps)
    {
        if (p.Energy < p.NextVertexValue || p.Steps == maxSteps - 1)
        {
            if (!(p.CurrentVertexValue < p.PreviousVertexValue))
            {
                ListenMelody(p);
            }
        }
    }

    private void ListenMelody(Plaszczak p)
    {
        p.Steps = 0;
        p.Energy = p.MaxEnergy;
        p.Melody++;
    }

    private void Resting(Plaszczak p)
    {
        if (p.CurrentVertexValue < p.PreviousVertexValue)
        {
            p.Steps = 0;
            p.Energy = p.MaxEnergy;
        }
    }
    private void ChangeGraphColor(int vertexIndex)
    {
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(vertexIndex, GraphStates.Highlighted));
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertexIndex, GraphStates.Highlighted));
        problemRecreationCommands?.NextStep();
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertexIndex, GraphStates.Active));
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(vertexIndex, GraphStates.Active));
    }
    private void ChangePlaszczakText(Plaszczak p, int xCoordinateForText)
    {
        problemRecreationCommands?.Add(new ChangeTextCommand(0, $"Energia: {p.Energy}", xCoordinateForText, 200));
        problemRecreationCommands?.Add(new ChangeTextCommand(1, $"Melodia: {p.Melody}", xCoordinateForText, 300));
        problemRecreationCommands?.Add(new ChangeTextCommand(2, $"Kroki: {p.Steps}", xCoordinateForText, 400));
    }
}

