using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Maze_Component;


namespace MazeGeneratorTests
{
    [TestClass]
    public class MazeGeneratorTests
    {
        MazeGenerator maze;

        [TestInitialize]
        public void TestInitialize()
        {
            maze = new MazeGenerator();
        }

        [TestMethod]
        public void Generate_HeightLowerThanLimit()
        {
            for (int i = -3; i < MazeGenerator.ParameterValidator.HEIGHT_LOWER_LIMIT; i++)
            {
                maze.Generate(i, 9);
                List<List<int>> listMaze = maze.GetListMaze();
                Assert.AreEqual(0, listMaze.Count);
            }
        }

        [TestMethod]
        public void Generate_LengthLowerThanLimit()
        {
            for (int i = -3; i < MazeGenerator.ParameterValidator.LENGTH_LOWER_LIMIT; i++)
            {
                maze.Generate(9, i);
                List<List<int>> listMaze = maze.GetListMaze();
                Assert.AreEqual(0, listMaze.Count);
            }
        }

        [TestMethod]
        public void Generate_HeightHigherThanLimit()
        {
            for (int i = 106; i > MazeGenerator.ParameterValidator.HEIGHT_UPPER_LIMIT; i--)
            {
                maze.Generate(i, 9);
                List<List<int>> listMaze = maze.GetListMaze();
                Assert.AreEqual(0, listMaze.Count);
            }
        }

        [TestMethod]
        public void Generate_LenghtHigherThanLimit()
        {
            for (int i = 106; i > MazeGenerator.ParameterValidator.LENGTH_UPPER_LIMIT; i--)
            {
                maze.Generate(9, i);
                List<List<int>> listMaze = maze.GetListMaze();
                Assert.AreEqual(0, listMaze.Count);
            }
        }

        [TestMethod]
        public void GetListMaze_EqualParameterValues()
        {
            maze.Generate(5, 5);
            for (int i = -3; i <= 3; i++)
            {
                List<List<int>> listMaze = maze.GetListMaze(i, i);
                Assert.AreEqual(MazeGenerator.WALL_CODE_DEFAULT_VALUE, listMaze[0][0]);
                Assert.AreEqual(MazeGenerator.FLOOR_CODE_DEFAULT_VALUE, listMaze[1][1]);
            }

        }

        [TestMethod]
        public void GetArrayMaze_EqualParameterValues()
        {
            maze.Generate(5, 5);
            for (int i = -3; i <= 3; i++)
            {
                int[,] arrayMaze = maze.GetArrayMaze(i, i);
                Assert.AreEqual(MazeGenerator.WALL_CODE_DEFAULT_VALUE, arrayMaze[0, 0]);
                Assert.AreEqual(MazeGenerator.FLOOR_CODE_DEFAULT_VALUE, arrayMaze[1, 1]);
            }
        }

    }
}
