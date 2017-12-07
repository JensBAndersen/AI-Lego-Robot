using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban_solver.Test
{
    [TestFixture]
    class TestProgramMethods
    {
        private const int YRows = 3;
        private const int XCollums = 4;

        public static string[,] map = new string[YRows, XCollums] {
                                               { "G", "D", "+", "W" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "+" } };

        public static string[,] map2 = new string[YRows, XCollums] {
                                               { "G", "D", "+", "W" },
                                               { "P", "+", "D", "+" },
                                               { "+", "W", "+", "+" } };

        int[] startPosition = new int[2] { 1, 1 };

        [Test]
        public void isStateSavedTest()
        {
            Program.ListOfStates.Add(new State(map).ToString());

            var test = new MovementHandler(map, startPosition);
            Assert.IsFalse(Program.canMapBeSaved(test));

            var test2 = new MovementHandler(map2, startPosition);
            Assert.IsTrue(Program.canMapBeSaved(test2));
        }

        [Test]
        public void isGameSolvedTest()
        {
            // Only works for 2 goal maps
            //string[,] solvedMap = new string[YRows, XCollums] {
            //                                   { "D", "+", "+", "W" },
            //                                   { "+", "P", "+", "+" },
            //                                   { "+", "W", "+", "D" } };


            //var test = new MovementHandler(solvedMap, startPosition);
            //Assert.IsTrue(Program.isGameSolved(test));

            //Test big map
            string[,] map2 = new string[10, 5] {
                                               { "+", "+", "+", "D", "+" },
                                               { "+", "+", "+", "+", "+" },
                                               { "D", "+", "D", "+", "D" },
                                               { "+", "+", "W", "+", "W" },
                                               { "+", "D", "D", "+", "+" },
                                               { "W", "W", "+", "D", "+" },
                                               { "W", "W", "+", "W", "+" },
                                               { "+", "D", "D", "+", "+" },
                                               { "+", "D", "+", "+", "+" },
                                               { "+", "+", "+", "W", "W" }, };

            var test2 = new MovementHandler(map2, startPosition);

            Assert.IsTrue(Program.isGameSolved(test2));
        }
    }
}
