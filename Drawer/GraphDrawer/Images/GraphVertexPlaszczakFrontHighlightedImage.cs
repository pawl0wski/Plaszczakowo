using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakFrontHighlightedImage : GraphVertexImage
{
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakFrontHighlighted;
    }
}