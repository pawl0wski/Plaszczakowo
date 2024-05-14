using ProblemResolver;
using ProblemResolver.Graph;

namespace ProjektZaliczeniowy_AiSD2.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

public static class GraphInputValidator
{


    public static List<GraphValidatorError> Validate(
        GraphInputValidatorModes modes,
        ProblemGraphInputData graphInputData,
        bool isDirected)
    {
        List<GraphValidatorError> errors = [];

        if (graphInputData.Vertices.Count == 0)
        {
            errors.Add(new GraphValidatorError("Musisz podać przyjamniej jeden wierzchołek."));
            return errors;
        }

        if (modes.HasFlag(GraphInputValidatorModes.ShouldHave3Vertices))
        {
            if (!Check3Vertices(graphInputData))
                errors.Add(new GraphValidatorError("Graf musi posiadać conajmniej 3 wierzchołki."));
        }
        
        if (modes.HasFlag(GraphInputValidatorModes.HaveLoop))
        {
            if (!CheckLoop(graphInputData, isDirected))
                errors.Add(new GraphValidatorError("Graf musi posiadać cykl."));
        }

        if (modes.HasFlag(GraphInputValidatorModes.OneEdgeFromEveryVertex))
        {
            if (!CheckOneEdgeFromEveryVertex(graphInputData))
            {
                errors.Add(new GraphValidatorError("Każdy wierzchołek powinien mieć tylko jedną krawędź incydentną."));
            }
        }

        if (modes.HasFlag(GraphInputValidatorModes.EverythingConnected))
        {
            if (!CheckEverythingConnected(graphInputData, isDirected))
            {
                errors.Add(new GraphValidatorError("Wszystkie wierzchołki muszą być połączone ze sobą."));
            }
        }

        return errors;
    }

    private static bool Check3Vertices(ProblemGraphInputData graphInputData)
    {
        return graphInputData.Vertices.Count >= 3;
    }

    private static bool CheckLoop(in ProblemGraphInputData inputData, bool isDirected)
    {
        return Dfs(inputData, (vertex, list) => list.Contains(vertex), null, isDirected);
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

    private static bool CheckEverythingConnected(in ProblemGraphInputData inputData, bool isDirected)
    {
        var howManyVertices = inputData.Vertices.Count;
        return Dfs(inputData, null, (list => list.Count == howManyVertices), isDirected);
    }

    private static bool Dfs(in ProblemGraphInputData inputData, 
        Func<ProblemVertex, List<ProblemVertex>, bool>? everyStepCheck = null,
        Func<List<ProblemVertex>, bool>? afterDfsCheck = null,
        bool isDirected = true
        )
    {
        Stack<ProblemVertex> verticesToVisit = [];
        List<ProblemVertex> visitedVertices = [];
        
        var firstVertex = inputData.Vertices.First();
        AddToStackVerticesConnectedToVertex(firstVertex.Id, ref verticesToVisit, inputData, isDirected);
        
        while (verticesToVisit.Count > 0)
        {
            var vertex = verticesToVisit.Pop();

            var shouldStop = everyStepCheck?.Invoke(vertex, visitedVertices);
            if (shouldStop ?? false)
                return true;
            
            if (visitedVertices.Contains(vertex))
                continue;
                
            visitedVertices.Add(vertex);
            
            AddToStackVerticesConnectedToVertex(vertex.Id, ref verticesToVisit, inputData, isDirected);
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