using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban_solver.Test
{
    [TestFixture]
    class TestSearchThreeMethods
    {
        private const int YRows = 3;
        private const int XCollums = 4;

        public static string[,] map = new string[YRows, XCollums] {
                                               { "G", "D", "+", "W" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "+" } };

        int[] startPosition = new int[2] { 1, 1 };

        [Test]
        public void isStateSavedTest()
        {
            Program.ListOfStates.Add(new State(map));

            var test = new MovementHandler(map, startPosition);
            Assert.IsFalse(Program.isStateSaved(test));
        }
    }
}
