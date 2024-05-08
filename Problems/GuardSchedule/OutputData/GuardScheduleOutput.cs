using ProblemResolver;

namespace Problem.GuardSchedule;

public record GuardScheduleOutput : ProblemOutput
{
    public List<Plaszczak> Plaszczaki { get; set; } = [];
}

