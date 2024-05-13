using Blazor.Extensions.Canvas.WebGL;
using Drawer.GraphDrawer;
using Microsoft.VisualBasic;
using Problem.CarrierAssignment;
using Problem.Demo;
using ProblemResolver;
using ProblemResolver.Graph;
using ProblemVisualizer.Commands;

namespace Problem.ConvexHull;

public class ConvexHullResolver : ProblemResolver<FenceTransportInputData, ConvexHullOutput, GraphData>
{
    private ProblemRecreationCommands<GraphData>? problemRecreationCommands;
    private int edgeIndex = -1;

    public override ConvexHullOutput Resolve(FenceTransportInputData data, ref ProblemRecreationCommands<GraphData> commands)
    {
        problemRecreationCommands = commands;
        ConvexHullOutput output = new();
        var LowestLandmark = FindLowestLandmark(data.Landmarks);
        var angles = GetAngleOfOtherLandmarks(LowestLandmark, data.Landmarks);

        SortByAngle(angles, data.Landmarks);

        var indexes = DrawConvexHull(data.Landmarks);
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
        if (lowestLandmark.Y == vertex.Y)
        {
            if (lowestLandmark.X > vertex.X)
            {
                return 180;
            }
            return 0;
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
    private List<int> DrawConvexHull(List<ProblemVertex> vertices)
    {
        List<int> ConvexHullIndexes = new();
        var ConvexHullStack = new Stack<ProblemVertex>();

        Activate(vertices[0].Id);
        Activate(vertices[1].Id);

        ConvexHullStack.Push(vertices[0]);
        ConvexHullStack.Push(vertices[1]);
        for (int i = 2; i < vertices.Count; i++)
        {
            problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertices[i].Id, GraphStates.Highlighted));
            problemRecreationCommands?.NextStep();
            var Last = ConvexHullStack.Pop();
            var BeforeLast = ConvexHullStack.Peek();
            while (IsClockwise(BeforeLast, Last, vertices[i]))
            {
                Inactivate(Last.Id);

                Last = ConvexHullStack.Pop();
                BeforeLast = ConvexHullStack.Peek();

                Highlight(Last.Id);
            }
            ConvexHullStack.Push(Last);
            ConvexHullStack.Push(vertices[i]);
            problemRecreationCommands?.Add(new ChangeVertexStateCommand(Last.Id, GraphStates.Active));
            problemRecreationCommands?.Add(new ChangeVertexStateCommand(vertices[i].Id, GraphStates.Highlighted));
            
            if (i == vertices.Count - 1)
            {
                foreach (ProblemVertex vertex in ConvexHullStack.Reverse())
                {
                    Activate(vertex.Id);
                }
                
            }
        }
        foreach (ProblemVertex vertex in ConvexHullStack)
        {
            ConvexHullIndexes.Add(vertex.Id);
        }
        Connect(ConvexHullIndexes[0], ConvexHullIndexes[ConvexHullIndexes.Count - 1]);
        for (int i = 0; i < ConvexHullIndexes.Count - 1; i++)
        {
            Connect(ConvexHullIndexes[i], ConvexHullIndexes[i + 1]);
        }

        return ConvexHullIndexes;
    }
    private bool IsClockwise(ProblemVertex BeforePrevious, ProblemVertex Previous, ProblemVertex Current)
    {
        int? deltaX1 = Previous.X - BeforePrevious.X;
        int? deltaY1 = Previous.Y - BeforePrevious.Y;
        int? deltaX2 = Current.X - BeforePrevious.X;
        int? deltaY2 = Current.Y - BeforePrevious.Y;

        int? result = deltaX1 * deltaY2 - deltaY1 * deltaX2;
        return result > 0;
    }
    private void Highlight(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Highlighted));
        problemRecreationCommands?.NextStep();
    }
    private void Activate(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Active));
        problemRecreationCommands?.NextStep();
    }
    private void Inactivate(int index)
    {
        problemRecreationCommands?.Add(new ChangeVertexStateCommand(index, GraphStates.Inactive));
        problemRecreationCommands?.NextStep();
    }
    private void Connect(int sourceId, int destinationId)
    {
        problemRecreationCommands?.Add(new ConnectVertexCommand(sourceId, destinationId));
        edgeIndex++;
        problemRecreationCommands?.Add(new ChangeEdgeStateCommand(edgeIndex, GraphStates.Active));
        problemRecreationCommands?.NextStep();

    }
}