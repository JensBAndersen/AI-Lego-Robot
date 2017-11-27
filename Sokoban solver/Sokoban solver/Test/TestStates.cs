using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban_solver.Test
{
    [TestFixture]
    class TestStates
    {
        private const int YRows = 3;
        private const int XCollums = 4;

        string[,] solvedMap = new string[YRows, XCollums] {
                                               { "D", "+", "+", "W" },
                                               { "+", "P", "+", "+" },
                                               { "+", "W", "+", "D" } };

        public static string[,] map = new string[YRows, XCollums] {
                                               { "G", "D", "+", "W" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "+" } };

        string[,] solvedMap2 = new string[YRows, XCollums] {
                                               { "D", "+", "+", "W" },
                                               { "+", "P", "+", "+" },
                                               { "+", "W", "+", "D" } };

        public static string[,] map2 = new string[YRows, XCollums] {
                                               { "G", "D", "+", "W" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "+" } };

        [Test]
        public void TestEquals()
        {
            State test1 = new State(solvedMap);
            State test2 = new State(map);

            State test3 = new State(solvedMap2);
            State test4 = new State(map2);

            Assert.AreEqual(-707971743, test1.ToString().GetHashCode());
            Assert.AreEqual(-707971743, test3.ToString().GetHashCode());

            Assert.IsFalse(test1.Equals(test2));
            Assert.IsTrue(test1.Equals(test3));
            Assert.IsTrue(test2.Equals(test4));
        }
    }
}
