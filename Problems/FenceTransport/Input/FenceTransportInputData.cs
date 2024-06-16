using Plaszczakowo.ProblemResolver.ProblemGraph;
using Plaszczakowo.ProblemResolver.ProblemInput;
using Plaszczakowo.Problems.FenceTransport.Output;

namespace Plaszczakowo.Problems.FenceTransport.Input;

public record FenceTransportInputData(
    int FrontCarrierNumber,
    int RearCarrierNumber,
    List<Edge> Relations,
    List<ProblemVertex> Vertices,
    List<ProblemEdge> Edges,
    int FactoryIndex) : ProblemGraphInputData(Vertices, Edges)
{
    public int FrontCarrierNumber { get; set; } = FrontCarrierNumber;
    public int RearCarrierNumber { get; set; } = RearCarrierNumber;
    public List<Edge> Relations { get; set; } = Relations;

    public int FactoryIndex { get; set; } = FactoryIndex;
    public CarrierAssignmentOutput? CarrierAssignmentOutput { get; set; }
    public ConvexHullOutput? ConvexHullOutput { get; set; }
}