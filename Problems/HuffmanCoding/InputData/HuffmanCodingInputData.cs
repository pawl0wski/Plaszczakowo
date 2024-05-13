using ProblemResolver;
namespace Problem.HuffmanCoding;

public record HuffmanCodingInputData : ProblemInputData
{
    public string InputPhrase { get; set; }

    public HuffmanCodingInputData(string inputPhrase)
    {
        InputPhrase = inputPhrase;
    }
}

