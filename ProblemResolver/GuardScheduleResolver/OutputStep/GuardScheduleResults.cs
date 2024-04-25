
namespace Problem.GuardSchedule;

public class GuardScheduleResults : ProblemResults
{
    public readonly Plaszczak Plaszczak;

    public GuardScheduleResults(Plaszczak plaszczak)
    {
        this.Plaszczak = plaszczak;
    }
}