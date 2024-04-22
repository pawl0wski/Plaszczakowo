namespace CarrierAssignmentDrawer;

using Problem.CarrierAssignment;
using ProblemDrawer;
using GraphDrawer;


public class CarrierAssignmentDrawer : ProblemDrawer<CarrierAssignmentOutputStep>
{
    public CarrierAssignmentDrawer(List<CarrierAssignmentOutputStep> steps, GraphDrawer graphDrawer) : base(steps, graphDrawer)
    {
    }
    protected override Task Draw()
    {
        throw new NotImplementedException();
    }
}