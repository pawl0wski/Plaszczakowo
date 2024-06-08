using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public abstract class GraphVertexImage
{
    public abstract bool GetOnVertex();

    public ElementReference GetImageReference()
        => ImageReference;
    protected ElementReference ImageReference { get; private set; }

    public void FillWithProvider(IGraphVertexImageProvider provider)
    {
        var reference = GetImageReferenceFromProvider(provider);
        ImageReference = reference;
    }

    protected abstract ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider);
}