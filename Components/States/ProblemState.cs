using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public class ProblemState : IProblemState
{
    private readonly ProtectedSessionStorage? _sessionStore;

    public ProblemState(ProtectedSessionStorage sessionStorage)
    {
        _sessionStore = sessionStorage;
    }
    
    public async ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();
        var inputData = await sessionStore.GetAsync<TInputData>("problemInputData");

        if (!inputData.Success || inputData.Value is null)
            throw new Exception("Can't get ProblemInputData. You need to set it first!");

        return inputData.Value;
    }

    public async Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync("problemInputData", inputData);
    }

    public async Task<bool> IsProblemInputDataSet()
    {
        var sessionStore = CheckSessionStoreIfNull();

        var result = await sessionStore.GetAsync<object>("problemInputData");
        return result.Value != null;
    }
    
    private ProtectedBrowserStorage CheckSessionStoreIfNull()
    {
        if (_sessionStore is null)
            throw new NullReferenceException("ProtectedSessionStore is null.");
        return _sessionStore;
    }
}