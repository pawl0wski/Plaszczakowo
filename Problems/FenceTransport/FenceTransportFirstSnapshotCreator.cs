using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.Images;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Problems.FenceTransport;

public class FenceTransportFirstSnapshotCreator(FenceTransportInputData inputData)
    : FirstSnapshotCreator<FenceTransportInputData, GraphData>(inputData)
{
    private readonly FenceTransportInputData _inputData = inputData;

    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];

        SetFactoryVertex();
        CreateVertices(vertices);
        CreateEdges(vertices, edges);
        return new (vertices, edges, []);
    }
    private void SetFactoryVertex()
    {
        _inputData.FactoryIndex = _inputData.Vertices.First(x => x.IsSpecial).Id;
    }

    private void CreateVertices(List<GraphVertex> graphVertices)
    {
        if (InputData.CarrierAssignmentOutput is null || InputData.CarrierAssignmentOutput.Pairs is null)
            throw new NullReferenceException("InputData cannot be null");
        var carriersCount = InputData.CarrierAssignmentOutput.Pairs.Count();
        _inputData.Vertices.Sort((x, y)=>x.Id.CompareTo(y.Id));
        
        foreach (var vertex in _inputData.Vertices) 
        {
            if (vertex.Id == _inputData.FactoryIndex){
                graphVertices.Add(new GraphVertex(
                    vertex.X ?? 0, 
                    vertex.Y ?? 0, 
                    carriersCount.ToString(),
                    GraphStates.Special,
                    GraphVertexImages.Factory));
            }
               
            else if (_inputData.ConvexHullOutput!.HullIndexes!.Contains(vertex.Id)){
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, 
                vertex.Y ?? 0, 
                0.ToString(), 
                GraphStates.Active));
            }
            else
                graphVertices.Add(new GraphVertex(vertex.X ?? 0, vertex.Y ?? 0, 0.ToString()));
        }
    }
    private void CreateEdges(List<GraphVertex> vertices, List<GraphEdge> edges)
    {
        foreach (var edge in _inputData.Edges)
        {
            if (edge.Throughput is null)
                edges.Add(new GraphEdge(vertices[edge.From], vertices[edge.To]));
            else
                edges.Add(new GraphEdge(vertices[edge.From],
                vertices[edge.To], GraphStates.Active,
                throughput: new GraphThroughput(0, edge.Throughput.Capacity)));
        }
    }
}