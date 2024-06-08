﻿using Microsoft.AspNetCore.Components;

namespace Drawer.GraphDrawer;

public class GraphVertex : ICloneable
{
    public GraphState State;
    public string? Value;
    public GraphVertexImage? VertexImage;
    public int X;
    public int Y;

    public GraphVertex(int x, int y, string? value = null, GraphState? state = null,
        GraphVertexImage? vertexImage = null)
    {
        X = x;
        Y = y;
        Value = value;
        State = state ?? GraphStates.Inactive;
        VertexImage = vertexImage;
    }

    public void FillImageWithProvider(IGraphVertexImageProvider provider)
    {
        ArgumentNullException.ThrowIfNull(VertexImage);
        
        VertexImage.FillWithProvider(provider);
    }

    public object Clone()
    {
        return new GraphVertex(X, Y, Value, State, VertexImage);
    }
}