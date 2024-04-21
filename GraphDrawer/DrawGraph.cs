﻿using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
namespace GraphDrawer;

public class DrawGraph
{
    private readonly Canvas2DContext _context;
    public List<GraphVertex> Vertices;
    public List<GraphEdge> Edges;

    public DrawGraph(Canvas2DContext _context, ref List<GraphVertex> vertexes, ref List<GraphEdge> edges)
    {
        this._context = _context;
        this.Vertices = vertexes;
        this.Edges = edges;
    }

    public DrawGraph(Canvas2DContext _context, ref GraphInput input)
    {
        this._context = _context;
        this.Vertices = input.Vertices;
        this.Edges = input.Edges;
    }

    public async Task ChangeVerticeStatus(int index, GraphState state) {
        Vertices[index].State = state;
        await DrawVertex(Vertices[index]);
    }

    public async Task ChangeEdgeStatus(int index, GraphState state) {
        var currentEdge = Edges[index];
        currentEdge.State = state;
        await DrawEdge(currentEdge);

        await DrawVertex(currentEdge.From);
        await DrawVertex(currentEdge.To);
    }

    public async Task Draw()
    {
        await ClearCanvas();

        foreach (var edge in Edges) {
            await DrawEdge(edge);
        }

        foreach(var vertex in Vertices) {
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

        if (e.Flow != null) await FillEdgeWithFlow(e); ;       
    }
    private async Task FillEdgeWithFlow(GraphEdge e)
    {
        if (e.Flow == null) throw new NullReferenceException();

        var x = (e.From.X + e.To.X) / 2 - 15.5;
        var y = (e.From.Y + e.To.Y) / 2 + 7;

        await _context.SetFillStyleAsync("red");
        await _context.SetFontAsync("bold 20px Cascadia Mono");
        await _context.FillTextAsync(e.Flow.ToString(), x, y);

    }

    public async Task DrawVertex(GraphVertex v) {

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
        var text_width = await _context.MeasureTextAsync(text);
        var x = v.X - text_width.Width / 2;
        var y = v.Y + 7;
        await _context.FillTextAsync(text, x, y);
    }

    private async Task ClearCanvas()
    {
        await _context.ClearRectAsync(0, 0, 1920, 1080);
    }

}
