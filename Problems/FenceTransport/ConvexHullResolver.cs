using Plaszczakowo.Drawer.GraphDrawer;
using Plaszczakowo.Drawer.GraphDrawer.States;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemResolver.ProblemGraph;
using Plaszczakowo.Problems.FenceTransport.Input;
using Plaszczakowo.Problems.FenceTransport.Output;
using Plaszczakowo.ProblemVisualizer.Commands;

namespace Plaszczakowo.Problems.FenceTransport;

public class ConvexHullResolver : ProblemResolver<FenceTransportInputData, ConvexHullOutput, GraphData>
{
    private int edgeIndex = -1;
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    public override ConvexHullOutput Resolve(FenceTransportInputData data,
        ref ProblemRecreationCommands<GraphData> commands)
    {
        problemRecreationCommands = commands;
        ConvexHullOutput output = new();
        var LowestVertex = FindLowestVertex(data.Vertices);
        var angles = GetAngleOfOtherVertices(LowestVertex, data.Vertices);

        SortByAngle(angles, data.Vertices);

        var indexes = DrawConvexHull(data.Vertices);
        output.HullIndexes = indexes;

        return output;
    }

    private ProblemVertex FindLowestVertex(List<ProblemVertex> vertices)
    {
        var maxY = int.MinValue;
        var lowestVertex = vertices[0];
        foreach (var vertex in vertices)
            if (vertex.Y > maxY)
            {
                maxY = vertex.Y ?? 0;
                lowestVertex = vertex;
            }

        problemRecreationCommands?.Add(new ChangeVertexStateCommand(lowestVertex.Id, GraphStates.Special));
        problemRecreationCommands?.NextStep();
        return lowestVertex;
    }

    private List<float> GetAngleOfOtherVertices(ProblemVertex lowestVertex, List<ProblemVertex> vertices)
    {
        List<float> angles = new();
        foreach (var vertex in vertices)
        {
            var gotAngle = GetAngle(lowestVertex, vertex);
            angles.Add(gotAngle);
        }

        return angles;
    }

    private float GetAngle(ProblemVertex lowestVertex, ProblemVertex vertex)
    {
        if (vertex == lowestVertex) return -1;
        if (lowestVertex.Y == vertex.Y)
        {
            if (lowestVertex.X > vertex.X) return 180;
            return 0;
        }

        float deltaY = vertex.Y - lowestVertex.Y ?? 0;
        float deltaX = vertex.X - lowestVertex.X ?? 0;
        float angle;

        angle = (float)(Math.Atan(1 / (deltaY / deltaX)) * 180 / Math.PI + 90);
        return angle;
    }

    private void SortByAngle(List<float> angles, List<ProblemVertex> vertices)
    {
        for (var i = 0; i < angles.Count; i++)
        for (var j = i + 1; j < angles.Count; j++)
            if (angles[i] > angles[j] || (angles[i] == angles[j] && vertices[i].X > vertices[j].X))
            {
                var temp = angles[i];
                angles[i] = angles[j];
                angles[j] = temp;
                var tempVertex = vertices[i];
                vertices[i] = vertices[j];
                vertices[j] = tempVertex;
            }
    }

    private List<int> DrawConvexHull(List<ProblemVertex> vertices)
    {
        List<int> ConvexHullIndexes = new();
        var ConvexHullStack = new Stack<ProblemVertex>();

        ConvexHullStack.Push(vertices[0]);
        ConvexHullStack.Push(vertices[1]);
        DrawCurrentHull(ConvexHullStack);
        for (var i = 2; i < vertices.Count; i++)
        {
            var Last = ConvexHullStack.Pop();
            var BeforeLast = ConvexHullStack.Peek();

            while (IsClockwise(BeforeLast, Last, vertices[i]))
            {
                Last = ConvexHullStack.Pop();
                BeforeLast = ConvexHullStack.Peek();
            }

            ConvexHullStack.Push(Last);
            ConvexHullStack.Push(vertices[i]);
            DrawCurrentHull(ConvexHullStack);
        }

        foreach (var vertex in ConvexHullStack)
        {
            ConvexHullIndexes.Add(vertex.Id);
            Activate(vertex.Id);
        }

        Connect(ConvexHullIndexes[0], ConvexHullIndexes[ConvexHullIndexes.Count - 1]);
        for (var i = 0; i < ConvexHullIndexes.Count - 1; i++) Connect(ConvexHullIndexes[i], ConvexHullIndexes[i + 1]);

        return ConvexHullIndexes;
    }

    private bool IsClockwise(ProblemVertex BeforePrevious, ProblemVertex Previous, ProblemVertex Current)
    {
        var deltaX1 = Previous.X - BeforePrevious.X;
        var deltaY1 = Previous.Y - BeforePrevious.Y;
        var deltaX2 = Current.X - BeforePrevious.X;
        var deltaY2 = Current.Y - BeforePrevious.Y;

        var result = deltaX1 * deltaY2 - deltaY1 * deltaX2;
        return result > 0;
    }

    private void Highlight(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Highlighted));
    }

    private void Activate(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
    }

    private void Connect(int sourceId, int destinationId)
    {
        problemRecreationCommands?.Add(new ConnectVertexCommand(sourceId, destinationId));
        edgeIndex++;
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(edgeIndex, GraphStates.Active));
    }

    private void HighlightConnect(int sourceId, int destinationId)
    {
        problemRecreationCommands?.Add(new ConnectVertexCommand(sourceId, destinationId));
        edgeIndex++;
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(edgeIndex, GraphStates.Highlighted));
    }

    private void DrawCurrentHull(Stack<ProblemVertex> vertexStack)
    {
        List<int> indices = new();
        foreach (var vertex in vertexStack) indices.Add(vertex.Id);
        ClearAllEdges();
        InactivateAllVertices();
        for (var i = 0; i < indices.Count - 1; i++)
        {
            HighlightConnect(indices[i], indices[i + 1]);
            Highlight(indices[i]);
        }

        Highlight(indices[indices.Count - 1]);
        problemRecreationCommands?.NextStep();
    }

    private void ClearAllEdges()
    {
        for (var i = edgeIndex; i >= 0; i--)
        {
            problemRecreationCommands?.Add(new RemoveEdgeCommand(i));
            edgeIndex--;
        }
    }

    private void InactivateAllVertices()
    {
        problemRecreationCommands?.Add(new ResetGraphStateCommand());
    }
}