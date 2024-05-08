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

    public async Task SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(InputDataKey, JsonSerializer.Serialize(inputData));
    }

    public async Task SetProblemJsonInputData(string inputData)
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(InputDataKey, inputData);
    }

    public async ValueTask<TInputData> GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        var serializedInputData = await GetDataFromStorage(InputDataKey);
        return DeserializeData<TInputData>(serializedInputData);
    }

    public async Task SetProblemOutputData<TOutputData>(TOutputData outputData) where TOutputData : ProblemOutput
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(OutputDataKey, JsonSerializer.Serialize(outputData));
    }

    public async Task SetProblemJsonOutputData(string outputData)
    {
        var sessionStore = CheckSessionStoreIfNull();

        await sessionStore.SetAsync(OutputDataKey, outputData);
    }

    public async ValueTask<TOutputData> GetProblemOutputData<TOutputData>() where TOutputData : ProblemOutput
    {
        var serializedOutputData = await GetDataFromStorage(OutputDataKey);
        return DeserializeData<TOutputData>(serializedOutputData);
    }

    private async ValueTask<string> GetDataFromStorage(string storageKey)
    {
        var sessionStore = CheckSessionStoreIfNull();

        var outputData = await sessionStore.GetAsync<string>(storageKey);
        if (!outputData.Success || outputData.Value is null)
            throw new Exception("Can't get JSON inputData. You need to set it first!");

        return outputData.Value;
    }

    private TData DeserializeData<TData>(string serializedData)
    {
        var deserializedData = JsonSerializer.Deserialize<TData>(serializedData);
        if (deserializedData is null)
            throw new Exception("Can't deserialize InputData");

        return deserializedData;
    }

    private ProtectedBrowserStorage CheckSessionStoreIfNull()
    {
        if (_sessionStore is null)
            throw new NullReferenceException("ProtectedSessionStore is null.");
        return _sessionStore;
    }
}