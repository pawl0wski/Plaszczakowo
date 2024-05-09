using ProblemResolver;
using ProblemResolver.Graph;

namespace Problem.CarrierAssignment;

public record CarrierAssignmentInputData(int FrontCarrierNumber, int RearCarrierNumber, List<Edge> Relations, 
    List<ProblemEdge> Paths, List<ProblemVertex> Landmarks, int FactoryIndex) : ProblemGraphInputData(Landmarks, Paths)
{
    public int FrontCarrierNumber { get; set; } = FrontCarrierNumber;
    public int RearCarrierNumber { get; set; } = RearCarrierNumber;
    public List<Edge> Relations { get; set; } = Relations;
    
    public List<ProblemEdge> Paths { get; set; } = Paths;
    public List<ProblemVertex> Landmarks { get; set; } = Landmarks;
    public int FactoryIndex { get; set; } = FactoryIndex;
}