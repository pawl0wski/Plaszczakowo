using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.Images;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemResolver.ProblemGraph;
using Plaszczakowo.Problems.GuardSchedule.Input;
using Plaszczakowo.Problems.GuardSchedule.Output;
using Plaszczakowo.ProblemVisualizer.Commands;

namespace Plaszczakowo.Problems.GuardSchedule;

public class GuardScheduleResolver
    : ProblemResolver<GuardScheduleInputData, GuardScheduleOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? _problemRecreationCommands;

    public override GuardScheduleOutput Resolve(GuardScheduleInputData data,
        ref ProblemRecreationCommands<GraphData> commands)
    {
        GuardScheduleOutput output = new();
        _problemRecreationCommands = commands;
        PrepareInputData(ref data);

        IteratePath(data, ref output);

        return output;
    }

    private void PrepareInputData(ref GuardScheduleInputData data)
    {
        List<ProblemVertex> sortedProblemVertices = [];
        List<ProblemEdge> sortedProblemEdges = [];

        var currentVertex = data.Vertices.First(v => v.IsSpecial);
        ProblemEdge? currentEdge;
        sortedProblemVertices.Add(currentVertex);

        while (true)
        {
            currentEdge = data.Edges.First(e => e.From == currentVertex.Id);
            sortedProblemEdges.Add(currentEdge);
            currentVertex = data.Vertices.First(v => currentEdge.To == v.Id);
            if (currentVertex == sortedProblemVertices[0])
                break;
            sortedProblemVertices.Add(currentVertex);
        }

        for (var newVertId = 0; newVertId < sortedProblemVertices.Count; newVertId++)
            sortedProblemVertices[newVertId].Id = newVertId;

        for (var newEdgeId = 0; newEdgeId < sortedProblemEdges.Count; newEdgeId++)
        {
            sortedProblemEdges[newEdgeId].From = newEdgeId;
            sortedProblemEdges[newEdgeId].To = newEdgeId == sortedProblemEdges.Count - 1 ? 0 : newEdgeId + 1;
        }

        data.Vertices = sortedProblemVertices;
        data.Edges = sortedProblemEdges;
    }

    private void IteratePath(in GuardScheduleInputData inputData, ref GuardScheduleOutput output)
    {
        var maxVertexValue = inputData.Vertices.Max(v => v.Value)!.Value;
        var plaszczaki = inputData.Plaszczaki;
        var verticesCount = inputData.Vertices.Count;
        var xCoordinateForText = FindMaxXCoordinate(inputData.Vertices);
        var maxSteps = inputData.MaxPossibleSteps;

        plaszczaki.Sort();
        var plaszczakIndex = 0;

        foreach (var p in plaszczaki)
        {
            p.Index = plaszczakIndex;

            if (p.IsGuard(maxVertexValue) == false)
                break;

            for (var vertexIndex = 0; vertexIndex < verticesCount; vertexIndex++)
            {
                var previousMelody = p.Melody;

                UpdatePosition(p, inputData.Vertices, vertexIndex);

                EnoughEnergyOrSteps(p, maxSteps, vertexIndex);

                Resting(p);

                ChangeVertexImages(p, vertexIndex, previousMelody);
                ChangePlaszczakText(p, xCoordinateForText, previousMelody, vertexIndex, maxSteps);
                ChangeGraphColor(vertexIndex);

                p.Steps++;
                p.Energy -= p.NextVertexValue;
                p.CurrentVertexIndex = vertexIndex;
            }

            _problemRecreationCommands?.Add(new ResetGraphStateCommand());

            output.Plaszczaki.Add(p);
            plaszczakIndex++;
        }
    }

    private void ChangeVertexImages(Plaszczak p, int vertexIndex, int previousMelody)
    {
        _problemRecreationCommands?.Add(new RemoveAllVertexImageCommand());
        GraphVertexImage? plaszczakImage = null;

        if (p.Energy == p.MaxEnergy && vertexIndex != 0)
            plaszczakImage = GraphVertexImages.PlaszczakSleeping;
        if (previousMelody < p.Melody && vertexIndex != 0)
            plaszczakImage = GraphVertexImages.PlaszczakMusic;

        _problemRecreationCommands?.Add(new ChangeVertexImageCommand(vertexIndex,
            plaszczakImage ?? GraphVertexImages.PlaszczakStep1));
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
            if (p.CurrentVertexValue >= p.PreviousVertexValue)
                ListenMelody(p);
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
        _problemRecreationCommands?.Add(new ChangeEdgeStateCommand(vertexIndex, GraphStates.Highlighted));
        _problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertexIndex, GraphStates.Highlighted));
        _problemRecreationCommands?.NextStep();
        _problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertexIndex, GraphStates.Active));
        _problemRecreationCommands?.Add(new ChangeEdgeStateCommand(vertexIndex, GraphStates.Active));
    }

    private void ChangePlaszczakText(Plaszczak p, int xCoordinateForText, int previousMelody, int vertexIndex,
        int maxSteps)
    {
        _problemRecreationCommands?.Add(new ChangeTextCommand(0, $"Id 💂: {p.Index}", xCoordinateForText, 260,
            GraphStates.Inactive));
        _problemRecreationCommands?.Add(new ChangeTextCommand(1, $"Max ⚡: {p.MaxEnergy}", xCoordinateForText, 310,
            GraphStates.Inactive));

        if (p.Energy == p.MaxEnergy && vertexIndex != 0)
            _problemRecreationCommands?.Add(new ChangeTextCommand(2, $"Energia: {p.Energy} 💤", xCoordinateForText, 360,
                GraphStates.Active));
        else
            _problemRecreationCommands?.Add(new ChangeTextCommand(2, $"Energia: {p.Energy}", xCoordinateForText, 360,
                GraphStates.Inactive));

        if (previousMelody < p.Melody)
            _problemRecreationCommands?.Add(new ChangeTextCommand(3, $"Melodia: {p.Melody} +🎵", xCoordinateForText,
                410, GraphStates.Active));
        else
            _problemRecreationCommands?.Add(new ChangeTextCommand(3, $"Melodia: {p.Melody}", xCoordinateForText, 410,
                GraphStates.Inactive));

        _problemRecreationCommands?.Add(new ChangeTextCommand(4, $"Max 🦶: {maxSteps}", xCoordinateForText, 460,
            GraphStates.Inactive));

        if (p.Steps == 0 && vertexIndex != 0)
            _problemRecreationCommands?.Add(new ChangeTextCommand(5, $"Kroki: {p.Steps} 💤", xCoordinateForText, 510,
                GraphStates.Active));
        else
            _problemRecreationCommands?.Add(new ChangeTextCommand(5, $"Kroki: {p.Steps}", xCoordinateForText, 510,
                GraphStates.Inactive));
    }

    private int FindMaxXCoordinate(List<ProblemVertex> inputData)
    {
        var maxX = int.MinValue;

        foreach (var vertex in inputData)
            if (vertex.X > maxX)
                maxX = (int)vertex.X;

        return maxX + 150;
    }
}