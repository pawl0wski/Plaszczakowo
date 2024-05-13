using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

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

        if (e.Throughput.Capacity != -1)
            (heightIndicator, xOffset, yOffset) = (1.3, -20, 7.5);
        else
            (heightIndicator, xOffset, yOffset) = (2, -1, 0);   
        
        var x = e.From.X + (e.To.X - e.From.X) / heightIndicator + xOffset;
        var y = e.From.Y + (e.To.Y - e.From.Y) / heightIndicator + yOffset;

        await _context.SetFillStyleAsync(e.State.GetThroughputColor());
        await _context.SetFontAsync("bold 20px Cascadia Mono");
        await _context.FillTextAsync(e.Throughput.ToString(), x, y);
    }

    private async Task DrawVertex(GraphVertex v)
    {
        if (v.VertexImageRef == null)
        {
            await DrawCircleOutline(v);
            await DrawCircle(v);
        }
        else
        {
            await DrawImageFromRef(v);
        }

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
        if (v.VertexImageRef is null)
            throw new NullReferenceException();

        await _context.DrawImageAsync((ElementReference)v.VertexImageRef, v.X - 15, v.Y - 16);
    }

    private async Task FillVertexTextContent(GraphVertex v)
    {
        await _context.SetFillStyleAsync(v.State.GetPrimaryColor());
        await _context.SetFontAsync("26px Cascadia Mono");
        var text = v.Value ?? "";
        var textWidth = await _context.MeasureTextAsync(text);
        var x = v.X - textWidth.Width / 2;
        var y = v.Y + 7;
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
        double arrowSize = 15; 
        var angle = Math.Atan2(e.To.Y - e.From.Y, e.To.X - e.From.X);

        double vertexRadius = 30; 
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