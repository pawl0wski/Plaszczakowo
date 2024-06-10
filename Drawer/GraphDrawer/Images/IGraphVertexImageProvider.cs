using Microsoft.AspNetCore.Components;

public interface IGraphVertexImageProvider
{
    public ElementReference Factory { get; }

    public ElementReference PlaszczakStep1 { get; }

    public ElementReference PlaszczakStep2 { get; }

    public ElementReference PlaszczakMusic { get; }

    public ElementReference PlaszczakSleeping { get; }

    public ElementReference PlaszczakiFence { get; }
}