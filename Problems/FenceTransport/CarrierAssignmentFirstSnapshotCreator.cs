using Drawer.GraphDrawer;
using Microsoft.AspNetCore.Components;
using ProblemVisualizer;

namespace Problem.FenceTransport;

public class CarrierAssignmentFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private int canvasWidth = 1920;
    private int canvasHeight = 1080;
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
            int ValueY = canvasHeight / (inputData.FrontCarrierNumber + 1) * i;
            vertices.Add(new GraphVertex((int)(canvasWidth * 1/3), ValueY, (i-1).ToString(), null));
        }
    }
    private void CreateRearCarriers(List<GraphVertex> vertices)
    {
        for (int i = _inputData.FrontCarrierNumber + 1; i <= _inputData.FrontCarrierNumber + _inputData.RearCarrierNumber; i++)
        {
            int ValueY = canvasHeight / (_inputData.RearCarrierNumber + 1) * (i - _inputData.FrontCarrierNumber);
            vertices.Add(new GraphVertex((int)(canvasWidth * 2/3), ValueY, (i-1).ToString(), null));
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
        vertices.Add(new GraphVertex((int)(canvasWidth * 1/6), canvasHeight/2, "source", new GraphStateSpecial()));
        for (int i = 0; i < _inputData.FrontCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber], vertices[i], null, new GraphThroughput(0, 1)));
        }
    }
    private void CreateSink(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex((int)(canvasWidth * 5/6), canvasHeight/2, "sink", new GraphStateSpecial()));
        for (int i = 0; i < _inputData.RearCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + i], vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber + 1], null, new GraphThroughput(0, 1)));
        }
    }
}