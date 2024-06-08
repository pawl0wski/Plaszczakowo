using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakFrontInactiveImage : GraphVertexImage
{
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakFrontInactive;
    }
}