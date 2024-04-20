namespace Problem;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<InputData, OutputSteps>
    where InputData : ProblemInputData
    where OutputSteps : ProblemOutputSteps
{
    public abstract List<OutputSteps> Resolve(InputData data);
}