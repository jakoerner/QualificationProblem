namespace QualificationProblem
{
    public static class TriangleExtensions
    {
        public static bool IsCoincident(this Triangle self, Triangle other)
        {
            //will work regardless of wind order or starting vertex.
            int count = 0;
            foreach (var vertex in self.Vertices)
            {
                foreach (var otherVertex in other.Vertices)
                {
                    if (vertex.IsCoincident(otherVertex))
                    {
                        count++;
                        break;
                    }
                }
            }
            return count == 3;
        }

        public static Triangle WithPosition(this Triangle triangle, Vertex cellOrigin, Grid grid, bool isEvenColumnId)
        {
            if (isEvenColumnId)
            {
                triangle.Vertices = new Vertex[]
                {
                    new Vertex{X = cellOrigin.X + grid.ColumnWidth, Y = cellOrigin.Y},
                    new Vertex{X = cellOrigin.X + grid.ColumnWidth, Y=cellOrigin.Y + grid.RowHeight},
                    cellOrigin
                };
            }
            else
            {
                triangle.Vertices = new Vertex[]
                {
                    new Vertex{X = cellOrigin.X, Y = cellOrigin.Y + grid.RowHeight},
                    cellOrigin,
                    new Vertex{X = cellOrigin.X + grid.ColumnWidth, Y = cellOrigin.Y + grid.RowHeight}
                };
            }
            return triangle;
        }
    }
}
