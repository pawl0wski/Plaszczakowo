using Drawer.GraphDrawer;
using ProblemResolver.Converters;
using ProblemVisualizer;

namespace Problem.HuffmanCoding;


public class FirstHuffmanCodingSnapshotCreator(HuffmanCodingInputData inputData)
    : FirstSnapshotCreator<HuffmanCodingInputData, GraphData>(inputData)
{
    public override GraphData CreateFirstSnapshot()
    {
        var outputData = new GraphData();
        outputData.Texts.Add(new GraphText("", 0, 0, GraphStates.Active));
        return outputData;
    }
}