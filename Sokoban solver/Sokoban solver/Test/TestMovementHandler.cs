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
            Assert.AreNotEqual(test.Moves, "F");

            test.makeMove("B");
            Assert.AreEqual(test.TestreadMap(test.posistion), "P");
            Assert.AreEqual(test.posistion[0], 1);
            Assert.AreEqual(test.posistion[1], 1);
            Assert.AreNotEqual(test.Moves, "B");

        }

        [Test]
        public void MakeOneMoveTest_AValidMove()
        {
            var test = new MovementHandler(map, startPosition);

            test.makeMove("R");
            var teststring = test.TestreadMap(test.posistion);
            // move is a deadlock
            Assert.AreEqual(teststring, "P");
            Assert.AreEqual(1, test.posistion[0]);
            Assert.AreEqual(1, test.posistion[1]);
            Assert.AreEqual(test.Moves, "");

            test = new MovementHandler(map, startPosition);

            test.makeMove("L");
            teststring = test.TestreadMap(test.posistion);
            Assert.AreEqual(teststring, "P");
            Assert.AreEqual(1, test.posistion[0]);
            Assert.AreEqual(0, test.posistion[1]);
            Assert.AreEqual(test.Moves, "L");

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

        [Test]
        public void SaveMovesTest()
        {
            var test = new MovementHandler(Program.map2, Program.StartingPosition);
            test.makeMove("R");
            test.makeMove("F");
            test.makeMove("R");

            Assert.AreEqual("RFR", test.Moves);


        }

        [Test]
        public void DeadLockTest()
        {
            var test = new MovementHandler(map, startPosition);
            Assert.IsFalse(test.testCheckForDeadLock(new int[] { 2, 0 }));
            Assert.IsFalse(test.testCheckForDeadLock(new int[] { 2, 3 }));
            Assert.IsTrue(test.testCheckForDeadLock(new int[] { 0, 1 }));
            Assert.IsFalse(test.testCheckForDeadLock(new int[] { 2, 2 }));
            Assert.IsTrue(test.testCheckForDeadLock(new int[] { 1, 1 }));
        }

        [Test]
        public void testMoves()
        {
            MovementHandler start = new MovementHandler(Program.map2, new int[] { 8, 2 });
            List<MovementHandler> test = new List<MovementHandler>() { start };

            foreach (int[] goal in Program.goalLocations)
            {
                if (MapHandler.readMap(goal) != "G")
                {
                    return;
                }
            }

            if (MapHandler.readMap(new int[] { 8, 2 }) != "+")
            {
                return;
            }

            Assert.IsTrue(true);
            start.makeMove("F");
            Assert.AreEqual("F", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBB", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLL", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFF", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFR", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRR", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBR", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRR", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFF", start.Moves);
            //Assert.IsTrue(Program.ListOfStates.Contains("FBBLLFFRRBRRFFF"));
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFF", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFL", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFF", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLL", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBR", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRR", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBB", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLL", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBR", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRR", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFF", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBR", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRR", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFF", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFL", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFF", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLL", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLB", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBR", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRB", start.Moves);
            start.makeMove("R");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBR", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRF", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRFF", start.Moves);
            start.makeMove("B");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRFFB", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRFFBL", start.Moves);
            start.makeMove("L");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRFFBLL", start.Moves);
            start.makeMove("F");
            Assert.AreEqual("FBBLLFFRRBRRFFFFLFFLLLBBRRBBBLLBBRRFFFFBBBRRFFFLFFFLLBBRBRFFBLLF", start.Moves);
            
        }
    }
}
