using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.States;

public interface IProblemState
{
    public TInputData GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData;

    public string GetProblemJsonInputData();

    public void SetProblemJsonInputData(string inputData);

    public void SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData;

    public TOutputData GetProblemOutputData<TOutputData>()
        where TOutputData : ProblemOutput;

    public void SetProblemJsonOutputData(string outputData);

    public void SetProblemOutputData<TOutputData>(TOutputData outputData)
        where TOutputData : ProblemOutput;
}