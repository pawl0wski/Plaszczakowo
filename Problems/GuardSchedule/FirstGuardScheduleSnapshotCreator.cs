using Drawer.GraphDrawer;
using ProblemResolver.Converters;
using ProblemVisualizer;

namespace Problem.GuardSchedule;

public class FirstGuardScheduleSnapshotCreator(GuardScheduleInputData inputData)
    : FirstSnapshotCreator<GuardScheduleInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        var outputData = ProblemToGraphData.Convert(inputData);
        int xCoordinateForText = FindMaxXCoordinate();

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
        int maxX = int.MinValue;

        foreach (var vertex in inputData.Vertices)
        {
            if (vertex.X > maxX)
            {
                maxX = (int)vertex.X;
            }
        }

        return maxX + 150;
    }
}