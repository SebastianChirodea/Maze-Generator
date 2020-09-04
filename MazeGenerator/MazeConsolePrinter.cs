using System;
using System.Collections.Generic;

namespace Maze_Component
{
    /// <summary>
    /// Contains methods for printing mazes from <c>MazeGenerator</c> class
    /// </summary>
    static class MazeConsolePrinter
    {

        /// <summary>
        /// Prints the maze on the console in different colors
        /// </summary>
        /// <param name="maze">The generated maze in 2 dimensional List format</param>
        /// <param name="wallColor">Color for displaying the walls, default color is red</param>
        /// <param name="floorColor">Color for displaying the walls, default color is green</param>
        public static void Print2dList(List<List<int>> maze, ConsoleColor wallColor = ConsoleColor.Red,
                                                             ConsoleColor floorColor = ConsoleColor.Green)
        {
            int wallCode;

            if (maze.Count > 0)
                wallCode = maze[0][0];
            else
                return;

            foreach (List<int> row in maze)
            {
                foreach (int value in row)
                {
                    if (value == wallCode)
                        Console.BackgroundColor = wallColor;
                    else
                        Console.BackgroundColor = floorColor;

                    Console.Write(value + " ");

                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Prints the maze on the console in different colors
        /// </summary>
        /// <param name="maze">The generated maze in 2 dimensional array format</param>
        /// <param name="wallColor">Color for displaying the walls, default color is red</param>
        /// <param name="floorColor">Color for displaying the walls, default color is green</param>
        public static void Print2dArray(int[,] maze, ConsoleColor wallColor = ConsoleColor.Red,
                                                             ConsoleColor floorColor = ConsoleColor.Green)
        {
            int i, j;
            int wallCode;

            if (maze.Length > 0)
                wallCode = maze[0, 0];
            else
                return;

            for (i = 0; i < maze.GetLength(0); i++)
            {
                for (j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == wallCode)
                        Console.BackgroundColor = wallColor;
                    else
                        Console.BackgroundColor = floorColor;

                    Console.Write(maze[i, j] + " ");
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }

            Console.WriteLine("\n");
        }
    }
}
