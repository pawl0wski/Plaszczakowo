namespace Problem;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<InputData, OutputData>
    where InputData : ProblemData
    where OutputData : ProblemResult
{
    public abstract OutputData Resolve(InputData data);
}