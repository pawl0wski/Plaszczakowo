using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public interface IProblemState
{
    public ValueTask<ProblemInputData> GetProblemInputData<TProblemInputData>()
        where TProblemInputData : ProblemInputData;

    public Task SetProblemInputData(ProblemInputData inputData);

    public Task<bool> IsProblemInputDataSet();
}