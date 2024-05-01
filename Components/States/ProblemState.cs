using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public class ProblemState : IProblemState
{
    private const string DataKey = "problemInputData";
    private readonly ProtectedSessionStorage? _sessionStore;

    public ProblemState(ProtectedSessionStorage sessionStorage)
    {
        _sessionStore = sessionStorage;
    }

    public async ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();
        var inputData = await sessionStore.GetAsync<string>(DataKey);
        if (!inputData.Success || inputData.Value is null)
            throw new Exception("Can't get JSON inputData. You need to set it first!");

        var deserializedInputData = JsonSerializer.Deserialize<TInputData>(inputData.Value);
        if (deserializedInputData is null)
            throw new Exception("Can't deserialize InputData");

        return deserializedInputData;
    }

    public async Task SetProblemJsonInputData(string inputData)
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(DataKey, inputData);
    }

    public async Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(DataKey, JsonSerializer.Serialize(inputData));
    }

    private ProtectedBrowserStorage CheckSessionStoreIfNull()
    {
        if (_sessionStore is null)
            throw new NullReferenceException("ProtectedSessionStore is null.");
        return _sessionStore;
    }
}