using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexFrontCarrierActiveImage : GraphVertexImage
{
    public override bool GetOnVertex()
        => true;

    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.FrontCarrierActive;
    }
    
}