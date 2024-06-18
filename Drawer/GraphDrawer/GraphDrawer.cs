using Blazor.Extensions.Canvas.Canvas2D;
using Plaszczakowo.Drawer.GraphDrawer.Images;

namespace Plaszczakowo.Drawer.GraphDrawer;

public class GraphDrawer : Drawer
{
    private readonly Canvas2DContext _context;
    private GraphData? _data;

    public GraphDrawer(Canvas2DContext context, GraphData? data = null)
    {
        _context = context;
        _data = data;
    }

    public GraphData? GetData()
    {
        return _data;
    }

    public void UpdateGraphData(GraphData newData)
    {
        _data = newData;
    }

    public void UpdateVertexImages(IGraphVertexImageProvider provider)
    {
        _data!.FillImagesWithProvider(provider);
    }

    public override async Task Draw()
    {
        if (_data is null)
            throw new NullReferenceException("GraphDrawer cannot draw if GraphData is not set.");

        await _context.BeginBatchAsync();

        await ClearCanvas();

        foreach (var edge in _data.Edges) await DrawEdge(edge);

        foreach (var vertex in _data.Vertices) await DrawVertex(vertex);

        foreach (var text in _data.Texts) await DrawText(text);

        await _context.EndBatchAsync();
    }

    public override void ChangeDrawerData(DrawerData drawerData)
    {
        if (drawerData is not GraphData newGraphData)
            throw new InvalidCastException("DrawerData should be GraphData");
        UpdateGraphData(newGraphData);
    }

    private async Task DrawEdge(GraphEdge e)
    {
        await _context.SetStrokeStyleAsync(e.State.GetPrimaryColor());
        await _context.SetLineWidthAsync(e.State.GetLineWidth());

        await _context.BeginPathAsync();
        await _context.MoveToAsync(e.From.X, e.From.Y);
        await _context.LineToAsync(e.To.X, e.To.Y);
        await _context.ClosePathAsync();
        await _context.StrokeAsync();

        if (e.Directed)
            await DrawDirectedArrow(e);

        if (e.Throughput != null)
            await FillEdgeWithThroughput(e);
    }

    private async Task FillEdgeWithThroughput(GraphEdge e)
    {
        if (e.Throughput == null)
            throw new NullReferenceException();
        double heightIndicator;
        double xOffset;
        double yOffset;

        if (e.Throughput.Capacity == -1 || e.Throughput.Capacity > 1)
            (heightIndicator, xOffset, yOffset) = (2, -1, 0);
        else
            (heightIndicator, xOffset, yOffset) = (1.3, -20, 7.5);

        var x = e.From.X + (e.To.X - e.From.X) / heightIndicator + xOffset;
        var y = e.From.Y + (e.To.Y - e.From.Y) / heightIndicator + yOffset;


        await _context.SetFillStyleAsync(e.State.GetThroughputColor());
        await _context.SetFontAsync("bold 24px Arial");
        await _context.FillTextAsync(e.Throughput.ToString(), x, y);
        
    }

    private async Task DrawVertex(GraphVertex v)
    {
        if (!v.VertexImage?.GetOnVertex() ?? true)
        {
            await DrawCircleOutline(v);
            await DrawCircle(v);
        }

        if (v.VertexImage != null) await DrawImageFromRef(v);

        await FillVertexTextContent(v);
    }

    private async Task DrawCircleOutline(GraphVertex v)
    {
        await _context.SetFillStyleAsync(v.State.GetPrimaryColor());
        await _context.BeginPathAsync();
        await _context.ArcAsync(v.X, v.Y, v.State.GetEdgeRadius() + v.State.GetOutlineWidth(), 0, 2 * Math.PI);
        await _context.FillAsync();
    }

    private async Task DrawCircle(GraphVertex v)
    {
        await _context.SetFillStyleAsync(v.State.GetSecondaryColor());
        await _context.BeginPathAsync();
        await _context.ArcAsync(v.X, v.Y, v.State.GetEdgeRadius(), 0, 2 * Math.PI);
        await _context.FillAsync();
    }

    private async Task DrawImageFromRef(GraphVertex v)
    {
        if (v.VertexImage is null)
            throw new NullReferenceException();

        if (v.VertexImage.GetOnVertex())
            await _context.DrawImageAsync(v.VertexImage.GetImageReference(), v.X - 25, v.Y - 25);
        else
            await _context.DrawImageAsync(v.VertexImage.GetImageReference(), v.X + 10, v.Y - 45);
    }

    private async Task FillVertexTextContent(GraphVertex v)
    {
        await _context.SetFillStyleAsync(v.State.GetPrimaryColor());
        await _context.SetFontAsync("26px Dekko");
        var text = v.Value ?? "";
        await _context.SetTextAlignAsync(TextAlign.Center);
        await _context.SetTextBaselineAsync(TextBaseline.Middle);
        var x = v.X;
        var y = v.Y + 4;
        await _context.FillTextAsync(text, x, y);
    }

    private async Task ClearCanvas()
    {
        await _context.ClearRectAsync(0, 0, 1920, 1080);
    }

    private async Task DrawText(GraphText graphText)
    {
        var text = graphText.Text;
        var x = graphText.X;
        var y = graphText.Y;

        await _context.SetFillStyleAsync(graphText.State.GetPrimaryColor());
        await _context.FillTextAsync(text, x, y);
    }

    private async Task DrawDirectedArrow(GraphEdge e)
    {
        await _context.SetFillStyleAsync(e.State.GetPrimaryColor());
        double arrowSize = 15;
        var angle = Math.Atan2(e.To.Y - e.From.Y, e.To.X - e.From.X);

        double vertexRadius = e.State.GetOutlineWidth() + e.State.GetEdgeRadius();
        var endX = e.To.X - vertexRadius * Math.Cos(angle);
        var endY = e.To.Y - vertexRadius * Math.Sin(angle);

        var intersectX = e.To.X - vertexRadius * Math.Cos(angle - Math.PI);
        var intersectY = e.To.Y - vertexRadius * Math.Sin(angle - Math.PI);

        await _context.BeginPathAsync();
        await _context.MoveToAsync(endX, endY);
        await _context.LineToAsync(intersectX, intersectY);
        await _context.StrokeAsync();

        var x1 = endX - arrowSize * Math.Cos(angle - Math.PI / 6);
        var y1 = endY - arrowSize * Math.Sin(angle - Math.PI / 6);
        var x2 = endX - arrowSize * Math.Cos(angle + Math.PI / 6);
        var y2 = endY - arrowSize * Math.Sin(angle + Math.PI / 6);

        await _context.BeginPathAsync();
        await _context.MoveToAsync(endX, endY);
        await _context.LineToAsync(x1, y1);
        await _context.LineToAsync(x2, y2);
        await _context.ClosePathAsync();
        await _context.FillAsync();
    }
}