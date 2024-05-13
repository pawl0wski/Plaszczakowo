using Blazor.Extensions.Canvas.WebGL;
using Drawer.GraphDrawer;
using ProblemResolver;
using ProblemResolver.Graph;

public static class GraphInputValidator
{
    [Flags]
    public enum Modes
    {
        HaveLoop = 0,
        OneEdgeFromEveryVertex = 1,
        EverythingConnected = 2,
    }

    public static List<GraphValidatorError> Validate(
        Modes modes,
        ProblemGraphInputData graphInputData)
    {
        List<GraphValidatorError> errors = [];
        
        if (modes.HasFlag(Modes.HaveLoop))
        {
            if (!CheckLoop(graphInputData))
                errors.Add(new GraphValidatorError("Graf musi posiadać cykl."));
        }

        if (modes.HasFlag(Modes.OneEdgeFromEveryVertex))
        {
            if (!CheckOneEdgeFromEveryVertex(graphInputData))
            {
                errors.Add(new GraphValidatorError("Każdy wierzchołek powinien mieć tylko jedną krawędź incydentną."));
            }
        }

        if (modes.HasFlag(Modes.EverythingConnected))
        {
            if (!CheckEverythingConnected(graphInputData))
            {
                errors.Add(new GraphValidatorError("Wszystkie wierzchołki muszą być połączone ze sobą!"));
            }
        }

        return errors;
    }

    private static bool CheckLoop(in ProblemGraphInputData inputData)
    {
        return Dfs(inputData, (vertex, list) => list.Contains(vertex));
    }

    private static bool CheckOneEdgeFromEveryVertex(in ProblemGraphInputData inputData)
    {
        foreach (var vertex in inputData.Vertices)
        {
            if (inputData.Edges.Count(edge => edge.From == vertex.Id) > 1)
                return false;
        }

        return true;
    }

    private static bool CheckEverythingConnected(in ProblemGraphInputData inputData)
    {
        var howManyVertices = inputData.Vertices.Count;
        return Dfs(inputData, null, (list => list.Count == howManyVertices), false);
    }

    private static bool Dfs(in ProblemGraphInputData inputData, 
        Func<ProblemVertex, List<ProblemVertex>, bool>? everyStepCheck = null,
        Func<List<ProblemVertex>, bool>? afterDfsCheck = null,
        bool directedGraph = true
        )
    {
        Stack<ProblemVertex> verticesToVisit = [];
        List<ProblemVertex> visitedVertices = [];
        
        var firstVertex = inputData.Vertices.First();
        AddToStackVerticesConnectedToVertex(firstVertex.Id, ref verticesToVisit, inputData, directedGraph);
        
        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Pop();

            var shouldStop = everyStepCheck?.Invoke(vertex, visitedVertices);
            if (shouldStop ?? false)
                return true;
            
            if (visitedVertices.Contains(vertex))
                continue;
                
            visitedVertices.Add(vertex);
            
            AddToStackVerticesConnectedToVertex(vertex.Id, ref verticesToVisit, inputData, directedGraph);
        }

        return afterDfsCheck?.Invoke(visitedVertices) ?? false;
    }

    private static void AddToStackVerticesConnectedToVertex(
        int vertexId,
        ref Stack<ProblemVertex> stack,
        in ProblemGraphInputData inputData,
        bool directedGraph = true
        )
    {
        Predicate<ProblemEdge> edgeChecker;
        Func<ProblemVertex, ProblemEdge, bool> vertexChecker;
        if (directedGraph)
        {
            edgeChecker = (e) => e.From == vertexId;
            vertexChecker = (v, e) => e.To == v.Id;
        }
        else
        {
            edgeChecker = (e) => e.From == vertexId || e.To == vertexId;
            vertexChecker = (v, e) => e.To == v.Id || e.From == v.Id;
        }
        
        var edges = inputData.Edges.FindAll(edgeChecker).ToList();
        foreach (var edge in edges)
        {
            foreach (var vertex in inputData.Vertices.FindAll((v) => vertexChecker(v, edge)))
                stack.Push(vertex);
        }
    }
    

}

public record GraphValidatorError(string Content)
{
    public readonly string Content = Content;
}