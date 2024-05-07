using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class FirstCarrierAssignmentSnapshotCreator(CarrierAssignmentInputData inputData)
    : FirstSnapshotCreator<CarrierAssignmentInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];
        for (int i = 1; i <= inputData.FrontCarrierNumber; i++){
            vertices.Add(new GraphVertex(300, i * 100));
        }
        for (int i = inputData.FrontCarrierNumber + 1; i <= inputData.FrontCarrierNumber + inputData.RearCarrierNumber; i++){
            vertices.Add(new GraphVertex(700, (i - inputData.FrontCarrierNumber) * 100));
        }
        for (int i = 0; i < inputData.Relations.Count; i++)
        {
            edges.Add(new GraphEdge(vertices[inputData.Relations[i].From], vertices[inputData.Relations[i].To], null, new GraphThroughput(inputData.Relations[i].Flow, inputData.Relations[i].Capacity)));
        }
        //source
        vertices.Add(new GraphVertex(100, inputData.FrontCarrierNumber * 50 + 50, "source", new GraphStateSpecial()));
        for (int i = 0; i < inputData.FrontCarrierNumber; i++){
            edges.Add(new GraphEdge(vertices[inputData.FrontCarrierNumber + inputData.RearCarrierNumber], vertices[i], null, new GraphThroughput(0, 1)));
        }
        //sink
        vertices.Add(new GraphVertex(900, inputData.RearCarrierNumber * 50 + 50, "sink", new GraphStateSpecial()));
        for (int i = 0; i < inputData.RearCarrierNumber; i++){
            edges.Add(new GraphEdge(vertices[inputData.FrontCarrierNumber + i], vertices[inputData.FrontCarrierNumber + inputData.RearCarrierNumber + 1], null, new GraphThroughput(0, 1)));
        }

        return new GraphData(vertices, edges, []);
    }
}