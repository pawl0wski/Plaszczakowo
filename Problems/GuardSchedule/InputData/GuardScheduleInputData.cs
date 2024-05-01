using ProblemResolver;

namespace Problem.GuardSchedule;

public record GuardScheduleInputData(List<Plaszczak> Plaszczaki, Pathway Pathway) : ProblemInputData
{
    public List<Plaszczak> Plaszczaki { get; set; } = Plaszczaki;

    public Pathway Pathway { get; set; } = Pathway;
}

