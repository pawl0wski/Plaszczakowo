using Blazor.Extensions.Canvas.Canvas2D;

namespace GraphDrawer;

public class GraphDrawer
{
    private readonly Canvas2DContext _context;
    private GraphData _data;

    public GraphDrawer(Canvas2DContext context, List<GraphVertex> vertexes, List<GraphEdge> edges)
    {
        _context = context;
        _data = new GraphData(vertexes, edges);
    }

    public GraphDrawer(Canvas2DContext context, GraphData data)
    {
        _context = context;
        _data = data;
    }
    
    public async Task Draw()
    {
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

    public async Task DrawEdge(GraphEdge e)
    {
        await _context.SetStrokeStyleAsync(e.State.GetPrimaryColor());
        await _context.SetLineWidthAsync(e.State.GetLineWidth());

        await _context.BeginPathAsync();
        await _context.MoveToAsync(e.From.X, e.From.Y);
        await _context.LineToAsync(e.To.X, e.To.Y);
        await _context.ClosePathAsync();
        await _context.StrokeAsync();
    }

    public async Task DrawVertex(GraphVertex v)
    {
        // Drawing circle outline
        await _context.SetFillStyleAsync(v.State.GetPrimaryColor());
        await _context.BeginPathAsync();
        await _context.ArcAsync(v.X, v.Y, 30, 0, 2 * Math.PI);
        await _context.FillAsync();

        // Drawing circle
        await _context.SetFillStyleAsync(v.State.GetSecondaryColor());
        await _context.BeginPathAsync();
        await _context.ArcAsync(v.X, v.Y, 25, 0, 2 * Math.PI);
        await _context.FillAsync();

        // Vertex value
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