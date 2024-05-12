using Blazor.Extensions.Canvas.WebGL;
using Drawer.GraphDrawer;
using Microsoft.VisualBasic;
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
        problemRecreationCommands = commands;
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
        int maxY = int.MinValue;
        ProblemVertex lowestLandmark = vertices[0];
        foreach (ProblemVertex vertex in vertices)
        {
            if (vertex.Y > maxY)
            {
                maxY = vertex.Y ?? 0;
                lowestLandmark = vertex;
            }
        }
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(lowestLandmark.Id, GraphStates.Special));
        problemRecreationCommands?.NextStep();
        return lowestLandmark;
    }
    private List<float> GetAngleOfOtherLandmarks(ProblemVertex lowestLandmark, List<ProblemVertex> vertices)
    {
        List<float> angles = new();
        foreach (ProblemVertex vertex in vertices)
        {
            float gotAngle = GetAngle(lowestLandmark, vertex);
            if (gotAngle != -1)
                angles.Add(gotAngle);
        }
        return angles;
    }
    private float GetAngle(ProblemVertex lowestLandmark, ProblemVertex vertex)
    {
        if (vertex == lowestLandmark)
        {
            return -1;
        }
        float deltaY = vertex.Y - lowestLandmark.Y ?? 0;
        float deltaX = vertex.X - lowestLandmark.X ?? 0;
        float angle;
    
        angle = (float)((Math.Atan(1 / (deltaY / deltaX)) * 180 / Math.PI) + 90);
        return angle;
    }
    private void SortByAngle(List<float> angles, List<ProblemVertex> vertices)
    {
        for (int i = 0; i < angles.Count; i++)
        {
            for (int j = i + 1; j < angles.Count; j++)
            {
                if (angles[i] >= angles[j])
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
            problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertex.Id, GraphStates.Active));
            problemRecreationCommands?.NextStep();
        }
        return ConvexHullIndexes;
    }
    private bool IsClockwise(ProblemVertex AfterNext, ProblemVertex Next, ProblemVertex Current)
    {
        return (Next.X - AfterNext.X) * (Current.Y - AfterNext.Y) - (Next.Y - AfterNext.Y) * (Current.X - AfterNext.X) < 0;
    }
}