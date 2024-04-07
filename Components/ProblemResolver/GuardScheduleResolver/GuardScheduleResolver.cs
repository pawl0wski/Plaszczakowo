public class GuardScheduleResolver : ProblemResolver
{
    public override ProblemResult Resolve(ProblemData data)
    {
        if (data is GuardScheduleData)
            return Resolve(data);
        throw new ArgumentException($"{this.GetType().Name} can't resolve {data.GetType().Name}");
    }
    public GuardScheduleResult Resolve(GuardScheduleData data)
    {
        throw new NotImplementedException();
    }
}