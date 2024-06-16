using Microsoft.AspNetCore.Components;

namespace Plaszczakowo.Drawer.GraphDrawer.Images;

public class GraphVertexPlaszczakMusicImage : GraphVertexImage
{
    public override bool GetOnVertex()
    {
        return false;
    }

    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakMusic;
    }
}