using ProblemResolver;

namespace ProblemVisualizer;

public abstract class FirstSnapshotCreator<TProblemInputData, TDrawerData>
    where TDrawerData : ICloneable
    where TProblemInputData : ProblemInputData
{
    public FirstSnapshotCreator()
    {
        
    }
    public abstract TDrawerData CreateFirstSnapshot(TProblemInputData inputData);
}