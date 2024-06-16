using Plaszczakowo.ProblemResolver;
using Plaszczakowo.Problems.GuardSchedule.Input;

namespace Plaszczakowo.Problems.GuardSchedule.Output;

public record GuardScheduleOutput : ProblemOutput
{
    public List<Plaszczak> Plaszczaki { get; set; } = [];
}