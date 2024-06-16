using Microsoft.AspNetCore.Components;

namespace Plaszczakowo.Drawer.GraphDrawer.Images;

public interface IGraphVertexImageProvider
{
    public ElementReference Factory { get; }

    public ElementReference PlaszczakStep1 { get; }

    public ElementReference PlaszczakStep2 { get; }

    public ElementReference PlaszczakMusic { get; }

    public ElementReference PlaszczakSleeping { get; }

    public ElementReference PlaszczakiFence { get; }
    
    public ElementReference FrontCarrierInactive { get; }
    
    public ElementReference FrontCarrierActive { get; }
    
    public ElementReference RearCarrierInactive { get; }
    
    public ElementReference RearCarrierActive { get; }
}