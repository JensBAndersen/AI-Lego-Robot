using System;
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

        public static List<State> ListOfStates = new List<State>();



        static void Main(string[] args)
        {
            ListOfStates.Add(new State(map));


        }

        public static List<MovementHandler> nextSteps(MovementHandler obj)
        {
            List<MovementHandler> list = new List<MovementHandler>();
            int[] oldposition = (int[])obj.posistion.Clone();


            MovementHandler newObjUP = obj;
            newObjUP.makeMove("F");
            if (!Enumerable.SequenceEqual(oldposition, newObjUP.posistion))
            {
                if (isStateSaved(newObjUP))
                {
                    list.Add(newObjUP);
                }
            }

            MovementHandler newObjDOWN = obj;
            newObjDOWN.makeMove("B");
            if (!Enumerable.SequenceEqual(oldposition, newObjDOWN.posistion))
            {
                list.Add(newObjDOWN);
            }

            MovementHandler newObjRight = obj;
            newObjRight.makeMove("R");
            if (!Enumerable.SequenceEqual(oldposition, newObjRight.posistion))
            {
                list.Add(newObjRight);
            }

            MovementHandler newObjLEFT = obj;
            newObjLEFT.makeMove("L");
            if (!Enumerable.SequenceEqual(oldposition, newObjLEFT.posistion))
            {
                list.Add(newObjLEFT);
            }

            return list;
        }

        public static bool isStateSaved(MovementHandler obj)
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
                nextRoundList.AddRange(nextSteps(obj));
            }

            return SearchThree(nextRoundList);
        }
    }
}
