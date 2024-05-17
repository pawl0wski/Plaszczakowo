using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.CarrierAssignment;

public class CarrierAssignmentFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private readonly FenceTransportInputData _inputData = inputData;

    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];
        
        CreateFrontCarriers(vertices);
        CreateRearCarriers(vertices);
        CreateRelations(vertices, edges);
        CreateSource(vertices, edges);
        CreateSink(vertices, edges);

        return new GraphData(vertices, edges, []);
    }
    private void CreateFrontCarriers(List<GraphVertex> vertices)
    {
        for (int i = 1; i <= _inputData.FrontCarrierNumber; i++)
        {
            vertices.Add(new GraphVertex(300, i * 100));
        }
    }
    private void CreateRearCarriers(List<GraphVertex> vertices)
    {
        for (int i = _inputData.FrontCarrierNumber + 1; i <= _inputData.FrontCarrierNumber + _inputData.RearCarrierNumber; i++)
        {
            vertices.Add(new GraphVertex(700, (i - _inputData.FrontCarrierNumber) * 100));
        }
    }
    private void CreateRelations(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        for (int i = 0; i < _inputData.Relations.Count; i++)
        {
            edges.Add(new GraphEdge(vertices[_inputData.Relations[i].From], vertices[_inputData.Relations[i].To], null, new GraphThroughput(_inputData.Relations[i].Flow, _inputData.Relations[i].Capacity)));
        }
    }
    private void CreateSource(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex(100, _inputData.FrontCarrierNumber * 50 + 50, "source", new GraphStateSpecial()));
        for (int i = 0; i < _inputData.FrontCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber], vertices[i], null, new GraphThroughput(0, 1)));
        }
    }
    private void CreateSink(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex(900, _inputData.RearCarrierNumber * 50 + 50, "sink", new GraphStateSpecial()));
        for (int i = 0; i < _inputData.RearCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + i], vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber + 1], null, new GraphThroughput(0, 1)));
        }
    }
}