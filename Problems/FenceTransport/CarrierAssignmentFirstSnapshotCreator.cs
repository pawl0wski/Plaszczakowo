using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.Images;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.FenceTransport;

public class CarrierAssignmentFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private readonly FenceTransportInputData _inputData = inputData;
    private readonly int canvasHeight = 1080;
    private readonly int canvasWidth = 1920;

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
        for (var i = 1; i <= _inputData.FrontCarrierNumber; i++)
        {
            var ValueY = canvasHeight / (_inputData.FrontCarrierNumber + 1) * i;
            vertices.Add(new GraphVertex(canvasWidth * 1 / 3,
                ValueY, (i - 1).ToString(),
                null,
                GraphVertexImages.FrontCarrierInactiveImage));
        }
    }

    private void CreateRearCarriers(List<GraphVertex> vertices)
    {
        for (var i = _inputData.FrontCarrierNumber + 1;
             i <= _inputData.FrontCarrierNumber + _inputData.RearCarrierNumber;
             i++)
        {
            var ValueY = canvasHeight / (_inputData.RearCarrierNumber + 1) * (i - _inputData.FrontCarrierNumber);
            vertices.Add(new GraphVertex(canvasWidth * 2 / 3,
                ValueY,
                (i - 1).ToString(),
                null,
                GraphVertexImages.RearCarrierInactiveImage));
        }
    }

    private void CreateRelations(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        for (var i = 0; i < _inputData.Relations.Count; i++)
            edges.Add(new GraphEdge(vertices[_inputData.Relations[i].From], vertices[_inputData.Relations[i].To], null,
                new GraphThroughput(_inputData.Relations[i].Flow, _inputData.Relations[i].Capacity)));
    }

    private void CreateSource(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex(canvasWidth * 1 / 6, canvasHeight / 2, "source", new GraphStateSpecial()));
        for (var i = 0; i < _inputData.FrontCarrierNumber; i++)
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber], vertices[i],
                null, new GraphThroughput(0, 1)));
    }

    private void CreateSink(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex(canvasWidth * 5 / 6, canvasHeight / 2, "sink", new GraphStateSpecial()));
        for (var i = 0; i < _inputData.RearCarrierNumber; i++)
            edges.Add(new GraphEdge(vertices[_inputData.FrontCarrierNumber + i],
                vertices[_inputData.FrontCarrierNumber + _inputData.RearCarrierNumber + 1], null,
                new GraphThroughput(0, 1)));
    }
}