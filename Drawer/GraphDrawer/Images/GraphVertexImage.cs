using Microsoft.AspNetCore.Components;

namespace Plaszczakowo.Drawer.GraphDrawer.Images;

public abstract class GraphVertexImage
{
    protected ElementReference ImageReference { get; private set; }
    public abstract bool GetOnVertex();

    public ElementReference GetImageReference()
    {
        return ImageReference;
    }

    public void FillWithProvider(IGraphVertexImageProvider provider)
    {
        var reference = GetImageReferenceFromProvider(provider);
        ImageReference = reference;
    }

    protected abstract ElementReference GetImageReferenceFromProvider(IGraphVertexImageProvider provider);
}