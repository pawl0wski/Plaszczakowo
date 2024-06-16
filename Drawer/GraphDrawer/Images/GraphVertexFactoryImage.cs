using Microsoft.AspNetCore.Components;

namespace Plaszczakowo.Drawer.GraphDrawer.Images;

public class GraphVertexFactoryImage : GraphVertexImage
{
    public override bool GetOnVertex()
        => false;

    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.Factory;
    }
    
}