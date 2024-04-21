using Microsoft.AspNetCore.Components;
namespace GraphDrawer;

public class GraphVertex
    {
        public int X;
        public int Y;
        public int? Value;
        public ElementReference? VertexImageRef;
        public GraphState State;

        public GraphVertex(int x, int y, ElementReference? vertexImageRef, int? value = null, GraphState? state = null) {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.State = state ?? GraphStates.Inactive;
            this.VertexImageRef = vertexImageRef;
        }

    }
