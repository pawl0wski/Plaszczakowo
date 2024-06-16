using Plaszczakowo.Drawer;
using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.ProblemResolver;

/*
    Klasa odpowiedzialna za rozwiązanie problemu.
*/
public abstract class ProblemResolver<TInputData, TOutputData, TDrawerData>
    where TInputData : ProblemInputData
    where TOutputData : ProblemOutput
    where TDrawerData : DrawerData

{
    public abstract TOutputData Resolve(TInputData data, ref ProblemRecreationCommands<TDrawerData> commands);
}