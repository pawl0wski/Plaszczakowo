namespace ProblemResolver;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<TInputData, TOutputData ,TDrawerData>
    where TInputData : ProblemInputData
    where TOutputData : ProblemOutput
    where TDrawerData : ICloneable
    
{
    public abstract ProblemOutput Resolve(TInputData data, ref ProblemRecreationCommands<TDrawerData> commands);
}