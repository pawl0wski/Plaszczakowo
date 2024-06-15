using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProblemResolver;

namespace ProjektZaliczeniowy_AiSD2.States;

public class ProblemState : IProblemState
{
    private string? _problemData = null;

    private string? _outputData = null;

    public void SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        _problemData = JsonSerializer.Serialize(inputData);
    }

    public void SetProblemJsonInputData(string inputData)
    {
        _problemData = inputData;
    }

    public TInputData GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        return DeserializeData<TInputData>(_problemData!);
    }

    public void SetProblemOutputData<TOutputData>(TOutputData outputData) where TOutputData : ProblemOutput
    {
        _outputData = JsonSerializer.Serialize(outputData);
    }

    public void SetProblemJsonOutputData(string outputData)
    {
        _outputData = outputData;
    }

    public TOutputData GetProblemOutputData<TOutputData>() where TOutputData : ProblemOutput
    {
        return DeserializeData<TOutputData>(_outputData!);
    }

    private TData DeserializeData<TData>(string serializedData)
    {
        var deserializedData = JsonSerializer.Deserialize<TData>(serializedData);
        if (deserializedData is null)
            throw new Exception("Can't deserialize InputData");

        return deserializedData;
    }

}