using ProblemResolver;
using ProblemResolver.Graph;

namespace Problem.GuardSchedule;

public record GuardScheduleInputData(
    List<Plaszczak> Plaszczaki,
    List<ProblemVertex> Vertices,
    List<ProblemEdge> Edges,
    int MaxPossibleSteps)
    : ProblemGraphInputData(Vertices, Edges)
{
    public List<Plaszczak> Plaszczaki { get; set; } = Plaszczaki;

    public int MaxPossibleSteps { get; set; } = MaxPossibleSteps;
}