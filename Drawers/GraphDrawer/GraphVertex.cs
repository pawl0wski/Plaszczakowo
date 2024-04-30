using Microsoft.AspNetCore.Components;
namespace Drawer.GraphDrawer;

public class GraphVertex : ICloneable
    {
        public int X;
        public int Y;
        public string? Value;
        public ElementReference? VertexImageRef;
        public GraphState State;

        public GraphVertex(int x, int y, string? value = null, GraphState? state = null, ElementReference? vertexImageRef = null) {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.State = state ?? GraphStates.Inactive;
            this.VertexImageRef = vertexImageRef;
        }

        public object Clone()
        {
            return new GraphVertex(X, Y, Value, State, VertexImageRef);
        }
    }
