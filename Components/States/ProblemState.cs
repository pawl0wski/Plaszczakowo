using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public class ProblemState : IProblemState
{
    [Inject] 
    private ProtectedSessionStorage? ProtectedSessionStore { get; set; }

    public async ValueTask<ProblemInputData> GetProblemInputData<TProblemInputData>() where TProblemInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();
        var inputData = await sessionStore.GetAsync<TProblemInputData>("problemInputData");

        if (!inputData.Success || inputData.Value is null)
            throw new Exception("Can't get ProblemInputData. You need to set it first!");

        return inputData.Value;
    }

    public async Task SetProblemInputData(ProblemInputData inputData)
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
        if (ProtectedSessionStore is null)
            throw new NullReferenceException("ProtectedSessionStore is null.");
        return ProtectedSessionStore;
    }
}