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
    private int _edgeIndex = -1;
    private ProblemRecreationCommands<GraphData>? _problemRecreationCommands;

    public override ConvexHullOutput Resolve(FenceTransportInputData data,
        ref ProblemRecreationCommands<GraphData> commands)
    {
        _problemRecreationCommands = commands;
        ConvexHullOutput output = new();
        var lowestVertex = FindLowestVertex(data.Vertices);
        var angles = GetAngleOfOtherVertices(lowestVertex, data.Vertices);

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

        _problemRecreationCommands?.Add(new ChangeVertexStateCommand(lowestVertex.Id, GraphStates.Special));
        _problemRecreationCommands?.NextStep();
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
            if (angles[i] >= angles[j])
            {
                (angles[i], angles[j]) = (angles[j], angles[i]);
                (vertices[i], vertices[j]) = (vertices[j], vertices[i]);
            }
    }

    private List<int> DrawConvexHull(List<ProblemVertex> vertices)
    {
        List<int> convexHullIndexes = new();
        var convexHullStack = new Stack<ProblemVertex>();

        convexHullStack.Push(vertices[0]);
        convexHullStack.Push(vertices[1]);
        DrawCurrentHull(convexHullStack);
        for (var i = 2; i < vertices.Count; i++)
        {
            var last = convexHullStack.Pop();
            var beforeLast = convexHullStack.Peek();

            while (IsClockwise(beforeLast, last, vertices[i]))
            {
                last = convexHullStack.Pop();
                beforeLast = convexHullStack.Peek();
            }

            convexHullStack.Push(last);
            convexHullStack.Push(vertices[i]);
            DrawCurrentHull(convexHullStack);
        }

        foreach (var vertex in convexHullStack)
        {
            convexHullIndexes.Add(vertex.Id);
            Activate(vertex.Id);
        }

        Connect(convexHullIndexes[0], convexHullIndexes[^1]);
        for (var i = 0; i < convexHullIndexes.Count - 1; i++) Connect(convexHullIndexes[i], convexHullIndexes[i + 1]);

        return convexHullIndexes;
    }

    private bool IsClockwise(ProblemVertex beforePrevious, ProblemVertex previous, ProblemVertex current)
    {
        var deltaX1 = previous.X - beforePrevious.X;
        var deltaY1 = previous.Y - beforePrevious.Y;
        var deltaX2 = current.X - beforePrevious.X;
        var deltaY2 = current.Y - beforePrevious.Y;

        var result = deltaX1 * deltaY2 - deltaY1 * deltaX2;
        return result > 0;
    }

    private void Highlight(int index)
    {
        _problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Highlighted));
    }

    private void Activate(int index)
    {
        _problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
    }

    private void Connect(int sourceId, int destinationId)
    {
        _problemRecreationCommands?.Add(new ConnectVertexCommand(sourceId, destinationId));
        _edgeIndex++;
        _problemRecreationCommands?.Add(new ChangeEdgeStateCommand(_edgeIndex, GraphStates.Active));
    }

    private void HighlightConnect(int sourceId, int destinationId)
    {
        _problemRecreationCommands?.Add(new ConnectVertexCommand(sourceId, destinationId));
        _edgeIndex++;
        _problemRecreationCommands?.Add(new ChangeEdgeStateCommand(_edgeIndex, GraphStates.Highlighted));
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
        _problemRecreationCommands?.NextStep();
    }

    private void ClearAllEdges()
    {
        for (var i = _edgeIndex; i >= 0; i--)
        {
            _problemRecreationCommands?.Add(new RemoveEdgeCommand(i));
            _edgeIndex--;
        }
    }

    private void InactivateAllVertices()
    {
        _problemRecreationCommands?.Add(new ResetGraphStateCommand());
    }
}