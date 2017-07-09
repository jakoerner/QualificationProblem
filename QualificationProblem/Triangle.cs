namespace QualificationProblem
{
    public struct Triangle
    {
        public char RowId { get; set; }
        public int ColumnId { get; set; }
        public Vertex[] Vertices { get; set; }

        public override string ToString()
        {
            return $"{{{RowId},{ColumnId}}}; Verts = {Vertices.ToAggregatedDisplayValue()}";
        }
    }
}
