using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakSleepingImage : GraphVertexImage
{
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakSleeping;
    }
}