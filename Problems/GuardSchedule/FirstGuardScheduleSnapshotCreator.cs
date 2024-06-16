using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.ProblemResolver.Converters;
using Plaszczakowo.Problems.GuardSchedule.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.GuardSchedule;

public class FirstGuardScheduleSnapshotCreator(GuardScheduleInputData inputData)
    : FirstSnapshotCreator<GuardScheduleInputData, GraphData>(inputData)
{
    private readonly GuardScheduleInputData _inputData = inputData;

    public override GraphData CreateFirstSnapshot()
    {
        var outputData = ProblemToGraphData.Convert(_inputData);
        var xCoordinateForText = FindMaxXCoordinate();

        outputData.ChangeVertexStatus(0, GraphStates.Special);
        outputData.Texts.Add(new GraphText("Id 💂: -", xCoordinateForText, 260, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Max ⚡: -", xCoordinateForText, 310, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Energia: -", xCoordinateForText, 360, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Melodia: -", xCoordinateForText, 410, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Max 🦶: -", xCoordinateForText, 460, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Kroki: -", xCoordinateForText, 510, GraphStates.Inactive));
        return outputData;
    }

    private int FindMaxXCoordinate()
    {
        var maxX = int.MinValue;

        foreach (var vertex in _inputData.Vertices)
            if (vertex.X > maxX)
                maxX = (int)vertex.X;

        return maxX + 150;
    }
}