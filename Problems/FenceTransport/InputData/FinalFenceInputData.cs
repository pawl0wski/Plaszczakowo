using ProblemResolver;

namespace Problem.FenceTransport;

public record FinalFenceInputData(FenceTransportInputData InputData, CarrierAssignmentOutput CarrierAssignmentOutput, 
    ConvexHullOutput ConvexHullOutput) : ProblemInputData
{
    public FenceTransportInputData InputData { get; set; } = InputData;
    public CarrierAssignmentOutput CarrierAssignmentOutput { get; set; } = CarrierAssignmentOutput;
    public ConvexHullOutput ConvexHullOutput { get; set; } = ConvexHullOutput;
}