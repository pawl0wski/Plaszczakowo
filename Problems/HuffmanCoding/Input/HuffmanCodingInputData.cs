using Plaszczakowo.ProblemResolver.ProblemInput;

namespace Plaszczakowo.Problems.HuffmanCoding.Input;

public record HuffmanCodingInputData(string InputPhrase) : ProblemInputData
{
    public string InputPhrase { get; set; } = InputPhrase;
}

