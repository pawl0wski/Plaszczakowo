using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingInputData(string InputPhrase) : ProblemInputData
{
    public string InputPhrase { get; set; } = InputPhrase;
}

