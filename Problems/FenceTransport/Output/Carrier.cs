using Plaszczakowo.ProblemResolver.ProblemGraph;

namespace Plaszczakowo.Problems.FenceTransport.Output;

public class Carrier (int Id, ProblemVertex Position) {
    public int Id { get; set; } = Id;
    public ProblemVertex Position { get; set; } = Position;
    public Queue<ProblemVertex> CurrentRoute { get; set;} = [];

    public ProblemEdge? EdgeToBuild { get; set; }
    public CarrierState State = CarrierState.Unassigned;
    public int Load { get; set; } = 100;

    public void MoveTo(ProblemVertex newPosition)
    {
        Position = newPosition;
    }
    public void Refill()
    {
        Load = 100;
    }
    public void Deliver()
    {
        var needed = EdgeToBuild?.Throughput?.Capacity -  EdgeToBuild?.Throughput?.Flow;
        if (needed > Load)
        {
            EdgeToBuild!.Throughput!.Flow += Load;
            Load = 0;
        }
        else
        {
            EdgeToBuild!.Throughput!.Flow += needed ?? 0;
            Load -= needed ?? 0;
        }
    }
}