namespace Problem;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<InputData, OutputSteps>
    where InputData : ProblemInputData
    where OutputSteps : ProblemResults
{
    public abstract OutputSteps Resolve(InputData data);
}