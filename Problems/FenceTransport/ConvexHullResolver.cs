using Blazor.Extensions.Canvas.WebGL;
using Drawer.GraphDrawer;
using Problem.CarrierAssignment;
using Problem.Demo;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.ConvexHull;

public class ConvexHullResolver : ProblemResolver<CarrierAssignmentInputData, ConvexHullOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;

    public override ConvexHullOutput Resolve(CarrierAssignmentInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        ConvexHullOutput output = new();
        var LowestLandmark = FindLowestLandmark(data.Landmarks);
        var angles = GetAngleOfOtherLandmarks(LowestLandmark, data.Landmarks);
        SortByAngle(angles, data.Landmarks);
        var indexes = DrawConvexHull(angles, data.Landmarks);
        output.HullIndexes = indexes;
        return output;
    }
    private ProblemVertex FindLowestLandmark(List<ProblemVertex> vertices)
    {
        int maxValue = int.MinValue;
        ProblemVertex lowestLandmark = vertices[0];
        foreach (ProblemVertex vertex in vertices)
        {
            if (vertex.Y > maxValue)
            {
                maxValue = vertex.Y ?? 0;
                lowestLandmark = vertex;
            }
        }
        return lowestLandmark;
    }
    private List<float> GetAngleOfOtherLandmarks(ProblemVertex lowestLandmark, List<ProblemVertex> vertices)
    {
        List<float> angles = new();
        foreach (ProblemVertex vertex in vertices)
        {
                angles.Add(GetAngle(lowestLandmark, vertex));
        }
        return angles;
    }
    private float GetAngle(ProblemVertex lowestLandmark, ProblemVertex vertex)
    {
        float angle = (float)Math.Atan2(vertex.Y - lowestLandmark.Y ?? 0, vertex.X - lowestLandmark.X ?? 0);
        return angle;
    }
    private void SortByAngle(List<float> angles, List<ProblemVertex> vertices)
    {
        for (int i = 0; i < angles.Count; i++)
        {
            for (int j = i + 1; j < angles.Count; j++)
            {
                if (angles[i] > angles[j])
                {
                    float temp = angles[i];
                    angles[i] = angles[j];
                    angles[j] = temp;
                    ProblemVertex tempVertex = vertices[i];
                    vertices[i] = vertices[j];
                    vertices[j] = tempVertex;
                }
            }
        }
    }
    private List<int> DrawConvexHull(List<float> angles, List<ProblemVertex> vertices)
    {
        List<int> ConvexHullIndexes = new();
        var ConvexHullStack = new Stack<ProblemVertex>();
        ConvexHullStack.Push(vertices[0]);
        ConvexHullStack.Push(vertices[1]);
        for (int i = 2; i < vertices.Count; i++)
        {
            var StackTop = ConvexHullStack.Pop();
            var StackAfterTop = ConvexHullStack.Peek();
            while (IsClockwise(StackAfterTop, StackTop, vertices[i]))
            {
                StackTop = ConvexHullStack.Pop();
                StackAfterTop = ConvexHullStack.Peek();
            }
            ConvexHullStack.Push(StackTop);
            ConvexHullStack.Push(vertices[i]);
        }
        foreach (ProblemVertex vertex in ConvexHullStack)
        {
            ConvexHullIndexes.Add(vertex.Id);
            problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertex.Id, GraphStates.Special));
        }
        return ConvexHullIndexes;
    }
    private bool IsClockwise(ProblemVertex a, ProblemVertex b, ProblemVertex c)
    {
        //zmienic nazwy zmiennych
        return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X) < 0;
    }
}