using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertexPlaszczakMusicImage : GraphVertexImage
{
    protected override ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider)
    {
        return provider.PlaszczakMusic;
    }
}