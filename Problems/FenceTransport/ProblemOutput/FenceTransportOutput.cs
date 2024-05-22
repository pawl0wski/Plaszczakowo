using ProblemResolver;

namespace Problem.FenceTransport;

public record FenceTransportOutput : ProblemOutput
{
    public int TimeToBuild { get; set; }
}