using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sokoban_solver.Test
{
    [TestFixture]
    class TestMovementHandler
    {
        private const int YRows = 3;
        private const int XCollums = 4;

        public static string[,] map = new string[YRows, XCollums] { 
                                               { "G", "D", "+", "W" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "+" } };

        int[] startPosition = new int[2] { 1, 1 };

        [Test]
        public void TestMap()
        {
            Assert.AreEqual(3, map.GetLength(0));
            Assert.AreEqual(4, map.GetLength(1));

        }

        [Test]
        public void ConstructMovementHandlerTest()
        {
            var test = new MovementHandler(map, startPosition);

            Assert.IsTrue(test.TestreadMap(test.posistion) == "P");
            Assert.IsTrue(test.posistion[0] == 1 && test.posistion[1] == 1);
            Assert.IsTrue(test.Moves == "");

        }

        [Test]
        public void MakeOneMoveTest_NotAValidMove()
        {
            var test = new MovementHandler(map, startPosition);

            test.makeMove("F");
            Assert.AreEqual(test.TestreadMap(test.posistion), "P");
            Assert.AreEqual(test.posistion[0], 1);
            Assert.AreEqual(test.posistion[1], 1);
            Assert.AreEqual(test.Moves, "");

            test.makeMove("B");
            Assert.AreEqual(test.TestreadMap(test.posistion), "P");
            Assert.AreEqual(test.posistion[0], 1);
            Assert.AreEqual(test.posistion[1], 1);
            Assert.AreEqual(test.Moves, "");

        }

        [Test]
        public void MakeOneMoveTest_AValidMove()
        {
            var test = new MovementHandler(map, startPosition);

            test.makeMove("R");
            var teststring = test.TestreadMap(test.posistion);
            Assert.AreEqual(teststring, "P");
            Assert.AreEqual(test.posistion[0], 1);
            Assert.AreEqual(test.posistion[1], 1);
            Assert.AreEqual(test.Moves, "");

            test = new MovementHandler(map, startPosition);

            test.makeMove("L");
            teststring = test.TestreadMap(test.posistion);
            Assert.AreEqual(teststring, "P");
            Assert.AreEqual(test.posistion[0], 1);
            Assert.AreEqual(test.posistion[1], 0);
            Assert.AreEqual(test.Moves, "");

        }

        [Test]
        public void ReadMapTest()
        {
            var test = new MovementHandler(map, startPosition);
            // Acctual map
            Assert.AreEqual(map[0, 0], "G");

            Assert.AreEqual(map[2, 2], "+");

            Assert.AreEqual(map[0, 1], "D");

            Assert.AreEqual(map[2, 1], "W");

            // StateMap
            Assert.AreEqual(test.TestreadMap(new int[] { 0, 0 }), "G");

            Assert.AreEqual(test.TestreadMap(new int[] { 2, 2 }), "+");

            Assert.AreEqual(test.TestreadMap(new int[] { 0, 1 }), "D");

            Assert.AreEqual(test.TestreadMap(new int[] { 2, 1 }), "W");


        }
    }
}
