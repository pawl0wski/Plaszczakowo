using System.Text.Json;
using Plaszczakowo.ProblemResolver;
using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.States;

public class ProblemState : IProblemState
{
    private string? _problemInputData = null;

    private string? _problemOutputData = null;

    public void SetProblemInputData<TInputData>(TInputData inputData)
        where TInputData : ProblemInputData
    {
        _problemInputData = JsonSerializer.Serialize(inputData);
    }

    public string GetProblemJsonInputData()
    {
        ArgumentNullException.ThrowIfNull(_problemInputData);
        return _problemInputData;
    }

    public void SetProblemJsonInputData(string inputData)
    {
        _problemInputData = inputData;
    }

    public TInputData GetProblemInputData<TInputData>()
        where TInputData : ProblemInputData
    {
        return DeserializeData<TInputData>(_problemInputData!);
    }

    public void SetProblemOutputData<TOutputData>(TOutputData outputData) where TOutputData : ProblemOutput
    {
        _problemOutputData = JsonSerializer.Serialize(outputData);
    }

    public void SetProblemJsonOutputData(string outputData)
    {
        _problemOutputData = outputData;
    }

    public TOutputData GetProblemOutputData<TOutputData>() where TOutputData : ProblemOutput
    {
        return DeserializeData<TOutputData>(_problemOutputData!);
    }

    private static TData DeserializeData<TData>(string serializedData)
    {
        var deserializedData = JsonSerializer.Deserialize<TData>(serializedData);
        if (deserializedData is null)
            throw new Exception("Can't deserialize InputData");

        return deserializedData;
    }
}