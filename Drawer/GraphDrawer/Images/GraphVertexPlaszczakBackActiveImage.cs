using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakBackActiveImage : GraphVertexImage
{
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakBackActive;
    }
}
