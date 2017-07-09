namespace QualificationProblem
{
    public struct Vertex
    {      
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }
    }
}
