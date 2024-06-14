using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexRearCarrierInactiveImage : GraphVertexImage
{
    public override bool GetOnVertex()
        => true;

    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.RearCarrierInactive;
    }
    
}