using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public interface IProblemState
{
    public ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData;

    public Task SetProblemJsonInputData(string inputData);
    public Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData;
}