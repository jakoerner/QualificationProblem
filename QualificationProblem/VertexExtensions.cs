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
    }
}
