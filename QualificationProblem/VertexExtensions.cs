using System.Text;
using System.Linq;
namespace QualificationProblem
{
    public static class VertexExtensions
    {
        public static bool IsCoincident(this Vertex self, Vertex other)
        {
            return self.X == other.X && self.Y == other.Y;
        }

        public static string ToAggregatedDisplayValue(this Vertex[] vertices)
        {
            if (vertices == null || vertices.Count() == 0)
                return "{Empty vertex collection!}";
            StringBuilder sb = new StringBuilder();
            foreach (var vertex in vertices)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(vertex.ToString());
            }
            return sb.ToString();
        }

        public static bool IsRightTriangleVertexSet(this Vertex[] vertices)
        {
            //validate verts as a valid triangle (a valid triangle will have 2 points with the same X, and 2 points with the same Y):
            if (vertices.Select(v => v.X).Distinct().Count() != 2)
                return false;
            if (vertices.Select(v => v.Y).Distinct().Count() != 2)
                return false;
            return true;
        }
    }
}
