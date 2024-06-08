using Microsoft.AspNetCore.Components;

public interface IGraphVertexImageProvider
{
    public ElementReference Factory { get; }

    public ElementReference PlaszczakBackActive { get; }

    public ElementReference PlaszczakBackHighlighted { get; }

    public ElementReference PlaszczakBackInactive { get; }

    public ElementReference PlaszczakFrontActive { get; }

    public ElementReference PlaszczakFrontHighlighted { get; }

    public ElementReference PlaszczakFrontInactive { get; }

    public ElementReference PlaszczakMusic { get; }

    public ElementReference PlaszczakSleeping { get; }

    public ElementReference PlaszczakiFence { get; }
}