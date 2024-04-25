namespace ProblemVisualizer;

public class ProblemVisualizerSnapshots<TDrawerData> 
    : List<TDrawerData>
    where TDrawerData : ICloneable
{
    private int _currentSnapshot;

    public TDrawerData Next()
    {
        if (_currentSnapshot < Count - 1)
            _currentSnapshot++;
        return this[_currentSnapshot];
    }

    public TDrawerData Back()
    {
        if (_currentSnapshot > 0)
            _currentSnapshot--;
        return this[_currentSnapshot];
    }

    public TDrawerData GoStart()
    {
        _currentSnapshot = 0;
        return this[_currentSnapshot];
    }

    public TDrawerData GoEnd()
    {
        _currentSnapshot = Count - 1;
        return this[_currentSnapshot];
    }

    public int GetCurrentSnapshot() => _currentSnapshot;
}