using ProblemResolver;
using ProblemResolver.Graph;

namespace Problem.CarrierAssignment;

public record CarrierAssignmentInputData(int FrontCarrierNumber, int RearCarrierNumber, List<Edge> Relations, 
    List<ProblemEdge> Roads, List<ProblemVertex> Nodes, int FactoryIndex) : ProblemInputData
{
    public int FrontCarrierNumber { get; set; } = FrontCarrierNumber;
    public int RearCarrierNumber { get; set; } = RearCarrierNumber;
    public List<Edge> Relations { get; set; } = Relations;
    
    public List<ProblemEdge> Roads { get; set; } = Roads;
    public List<ProblemVertex> Nodes { get; set; } = Nodes;
    public int FactoryIndex { get; set; } = FactoryIndex;
}