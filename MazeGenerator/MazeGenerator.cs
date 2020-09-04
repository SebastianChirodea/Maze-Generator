using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze_Component
{
    /// <summary>
    /// Generates 2d mazes and returns them in different formats
    /// </summary>
    class MazeGenerator
    {
        private const int COLUMN_INDEX_BEGINNING = 1;
        private const int ROW_INDEX_BEGINNING = 1;
        private const int LEFTMOST_INDEX = 0;

        private const int ROW_DIMENSION = 0;
        private const int COLUMN_DIMENSION = 1;

        private const int NR_OF_ROWS_TO_CHECK_HORIZONTALLY = 2;
        private const int NR_OF_COLUMNS_TO_CHECK_HORIZONTALLY = 3;

        private const int NR_OF_ROWS_TO_CHECK_VERTICALLY = 3;
        private const int NR_OF_COLUMNS_TO_CHECK_VERTICALLY = 2;

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private enum MazeCode
        {
            Wall,
            Floor
        }

        int[,] maze;
        Random random;

        public MazeGenerator() { }

        /// <summary>
        /// Generates a 2d array maze
        /// </summary>
        /// <remarks>
        /// It only calls <c>Generate</c> function with the same parameters
        /// </remarks>
        /// <param name="height">Number of rows in maze</param>
        /// <param name="length">Number of columns in maze</param>
        public MazeGenerator(int height, int length)
        {
            Generate(height, length);
        }

        /// <summary>
        /// Generates a 2 dimensional maze. The maze is surrounded by on all sides by wall values. 
        /// Generation always starts at the top right ([1,1] coordinate). 
        /// Values less than 3 and bigger than 100 will result in an empty maze
        /// </summary>
        /// <param name="height">Number of rows in maze</param>
        /// <param name="length">Number of columns in maze</param>
        public void Generate(int height, int length)
        {
            if(height < 3 || length < 3 || (height + length > 150))
            {
                maze = new int[0, 0];
                return;
            }

            maze = new int[height, length];
            random = new Random();

            FillMaze((int)MazeCode.Wall);
            GenerateRecursive(ROW_INDEX_BEGINNING, COLUMN_INDEX_BEGINNING);
        }

        private void FillMaze(int value)
        {
            int i, j;

            for (i = 0; i < maze.GetLength(ROW_DIMENSION); i++)
                for (j = 0; j < maze.GetLength(COLUMN_DIMENSION); j++)
                    maze[i, j] = value;
        }

        private void GenerateRecursive(int row, int column)
        {
            HashSet<Direction> availableDirections;
            Direction currentDirection;

            maze[row, column] = (int)MazeCode.Floor;
            availableDirections = GetAllDirections();

            do
            {
                currentDirection = GetRandomDirectionFrom(availableDirections);
                availableDirections.Remove(currentDirection);
                MoveRecursivelyIn(currentDirection, row, column);

            } while (availableDirections.Count > 0);
        }

        private HashSet<Direction> GetAllDirections()
        {
            return new HashSet<Direction> { Direction.Up, Direction.Down,
                                            Direction.Left, Direction.Right };
        }

        private Direction GetRandomDirectionFrom(HashSet<Direction> directions)
        {
            int randomPosition;

            randomPosition = random.Next(0, directions.Count);

            return directions.ElementAt(randomPosition);
        }

        private void MoveRecursivelyIn(Direction direction, int row, int column)
        {
            int nextRow, nextColumn;

            (nextRow, nextColumn) = DetermineNextPosition(direction, row, column);
            if (CanMoveInDirection(direction, nextRow, nextColumn))
            {
                GenerateRecursive(nextRow, nextColumn);
            }
        }

        private Tuple<int, int> DetermineNextPosition(Direction direction, int row, int column)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Tuple.Create(row - 1, column);
                case Direction.Down:
                    return Tuple.Create(row + 1, column);
                case Direction.Left:
                    return Tuple.Create(row, column - 1);
                case Direction.Right:
                    return Tuple.Create(row, column + 1);
                default:
                    throw new Exception("Internal code error! Non-existent direction!");
            }
        }

        private bool CanMoveInDirection(Direction direction, int row, int column)
        {
            if (IsAtEdgeOfMaze(row, column))
                return false;

            switch (direction)
            {
                case Direction.Up:
                    return CheckHorizontallyForFloorValues(row - 1, column - 1);
                case Direction.Down:
                    return CheckHorizontallyForFloorValues(row, column - 1);
                case Direction.Left:
                    return CheckVerticallyForFloorValues(row - 1, column - 1);
                case Direction.Right:
                    return CheckVerticallyForFloorValues(row - 1, column);
                default:
                    throw new Exception("Internal code error! Non-existent direction!");
            }
        }

        private bool IsAtEdgeOfMaze(int row, int column)
        {
            bool isRowAtEdge, isColumnAtEdge;

            isRowAtEdge = (row == LEFTMOST_INDEX || row == maze.GetLength(ROW_DIMENSION) - 1);
            isColumnAtEdge = (column == LEFTMOST_INDEX || column == maze.GetLength(COLUMN_DIMENSION) - 1);

            return isRowAtEdge || isColumnAtEdge;
        }

        private bool CheckHorizontallyForFloorValues(int row, int column)
        {
            int i, j;
            int nrOfFloors;

            nrOfFloors = 0;

            for (i = row; i < row + NR_OF_ROWS_TO_CHECK_HORIZONTALLY; i++)
                for (j = column; j < column + NR_OF_COLUMNS_TO_CHECK_HORIZONTALLY; j++)
                    nrOfFloors = (maze[i, j] == (int)MazeCode.Floor) ? nrOfFloors + 1 : nrOfFloors;

            return nrOfFloors == 0;
        }

        private bool CheckVerticallyForFloorValues(int row, int column)
        {
            int i, j;
            int nrOfFloors;

            nrOfFloors = 0;

            for (i = row; i < row + NR_OF_ROWS_TO_CHECK_VERTICALLY; i++)
                for (j = column; j < column + NR_OF_COLUMNS_TO_CHECK_VERTICALLY; j++)
                    nrOfFloors = (maze[i, j] == (int)MazeCode.Floor) ? nrOfFloors + 1 : nrOfFloors;

            return nrOfFloors == 0;
        }

        /// <summary>
        /// Getter method for the maze
        /// </summary>
        /// <param name="floorCode">Caller can choose the floor integer value in the maze, default value is 1</param>
        /// <param name="wallCode">Caller can choose the wall integer value in the maze, default value is 0</param>
        /// <returns>
        /// Returns a copy of the maze using a 2 dimensional List
        /// </returns>
        public List<List<int>> GetListMaze(int floorCode = 1,
                                           int wallCode = 0)
        {
            List<List<int>> mazeCopy;
            int i, j, rowsCount, columnsCount;

            rowsCount = maze.GetLength(0);
            columnsCount = maze.GetLength(1);

            mazeCopy = new List<List<int>>();

            for (i = 0; i < rowsCount; i++)
            {
                mazeCopy.Add(new List<int>());

                for (j = 0; j < columnsCount; j++)
                {
                    if (maze[i, j] == (int)MazeCode.Wall)
                        mazeCopy[i].Add(wallCode);
                    else
                        mazeCopy[i].Add(floorCode);
                }
            }

            return mazeCopy;
        }

        /// <summary>
        /// Getter method for the maze
        /// </summary>
        /// <param name="floorCode">Caller can choose the floor integer value in the maze, default value is 1</param>
        /// <param name="wallCode">Caller can choose the wall integer value in the maze, default value is 0</param>
        /// <returns>
        /// Returns a copy of the maze using a 2 dimensional array
        /// </returns>
        public int[,] GetArrayMaze(int floorCode = 1,
                                   int wallCode = 0)
        {
            int[,] mazeCopy;
            int i, j, rowsCount, columnsCount;

            rowsCount = maze.GetLength(0);
            columnsCount = maze.GetLength(1);

            mazeCopy = new int[rowsCount, columnsCount];

            for (i = 0; i < rowsCount; i++)
                for (j = 0; j < columnsCount; j++)
                {
                    if (maze[i, j] == (int)MazeCode.Wall)
                        mazeCopy[i, j] = wallCode;
                    else
                        mazeCopy[i, j] = floorCode;
                }

            return mazeCopy;
        }

        /// <summary>
        /// Getter method that returns floor(non-wall) coordinates
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetFloorCoordinates()
        {
            List<Tuple<int, int>> coordinates;
            int i, j, columnsCount, rowsCount;

            columnsCount = maze.GetLength(0);
            rowsCount = maze.GetLength(1);

            coordinates = new List<Tuple<int, int>>();

            for (i = 0; i < columnsCount; i++)
                for (j = 0; j < rowsCount; j++)
                    if (maze[i, j] == (int)MazeCode.Floor)
                        coordinates.Add(new Tuple<int, int>(i, j));

            return coordinates;
        }
    }
}
