public static class FenceCreator {

    static Random r = new();

    public static GraphInput CreateEdges(int vertex_amount) {
            List<GraphVertex> vertices = new();

            for (int index = 0; index < vertex_amount; index++) {
                int center_x = 740;
                int center_y = 440;
                int radius_x;
                int radius_y;
                int x, y;


                radius_x = 20 * vertex_amount;
                radius_y = 15 * vertex_amount;
                double angle = 2 * Math.PI * index / vertex_amount;
                x = (int)(center_x + radius_x * Math.Cos(angle));
                y = (int)(center_y + radius_y * Math.Sin(angle));

                vertices.Add(new GraphVertex(x, y, r.Next(1, 30)));
            }

        vertices.First().State = GraphStates.Special;

        List<GraphEdge> edges = new();

        for(int i = 0 ; i < vertex_amount; i++) {
            edges.Add(new GraphEdge(vertices[i], i == vertex_amount - 1 ? vertices.First() : vertices[i+1]));
        }

        return new GraphInput(vertices, edges);
    }

}