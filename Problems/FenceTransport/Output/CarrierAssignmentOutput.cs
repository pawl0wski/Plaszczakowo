using Plaszczakowo.ProblemResolver;

namespace Plaszczakowo.Problems.FenceTransport.Output;

public record CarrierAssignmentOutput : ProblemOutput
{
    public List<Pair> Pairs { get; set; } = new();
}