using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QualificationProblem;

namespace QualificationProblemConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            int mode = -1;
            do
            {
                Console.Write("Select mode ([1] Get triangle vertices from {row, column}; [2] Get {row, column} from triangle vertices): ");
                var response = Console.ReadKey();
                if (response.Key == ConsoleKey.Escape)
                    return;
                int.TryParse(response.KeyChar.ToString(), out mode);
                Console.WriteLine();
            } while (!ModeIsValid(mode));

            Grid grid = new Grid();

            if (mode == 1)
                RunInMode1(grid);
            else
                RunInMode2(grid);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        #region Mode 1 methods (problem question #1)
        private static void RunInMode1(Grid grid)
        {           
            char rowId = DoPromptForRowId(grid);
            int columnId = DoPromptForColumnId(grid);

            var triangle = grid.GetTriangleByRowColumn(rowId, columnId);
            if (triangle == null)
            {
                Console.WriteLine("No triangle was found!");
            }
            else
            {
                Console.WriteLine($"Triangle found: {triangle.ToString()}");
            }
        }

        private static char DoPromptForRowId(Grid grid)
        {
            char result = char.MinValue;

            do
            {
                Console.Write($"Enter RowId ({grid.MinRow}-{grid.MaxRow}): ");
                var response = Console.ReadKey();
                result = (response.KeyChar.ToString().ToUpper().FirstOrDefault());
                Console.WriteLine();
            } while (!grid.IsValidRowId(result));

            return result;
        }

        private static int DoPromptForColumnId(Grid grid)
        {
            int result = -1;

            do
            {
                Console.Write($"Enter ColumnId ({grid.MinColumn}-{grid.MaxColumn}): ");
                var response = Console.ReadLine();
                int.TryParse(response, out result);
                Console.WriteLine();
            } while (!grid.IsValidColumnId(result));

            return result;
        }
        #endregion

        #region Mode 2 methods (problem question #2)
        private static void RunInMode2(Grid grid)
        {
            var vertex0 = DoPromptForVertex(grid, 0);
            var vertex1 = DoPromptForVertex(grid, 1);
            var vertex2 = DoPromptForVertex(grid, 2);

            var vertices = new Vertex[] { vertex0, vertex1, vertex2 };
            char rowId = char.MinValue;
            int columnId = -1;

            if (grid.GetTriangleRowColumnFromVertices(vertices, out rowId, out columnId))
            {
                Console.WriteLine($"Triangle grid position:  {{{rowId}, {columnId}}}");
            }
            else
            {
                Console.WriteLine("Unable to determine triangle grid position from the specified vertices!");
            }
        }

        private static Vertex DoPromptForVertex(Grid grid, int vertexId)
        {
            Vertex result = new Vertex();
            bool isValidVertex = false;
            do
            {
                Console.Write($"Enter (pixel) coordinates for vertex #{vertexId} in the form 'X,Y': ");
                var response = Console.ReadLine();

                var splitResponse = response.Split(",".ToCharArray());

                if (splitResponse.Count() == 2)
                {
                    int x = -1;
                    int y = -1;

                    if (int.TryParse(splitResponse[0].Trim(), out x) && int.TryParse(splitResponse[1].Trim(), out y))
                    {
                        result.X = x;
                        result.Y = y;
                        isValidVertex = true;
                    }
                }
                Console.WriteLine();
            } while (!isValidVertex);

            return result;
        }

        #endregion

        #region Misc
        private static bool ModeIsValid(int mode)
        {
            return mode == 1 || mode == 2;
        }
        #endregion
    }
}
