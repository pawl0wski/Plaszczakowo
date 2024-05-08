using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.Components.States;

public class ProblemState : IProblemState
{
    private const string InputDataKey = "problemInputData";
    private const string OutputDataKey = "problemOutputData";

    private readonly ProtectedSessionStorage? _sessionStore;

    public ProblemState(ProtectedSessionStorage sessionStorage)
    {
        _sessionStore = sessionStorage;
    }

    public async ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();
        var inputData = await sessionStore.GetAsync<string>(InputDataKey);
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

        await sessionStore.SetAsync(InputDataKey, inputData);
    }

    public async Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(InputDataKey, JsonSerializer.Serialize(inputData));
    }

    public async ValueTask<TOutputData> GetProblemOutputData<TOutputData>() where TOutputData : ProblemOutput
    {
        var sessionStore = CheckSessionStoreIfNull();
        var outputData = await sessionStore.GetAsync<string>(OutputDataKey);
        if (!outputData.Success || outputData.Value is null)
            throw new Exception("Can't get JSON inputData. You need to set it first!");

        var deserializedOutputData = JsonSerializer.Deserialize<TOutputData>(outputData.Value);
        if (deserializedOutputData is null)
            throw new Exception("Can't deserialize InputData");

        return deserializedOutputData;
    }

    public async Task SetProblemJsonOutputData(string outputData)
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(OutputDataKey, outputData);
    }

    public async Task SetProblemOutputData<TOutputData>(TOutputData outputData) where TOutputData : ProblemOutput
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(OutputDataKey, JsonSerializer.Serialize(outputData));
    }

    private ProtectedBrowserStorage CheckSessionStoreIfNull()
    {
        if (_sessionStore is null)
            throw new NullReferenceException("ProtectedSessionStore is null.");
        return _sessionStore;
    }
}