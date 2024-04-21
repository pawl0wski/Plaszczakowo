using Blazor.Extensions.Canvas.Canvas2D;

namespace GraphDrawer;

public class DrawGraph
{
    private readonly Canvas2DContext _context;
    private readonly List<GraphVertex> _vertices;
    private readonly List<GraphEdge> _edges;

    public DrawGraph(Canvas2DContext context, ref List<GraphVertex> vertexes, ref List<GraphEdge> edges)
    {
        _context = context;
        _vertices = vertexes;
        _edges = edges;
    }

    public DrawGraph(Canvas2DContext context, ref GraphInput input)
    {
        _context = context;
        _vertices = input.Vertices;
        _edges = input.Edges;
    }

    public async Task ChangeVerticeStatus(int index, GraphState state) {
        _vertices[index].State = state;
        await DrawVertex(_vertices[index]);
    }

    public async Task ChangeEdgeStatus(int index, GraphState state) {
        var currentEdge = _edges[index];
        currentEdge.State = state;
        await DrawEdge(currentEdge);

        await DrawVertex(currentEdge.From);
        await DrawVertex(currentEdge.To);
    }

    public async Task Draw()
    {
        await ClearCanvas();

        foreach (var edge in _edges) {
            await DrawEdge(edge);
        }

        foreach(var vertex in _vertices) {
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
    }

    private async Task DrawVertex(GraphVertex v) {
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
