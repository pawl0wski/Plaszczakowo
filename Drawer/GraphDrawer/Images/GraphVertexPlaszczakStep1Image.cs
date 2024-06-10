using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakStep1Image : GraphVertexImage
{
    public override bool GetOnVertex()
        => false;
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakStep1;
    }
}
