using ProblemResolver;

namespace Problem.CarrierAssignment;

public record CarrierAssignmentInputData(int FrontCarrierNumber, int RearCarrierNumber, List<Edge> Relations) : ProblemInputData
{
    public int FrontCarrierNumber { get; set; } = FrontCarrierNumber;
    public int RearCarrierNumber { get; set; } = RearCarrierNumber;
    public List<Edge> Relations { get; set; } = Relations;
}