using System.Linq;

namespace QualificationProblem
{
    public static class GridExtensions
    {
        //row and column indices (base-0):
        public static int GetRowIndex(this Grid grid, char rowId) => grid.IsValidRowId(rowId) ? (int)rowId - (int)grid.MinRow : -1;

        public static int GetColumnIndex(this Grid grid, int columnId) => grid.IsValidColumnId(columnId) ? (columnId - 1) / 2 : -1;
                
        //grid cells:
        public static Vertex? GetGridCellOrigin(this Grid grid, char rowId, int columnId)
        {
            var rowIndex = grid.GetRowIndex(rowId);
            if (rowIndex < 0)
                return null;
            var columnIndex = grid.GetColumnIndex(columnId);
            if (columnIndex < 0)
                return null;

            //row and column index are valid...proceed to calculate the vertex coordinates:
            return new Vertex()
            {
                X = grid.ColumnWidth * columnIndex,
                Y = grid.RowHeight * rowIndex
            };
        }

        //triangles:
        public static Triangle? GetTriangleByRowColumn(this Grid grid, char rowId, int columnId)
        {
            //origin opposite hypotenuse, CW wind order:
            var gridCellOrigin = grid.GetGridCellOrigin(rowId, columnId);
            if (gridCellOrigin == null)
                return null;
            return new Triangle()
                {
                    RowId = rowId,
                    ColumnId = columnId
                }
                .WithPosition(gridCellOrigin.Value, grid, columnId.IsEven());
        }

        public static bool GetTriangleRowColumnFromVertices(this Grid grid, Vertex[] vertices, out char rowId, out int columnId)
        {
            //set defaults for out params:
            rowId = '?';
            columnId = -1;

            //check vertex count
            if (vertices == null || vertices.Count() != 3)
                return false;

            //validate verts as a valid triangle (a valid triangle will have 2 points with the same X, and 2 points with the same Y):
            if (vertices.Select(v => v.X).Distinct().Count() != 2)
                return false;
            if (vertices.Select(v => v.Y).Distinct().Count() != 2)
                return false;

            //Locate cell origin:
            int minX = vertices.Min(v => v.X);
            int minY = vertices.Min(v => v.Y);

            int maxX = vertices.Max(v => v.X);
            int maxY = vertices.Max(v => v.Y);

            //check out of range conditions; fail if any exist:
            if (minX < 0 || minY < 0)
                return false;
            if (maxX > grid.MaxX() || maxY > grid.MaxY())
                return false;

            //check that coordinates are multiples of row/column dimensions:
            if (minX % grid.ColumnWidth != 0 || maxX % grid.ColumnWidth != 0 || minY % grid.RowHeight != 0 || maxY % grid.RowHeight != 0)
                return false;

            //we know we are inside the grid now...proceed to determine actual row/column:
                var rowIndex = minY / grid.RowHeight;
            rowId = (char)((int)grid.MinRow + rowIndex);

            var rawColumnIndex = minX / grid.ColumnWidth;

            //check whether we are on an odd or even column:
            //assign 'offset' of 1 for odd; 2 for even...
            //assuming the triangle verts are valid, this will occur if we count the vertices where:  vert.Y == minY
            int offfset = vertices.Where(v => v.Y == minY).Count();
            columnId = rawColumnIndex * 2 + offfset;
            return true;
        }

        //image coordinates:
        public static int MaxX(this Grid grid)
        {
            return (1 + grid.MaxColumn - grid.MinColumn) * grid.ColumnWidth / 2;
        }
        public static int MaxY(this Grid grid)
        {
            return (1 + (int)grid.MaxRow - (int)grid.MinRow) * grid.RowHeight;
        }

        //row and column Id validation:
        public static bool IsValidRowId(this Grid grid, char rowId)
        {
            return rowId >= grid.MinRow && rowId <= grid.MaxRow;
        }
        public static bool IsValidColumnId(this Grid grid, int columnId)
        {
            return columnId >= grid.MinColumn && columnId <= grid.MaxColumn;
        }

    }
}
