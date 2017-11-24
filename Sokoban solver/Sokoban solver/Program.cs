﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_solver
{
    class Program
    {
        public static string[,] map = new string[3, 3] { { "G", "D", "+" },
                                               { "+", "P", "+" },
                                               { "+", "W", "+"} };

        private const int YRows = 3;
        private const int XCollums = 4;
        public static string[,] map2 = new string[YRows, XCollums] {
                                               { "G", "D", "+", "+" },
                                               { "+", "P", "D", "+" },
                                               { "+", "W", "+", "G" } };

        private static int[] G1 = new int[2] { 0, 0 };
        private static int[] G2 = new int[2] { 2, 3 };
        public static int[][] goalLocations = new int[][] { G1, G2 };
        private static bool GameSolved = false;
        private static int SearchThreeCounter = 0;

        public static List<State> ListOfStates = new List<State>();



        static void Main(string[] args)
        {

            startTheRun();
        }

        public static void startTheRun()
        {
            ListOfStates.Add(new State(map2));
            MovementHandler start = new MovementHandler(map2, new int[] { 1, 1 });
            List<MovementHandler> test = new List<MovementHandler>() { start };
            var test2 = SearchThree(test);
            Console.WriteLine(test2.First().Moves);
            Console.ReadLine();
        }

        public static List<MovementHandler> nextSteps(MovementHandler obj)
        {
            List<MovementHandler> list = new List<MovementHandler>();
            int[] oldposition = (int[])obj.posistion.Clone();


            MovementHandler newObjUP = new MovementHandler(obj);

            newObjUP.makeMove("F");
            if (!Enumerable.SequenceEqual(oldposition, newObjUP.posistion))
            {
                if (canMapBeSaved(newObjUP))
                {
                    if (isGameSolved(newObjUP))
                    {
                        GameSolved = true;
                        return new List<MovementHandler>() { newObjUP };
                    }
                    list.Add(newObjUP);
                }
            }

            MovementHandler newObjDOWN = new MovementHandler(obj);
            newObjDOWN.makeMove("B");
            if (!Enumerable.SequenceEqual(oldposition, newObjDOWN.posistion))
            {
                if (canMapBeSaved(newObjDOWN))
                {
                    if (isGameSolved(newObjDOWN))
                    {
                        GameSolved = true;
                        return new List<MovementHandler>() { newObjDOWN };
                    }
                    list.Add(newObjDOWN);
                }
            }

            MovementHandler newObjRight = new MovementHandler(obj);
            newObjRight.makeMove("R");
            if (!Enumerable.SequenceEqual(oldposition, newObjRight.posistion))
            {
                if (canMapBeSaved(newObjRight))
                {
                    if (isGameSolved(newObjRight))
                    {
                        GameSolved = true;
                        return new List<MovementHandler>() { newObjRight };
                    }
                    list.Add(newObjRight);
                }
            }

            MovementHandler newObjLEFT = new MovementHandler(obj);
            newObjLEFT.makeMove("L");
            if (!Enumerable.SequenceEqual(oldposition, newObjLEFT.posistion))
            {
                if (canMapBeSaved(newObjLEFT))
                {
                    if (isGameSolved(newObjLEFT))
                    {
                        GameSolved = true;
                        return new List<MovementHandler>() { newObjLEFT };
                    }
                    list.Add(newObjLEFT);
                }
            }

            return list.ToList();
        }

        public static bool canMapBeSaved(MovementHandler obj)
        {

            foreach (State item in ListOfStates)
            {
                var equal = item.savedMap.Rank == obj.CurrentMap.Rank &&
                        Enumerable.Range(0, item.savedMap.Rank).All(dimension => item.savedMap.GetLength(dimension) == obj.CurrentMap.GetLength(dimension)) &&
                        item.savedMap.Cast<string>().SequenceEqual(obj.CurrentMap.Cast<string>());
                if (equal)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<MovementHandler> SearchThree(List<MovementHandler> list)
        {
            List<MovementHandler> nextRoundList = new List<MovementHandler>();

            foreach (MovementHandler obj in list)
            {
                SearchThreeCounter++;
                Console.WriteLine(SearchThreeCounter);
                nextRoundList.AddRange(nextSteps(obj));
                if (GameSolved)
                {
                    return new List<MovementHandler>() { nextRoundList.Last() };
                }
            }
            if(nextRoundList.Count == 0)
            {
                Console.WriteLine("Can't be solved");
                return new List<MovementHandler>();
            }
            return SearchThree(nextRoundList);
        }

        public static bool isGameSolved(MovementHandler movement)
        {
            foreach(int[] goal in goalLocations)
            {
                if(!(movement.CurrentMap[goal[0],goal[1]] == "D"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
