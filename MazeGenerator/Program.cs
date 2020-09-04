using System;
using System.Collections.Generic;

namespace Maze_Component
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeGenerator mazeGenerator = new MazeGenerator(200, 200);

            List<List<int>> maze = mazeGenerator.GetListMaze();
            MazeConsolePrinter.Print2dList(maze);

            MazeConsolePrinter.Print2dArray(mazeGenerator.GetArrayMaze());

            mazeGenerator.Generate(6, 6);
            MazeConsolePrinter.Print2dList(mazeGenerator.GetListMaze());

            foreach (Tuple<int, int> pair in mazeGenerator.GetFloorCoordinates())
                Console.Write("(" + pair.Item1 + ", " + pair.Item2 + "), ");
            Console.WriteLine();
        }
    }
}
