using ProblemResolver;

namespace Problem.FenceTransport;

public record ConvexHullOutput : ProblemOutput
{
    public List<int>? HullIndexes { get; set; } = new();
}

