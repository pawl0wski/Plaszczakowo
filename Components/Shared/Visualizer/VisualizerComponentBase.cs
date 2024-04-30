using Microsoft.AspNetCore.Components;
using ProblemVisualizer;

namespace ProjektZaliczeniowy_AiSD2.Components.Shared.Visualizer;

public abstract class VisualizerComponentBase<TDrawerData> : ComponentBase
    where TDrawerData : ICloneable
{
    [Parameter] public required ProblemVisualizerSnapshots<TDrawerData> Snapshots { get; set; }

    protected abstract void OnSnaphotChange(TDrawerData newDrawerData);
}