namespace CarrierAssignmentDrawer;

using Problem.CarrierAssignment;
using ProblemDrawer;
using GraphDrawer;

public class CarrierAssignmentDrawer : ProblemDrawer<CarrierAssignmentInputData, CarrierAssignmentOutputStep>
{
    public List<GraphVertex> graphVertex;
    public List<GraphEdge> graphEdge;
    public CarrierAssignmentDrawer(List<CarrierAssignmentOutputStep> steps, GraphDrawer graphDrawer) : base(steps, graphDrawer)
    {
    }
    protected override Task ModifyGraphDataByCurrentStep()
    {
        throw new NotImplementedException();
    }
    public override void CreateGraphDataFromInputData(CarrierAssignmentInputData data)
    {
        
        for (int i = 0; i < data.FrontCarrierNumber; i++){
            graphVertex.Add(new GraphVertex(200, i * 100));
        }
        for (int i = data.FrontCarrierNumber; i < data.RearCarrierNumber; i++){
            graphVertex.Add(new GraphVertex(500, i * 100));
        }
        for (int i = 0; i < data.Relations.Count; i++)
        {
            graphEdge.Add(new GraphEdge(graphVertex[data.Relations[i].From], graphVertex[data.Relations[i].To], null, new GraphFlow(data.Relations[i].Flow, data.Relations[i].Capacity)));
        }
        Drawer.ApplyNewGraphData(new GraphData(graphVertex, graphEdge));

    }
}