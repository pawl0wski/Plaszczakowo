using Drawer.GraphDrawer;
using ProblemVisualizer;

namespace Problem.GuardSchedule;

public class FirstGuardScheduleSnapshotCreator(GuardScheduleInputData inputData)
    : FirstSnapshotCreator<GuardScheduleInputData, GraphData>(inputData)
{
    static Random r = new();
    public override GraphData CreateFirstSnapshot()
    {
        List<GraphVertex> vertices = [];
        List<GraphEdge> edges = [];
        int centerX = 640;
        int centerY = 360;
        int radiusX;
        int radiusY;
        int x, y;

        int verticeAmount = inputData.Pathway.Vertices.Count;

        for (int vertexIndex = 0; vertexIndex < verticeAmount; vertexIndex++)
        {
            radiusX = 20 * verticeAmount;
            radiusY = 15 * verticeAmount;
            double angle = 2 * Math.PI * vertexIndex / verticeAmount;
            x = (int)(centerX + radiusX * Math.Cos(angle));
            y = (int)(centerY + radiusY * Math.Sin(angle));

            vertices.Add(new GraphVertex(x, y, inputData.Pathway.Vertices[vertexIndex].ToString()));
        }

        for (int vertexIndex = 0; vertexIndex < verticeAmount; vertexIndex++)
        {
            edges.Add(new GraphEdge(vertices[vertexIndex], vertexIndex == verticeAmount - 1 ? vertices.First() : vertices[vertexIndex + 1]));
        }

        vertices.First().State = GraphStates.Special;        

        return new GraphData(vertices, edges);
    }
}