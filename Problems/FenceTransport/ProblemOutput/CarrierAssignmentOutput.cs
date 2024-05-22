using ProblemResolver;

namespace Problem.FenceTransport;

public record CarrierAssignmentOutput : ProblemOutput
{
    public List<Pair> Pairs { get; set; } = new();
}