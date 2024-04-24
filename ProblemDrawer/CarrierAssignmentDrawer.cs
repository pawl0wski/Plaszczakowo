namespace CarrierAssignmentDrawer;

using Problem.CarrierAssignment;
using ProblemDrawer;
using GraphDrawer;

public class CarrierAssignmentDrawer : ProblemDrawer<CarrierAssignmentInputData, CarrierAssignmentOutputStep>
{
    public List<GraphVertex> GraphVertex;
    public List<GraphEdge> GraphEdge;
    public CarrierAssignmentDrawer(List<CarrierAssignmentOutputStep> steps, GraphDrawer graphDrawer) : base(steps, graphDrawer)
    {
        GraphVertex = new List<GraphVertex>();
        GraphEdge = new List<GraphEdge>();
    }
    protected override Task ModifyGraphDataByCurrentStep()
    {
        throw new NotImplementedException();
    }
    public override void CreateGraphDataFromInputData(CarrierAssignmentInputData data)
    {
        for (int i = 1; i <= data.FrontCarrierNumber; i++){
            GraphVertex.Add(new GraphVertex(300, i * 100));
        }
        for (int i = data.FrontCarrierNumber + 1; i <= data.FrontCarrierNumber + data.RearCarrierNumber; i++){
            GraphVertex.Add(new GraphVertex(700, (i - data.FrontCarrierNumber) * 100));
        }
        for (int i = 0; i < data.Relations.Count; i++)
        {
            GraphEdge.Add(new GraphEdge(GraphVertex[data.Relations[i].From], GraphVertex[data.Relations[i].To], null, new GraphFlow(data.Relations[i].Flow, data.Relations[i].Capacity)));
        }
        //source
        GraphVertex.Add(new GraphVertex(100, data.FrontCarrierNumber * 50 + 50, "source", new GraphStateSpecial()));
        for (int i = 0; i < data.FrontCarrierNumber; i++){
            GraphEdge.Add(new GraphEdge(GraphVertex[data.FrontCarrierNumber + data.RearCarrierNumber], GraphVertex[i], null, new GraphFlow(0, 1)));
        }
        //sink
        GraphVertex.Add(new GraphVertex(900, data.RearCarrierNumber * 50 + 50, "sink", new GraphStateSpecial()));
        for (int i = 0; i < data.RearCarrierNumber; i++){
            GraphEdge.Add(new GraphEdge(GraphVertex[data.FrontCarrierNumber + i], GraphVertex[data.FrontCarrierNumber + data.RearCarrierNumber + 1], null, new GraphFlow(0, 1)));
        }

        Drawer.ApplyNewGraphData(new GraphData(GraphVertex, GraphEdge));
    }

}