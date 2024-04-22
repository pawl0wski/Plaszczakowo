namespace CarrierAssignmentDrawer;

using Problem.CarrierAssignment;
using ProblemDrawer;
using GraphDrawer;

namespace CarrierAssignmentDrawer;

public class CarrierAssignmentDrawer : ProblemDrawer<CarrierAssignmentInputData, CarrierAssignmentOutputStep>
{
    public CarrierAssignmentDrawer(List<CarrierAssignmentOutputStep> steps, GraphDrawer graphDrawer) : base(steps, graphDrawer)
    {
    }
    protected override Task ModifyGraphDataByCurrentStep()
    {
        throw new NotImplementedException();
    }
    public override void CreateGraphDataFromInputData(CarrierAssignmentInputData data)
    {
        throw new NotImplementedException();
    }
}