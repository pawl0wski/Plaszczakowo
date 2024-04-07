public class FenceTransportResolver : ProblemResolver
{
    public override ProblemResult Resolve(ProblemData data)
    {
        if (data is FenceTransportData)
            return Resolve(data);
        throw new ArgumentException($"{this.GetType().Name} can't resolve {data.GetType().Name}");
    }
    public FenceTransportResult Resolve(FenceTransportData data)
    {
        throw new NotImplementedException();
    }
}