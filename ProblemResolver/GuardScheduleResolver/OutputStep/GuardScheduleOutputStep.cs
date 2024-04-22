using ProblemResolver.GuardScheduleResolver.InputData;

namespace Problem.GuardSchedule;

public class GuardScheduleOutputStep : ProblemOutputStep
{
    public readonly Plaszczak Plaszczak;

    public GuardScheduleOutputStep(Plaszczak plaszczak)
    {
        this.Plaszczak = plaszczak;
    }
}