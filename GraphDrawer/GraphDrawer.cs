using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace GraphDrawer;

public class GraphDrawer
{
    private readonly Canvas2DContext _context;
    private GraphData? _data;

    public GraphDrawer(Canvas2DContext context, List<GraphVertex> vertexes, List<GraphEdge> edges)
    {
        _context = context;
        _data = new GraphData(vertexes, edges);
    }

    public GraphDrawer(Canvas2DContext context, GraphData? data = null)
    {
        _context = context;
        _data = data;
    }

    public void ApplyNewGraphData(GraphData data)
    {
        _data = data;
    }

    public async Task ChangeVertexStatusAndRedraw(int index, GraphState state)
    {
        if (_data is null)
            throw new NullReferenceException();

        var currentVertex = _data.ChangeVertexStatus(index, state);

        await DrawVertex(currentVertex);
    }

    public async Task ChangeEdgeStatusAndRedraw(int index, GraphState state)
    {
        if (_data is null)
            throw new NullReferenceException();

        var currentEdge = _data.ChangeEdgeStatus(index, state);

        await DrawEdge(currentEdge);
        await DrawVertex(currentEdge.From);
        await DrawVertex(currentEdge.To);
    }

    public async Task ChangeEdgeFlowAndRedraw(int index, GraphFlow newFlow)
    {
        if (_data is null)
            throw new NullReferenceException();

        var currentEdge = _data.ChangeEdgeFlow(index, newFlow);
        await DrawEdge(currentEdge);
    }


    public async Task Draw()
    {
        if (_data is null)
            throw new NullReferenceException();

        await ClearCanvas();

        foreach (var edge in _data.Edges)
        {
            await DrawEdge(edge);
        }

        foreach (var vertex in _data.Vertices)
        {
            await DrawVertex(vertex);
        }
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

        if (e.Flow != null)
            await FillEdgeWithFlow(e);
    }

    private async Task FillEdgeWithFlow(GraphEdge e)
    {
        if (e.Flow == null)
            throw new NullReferenceException();

        var x = (e.From.X + e.To.X) / 2 - 15.5;
        var y = (e.From.Y + e.To.Y) / 2 + 7;

        await _context.SetFillStyleAsync("red");
        await _context.SetFontAsync("bold 20px Cascadia Mono");
        await _context.FillTextAsync(e.Flow.ToString(), x, y);
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
        await _context.ArcAsync(v.X, v.Y, 30, 0, 2 * Math.PI);
        await _context.FillAsync();
    }

    private async Task DrawCircle(GraphVertex v)
    {
        await _context.SetFillStyleAsync(v.State.GetSecondaryColor());
        await _context.BeginPathAsync();
        await _context.ArcAsync(v.X, v.Y, 25, 0, 2 * Math.PI);
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
        var text = v.Value.ToString();
        var textWidth = await _context.MeasureTextAsync(text);
        var x = v.X - textWidth.Width / 2;
        var y = v.Y + 7;
        await _context.FillTextAsync(text, x, y);
    }

    private async Task ClearCanvas()
    {
        await _context.ClearRectAsync(0, 0, 1920, 1080);
    }
}