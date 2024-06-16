using Microsoft.AspNetCore.Components;
using Plaszczakowo.Drawer;
using Plaszczakowo.ProblemVisualizer;

namespace Plaszczakowo.Components.Shared.Visualizer;

public abstract class VisualizerComponentBase<TDrawerData> : ComponentBase
    where TDrawerData : DrawerData
{
    [Parameter] public required ProblemVisualizerSnapshots<TDrawerData> Snapshots { get; set; }

    protected abstract void OnSnapshotChange(TDrawerData newDrawerData);
}