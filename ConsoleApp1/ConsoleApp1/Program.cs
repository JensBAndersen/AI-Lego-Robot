using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static string[,] map = new string[3, 3] { { "G", "D", "+" },
                                               { "+", "P", "+" },
                                               { "+", "W", "+"} };

        public static List<State> ListOfStates = new List<State>();



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] startPosition = new int[2] { 1, 1 };

            Console.WriteLine("Posistion: " + MapHandler.readMap(startPosition));
            Console.ReadLine();

            //int[] newPosition = MovementHandler.makeMove(startPosition, "L");

            //nextStep(startPosition);

            Console.ReadLine();

            //position = MovementHandler.makeMove(startPosition, "F");
            //Console.WriteLine("Posistion: " + MapHandler.readMap(position));
            //Console.WriteLine(position[0] + " " + position[1]);
            Console.ReadLine();
        }

        //private static List<int[]> nextStep(int[] position)
        //{
        //    List<int[]> list = new List<int[]>();

        //    int[] newPositionUP = MovementHandler.makeMove(position, "F");
        //    if(Enumerable.SequenceEqual(newPositionUP, position))
        //    {
        //        if (isStateSaved())
        //        {

        //        }
        //        ListOfStates.Add(new State());
        //        list.Add(newPositionUP);
        //    }
                

        //    int[] newPositionDOWN = MovementHandler.makeMove(position, "B");
        //    if (Enumerable.SequenceEqual(newPositionDOWN, position))
        //    {
        //        list.Add(newPositionDOWN);
        //    }

        //    int[] newPositionRIGHT = MovementHandler.makeMove(position, "R");
        //    if (Enumerable.SequenceEqual(newPositionRIGHT, position))
        //    {
        //        list.Add(newPositionRIGHT);
        //    }

        //    int[] newPositionLEFT = MovementHandler.makeMove(position, "L");
        //    if (Enumerable.SequenceEqual(newPositionLEFT, position))
        //    {
        //        list.Add(newPositionLEFT);
        //    }

        //    return list;
        //}

        private static bool isStateSaved(State state)
        {
            foreach(State item in ListOfStates)
            {
                var equal = item.mapstate.Rank == state.mapstate.Rank &&
                        Enumerable.Range(0, item.mapstate.Rank).All(dimension => item.mapstate.GetLength(dimension) == state.mapstate.GetLength(dimension)) &&
                        item.mapstate.Cast<double>().SequenceEqual(state.mapstate.Cast<double>());
                if (equal)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<int[]> SearchThree(List<int[]> list)
        {
            List<int[]> nextRoundList = new List<int[]>();

            foreach (int[] position in list)
            {
                //nextRoundList.AddRange(nextStep(position));
            }

            return SearchThree(nextRoundList);
        }
    }
}
