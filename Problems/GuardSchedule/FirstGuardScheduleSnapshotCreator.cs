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
        outputData.Texts.Add(new GraphText("Index: -", xCoordinateForText, 200, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Max ⚡: -", xCoordinateForText, 250, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Energia: -", xCoordinateForText, 300, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Melodia: -", xCoordinateForText, 350, GraphStates.Inactive));
        outputData.Texts.Add(new GraphText("Kroki: -", xCoordinateForText, 400, GraphStates.Inactive));
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