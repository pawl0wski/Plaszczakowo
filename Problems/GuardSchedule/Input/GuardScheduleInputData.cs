using Plaszczakowo.ProblemResolver.ProblemGraph;
using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.Problems.GuardSchedule.Input;

public record GuardScheduleInputData(
    List<ProblemVertex> Vertices,
    List<ProblemEdge> Edges,
    List<Plaszczak> Plaszczaki,
    int MaxPossibleSteps)
    : ProblemGraphInputData(Vertices, Edges)
{
    public List<Plaszczak> Plaszczaki { get; set; } = Plaszczaki;

    public int MaxPossibleSteps { get; set; } = MaxPossibleSteps;
}