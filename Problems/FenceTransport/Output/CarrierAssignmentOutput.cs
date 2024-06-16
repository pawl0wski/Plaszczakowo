namespace Plaszczakowo.Problems.FenceTransport.Output;

public record CarrierAssignmentOutput : ProblemResolver.ProblemOutput
{
    public List<Pair> Pairs { get; set; } = new();
}