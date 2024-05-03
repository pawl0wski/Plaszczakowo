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
        outputData.ChangeVertexStatus(0, GraphStates.Special);
        return outputData;
    }
}