using ProblemResolver;

namespace Problem.CarrierAssignment;

public record CarrierAssignmentOutput : ProblemOutput
{
    public List<Pair> Pairs { get; set; } = new();
}