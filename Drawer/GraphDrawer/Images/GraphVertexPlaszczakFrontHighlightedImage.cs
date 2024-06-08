using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakFrontHighlightedImage : GraphVertexImage
{
    public override bool GetOnVertex()
        => false;
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakFrontHighlighted;
    }
}