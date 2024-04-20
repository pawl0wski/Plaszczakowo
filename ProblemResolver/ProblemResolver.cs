namespace Problem;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<InputData, OutputData>
    where InputData : ProblemInputData
    where OutputData : ProblemOutputSteps
{
    public abstract List<OutputData> Resolve(InputData data);
}