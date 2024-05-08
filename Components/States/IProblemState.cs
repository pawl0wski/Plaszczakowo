using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public interface IProblemState
{
    public ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData;

    public Task SetProblemJsonInputData(string inputData);

    public Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData;

    public ValueTask<TOutputData> GetProblemOutputData<TOutputData>()
    where TOutputData : ProblemOutput;

    public Task SetProblemJsonOutputData(string outputData);

    public Task SetProblemOutputData<TOutputData>(TOutputData outputData)
        where TOutputData : ProblemOutput;
}