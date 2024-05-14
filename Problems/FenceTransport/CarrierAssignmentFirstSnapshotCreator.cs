using Drawer.GraphDrawer;
using Microsoft.AspNetCore.Components;
using ProblemVisualizer;

namespace Problem.FenceTransport;

public class CarrierAssignmentFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private int canvasWidth = 1920;
    private int canvasHeight = 1080;
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
        for (int i = 1; i <= inputData.FrontCarrierNumber; i++)
        {
            int ValueY = canvasHeight / (inputData.FrontCarrierNumber + 1) * i;
            vertices.Add(new GraphVertex((int)(canvasWidth * 1/3), ValueY, (i-1).ToString(), null));
        }
    }
    private void CreateRearCarriers(List<GraphVertex> vertices)
    {
        for (int i = inputData.FrontCarrierNumber + 1; i <= inputData.FrontCarrierNumber + inputData.RearCarrierNumber; i++)
        {
            int ValueY = canvasHeight / (inputData.RearCarrierNumber + 1) * (i - inputData.FrontCarrierNumber);
            vertices.Add(new GraphVertex((int)(canvasWidth * 2/3), ValueY, (i-1).ToString(), null));
        }
    }
    private void CreateRelations(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        for (int i = 0; i < inputData.Relations.Count; i++)
        {
            edges.Add(new GraphEdge(vertices[inputData.Relations[i].From], vertices[inputData.Relations[i].To], null, new GraphThroughput(inputData.Relations[i].Flow, inputData.Relations[i].Capacity)));
        }
    }
    private void CreateSource(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex((int)(canvasWidth * 1/6), canvasHeight/2, "source", new GraphStateSpecial()));
        for (int i = 0; i < inputData.FrontCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[inputData.FrontCarrierNumber + inputData.RearCarrierNumber], vertices[i], null, new GraphThroughput(0, 1)));
        }
    }
    private void CreateSink(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        vertices.Add(new GraphVertex((int)(canvasWidth * 5/6), canvasHeight/2, "sink", new GraphStateSpecial()));
        for (int i = 0; i < inputData.RearCarrierNumber; i++)
        {
            edges.Add(new GraphEdge(vertices[inputData.FrontCarrierNumber + i], vertices[inputData.FrontCarrierNumber + inputData.RearCarrierNumber + 1], null, new GraphThroughput(0, 1)));
        }
    }
}