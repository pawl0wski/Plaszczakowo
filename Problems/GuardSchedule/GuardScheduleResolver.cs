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

        IteratePath(data, ref output);

        return output;
    }

    private void IteratePath(in GuardScheduleInputData inputData, ref GuardScheduleOutput output)
    {
        var maxVertexValue = inputData.Vertices.Max((v) => v.Value)!.Value;
        var plaszczaki = inputData.Plaszczaki;
        int verticesCount = inputData.Vertices.Count;
        int xCoordinateForText = FindMaxXCoordinate(inputData.Vertices);

        plaszczaki.Sort();
        var plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(maxVertexValue) == false) 
                break;

            for (int vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
            {
                int previousMelody = p.Melody;

                UpdatePosition(p, inputData.Vertices, vertexIndex);

                EnoughEnergyOrSteps(p, inputData.MaxPossibleSteps, vertexIndex);

                Resting(p);

                ChangePlaszczakText(p, xCoordinateForText, previousMelody, vertexIndex);
                ChangeGraphColor(vertexIndex);

                p.Steps++;
                p.Energy -= p.NextVertexValue;
                p.CurrentVertexIndex = vertexIndex;
            }

            problemRecreationCommands?.Add(new ResetGraphStateCommand());

            output.Plaszczaki.Add(p);
            plaszczakIndex++;
        }
    }

    private static void UpdatePosition(Plaszczak p, List<ProblemVertex> vertices, int vertexIndex)
    {
        if (vertexIndex == 0)
        {
            p.PreviousVertexValue = vertices[vertices.Count - 1].Value ?? 0;
            p.CurrentVertexValue = vertices[vertexIndex].Value ?? 0;
            p.NextVertexValue = vertices[vertexIndex + 1].Value ?? 0;
        }
        else if (vertexIndex == vertices.Count - 1)
        {
            p.PreviousVertexValue = vertices[vertexIndex - 1].Value ?? 0;
            p.CurrentVertexValue = vertices[vertexIndex].Value ?? 0;
            p.NextVertexValue = vertices[0].Value ?? 0;
        }
        else
        {
            p.PreviousVertexValue = vertices[vertexIndex - 1].Value ?? 0;
            p.CurrentVertexValue = vertices[vertexIndex].Value ?? 0;
            p.NextVertexValue = vertices[vertexIndex + 1].Value ?? 0;
        }
    }

    private void EnoughEnergyOrSteps(Plaszczak p, int maxSteps, int vertexIndex)
    {
        if ((p.Energy < p.NextVertexValue || p.Steps >= maxSteps) && vertexIndex != 0)
        {
            if (p.CurrentVertexValue >= p.PreviousVertexValue)
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
    private void ChangePlaszczakText(Plaszczak p, int xCoordinateForText, int previousMelody, int vertexIndex)
    {
        problemRecreationCommands?.Add(new ChangeTextCommand(0, $"Index: {p.Index}", xCoordinateForText, 200, GraphStates.Inactive));
        problemRecreationCommands?.Add(new ChangeTextCommand(1, $"Max ⚡: {p.MaxEnergy}", xCoordinateForText, 250, GraphStates.Inactive));

        if (p.Energy == p.MaxEnergy && vertexIndex != 0)
            problemRecreationCommands?.Add(new ChangeTextCommand(2, $"Energia: 💤", xCoordinateForText, 300, GraphStates.Active));
        else
            problemRecreationCommands?.Add(new ChangeTextCommand(2, $"Energia: {p.Energy}", xCoordinateForText, 300, GraphStates.Inactive));
        
        if (previousMelody < p.Melody)
            problemRecreationCommands?.Add(new ChangeTextCommand(3, $"Melodia: {p.Melody} +🎵", xCoordinateForText, 350, GraphStates.Active));
        else
            problemRecreationCommands?.Add(new ChangeTextCommand(3, $"Melodia: {p.Melody}", xCoordinateForText, 350, GraphStates.Inactive));

        if (p.Steps == 0 && vertexIndex != 0)
            problemRecreationCommands?.Add(new ChangeTextCommand(4, $"Kroki: 💤", xCoordinateForText, 400, GraphStates.Active));
        else
            problemRecreationCommands?.Add(new ChangeTextCommand(4, $"Kroki: {p.Steps}", xCoordinateForText, 400, GraphStates.Inactive));
    }
    private int FindMaxXCoordinate(List<ProblemVertex> inputData)
    {
        int maxX = int.MinValue;

        foreach (var vertex in inputData)
        {
            if (vertex.X > maxX)
            {
                maxX = (int)vertex.X;
            }
        }

        return maxX + 150;
    }
}

