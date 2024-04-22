using Microsoft.AspNetCore.Components;
namespace GraphDrawer;

public class GraphVertex
    {
        public int X;
        public int Y;
        public int? Value;
        public ElementReference? VertexImageRef;
        public GraphState State;

        public GraphVertex(int x, int y, int? value = null, GraphState? state = null, ElementReference? vertexImageRef = null) {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.State = state ?? GraphStates.Inactive;
            this.VertexImageRef = vertexImageRef;
        }

    }
