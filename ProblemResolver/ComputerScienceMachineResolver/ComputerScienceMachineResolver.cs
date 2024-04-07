public class ComputerScienceMachineResolver : ProblemResolver
{
    public override ProblemResolverResult Resolve(ProblemResolverData data)
    {
        if (data is ComputerScienceMachineData)
            return Resolve(data);
        throw new ArgumentException($"{this.GetType().Name} can't resolve {data.GetType().Name}");
    }
    public ComputerScienceMachineResult Resolve(ComputerScienceMachineData data)
    {
        throw new NotImplementedException();
    }
}