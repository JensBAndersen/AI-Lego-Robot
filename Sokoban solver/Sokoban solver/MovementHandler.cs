using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_solver
{
    public class MovementHandler
    {
        public readonly string[,] CurrentMap;
        public int[] posistion { get; private set; }
        public readonly string Moves;

        public MovementHandler(string[,] startingMap, int[] startingPosistion)
        {
            CurrentMap = new string[startingMap.GetLength(0),startingMap.GetLength(1)];
            Array.Copy(startingMap, 0, CurrentMap, 0, startingMap.Length);
            posistion = (int[])startingPosistion.Clone();
            Moves = string.Empty;
        }

        public void makeMove(string direction)
        {
            int[] oldPosistion = new int[2];
            oldPosistion[0] = posistion[0];
            oldPosistion[1] = posistion[1];

            int[] NewPosistion = new int[2];
            NewPosistion[0] = posistion[0];
            NewPosistion[1] = posistion[1];

            switch (direction)
            {
                case "F":
                    NewPosistion[0]--;
                    if (IsMoveValid(NewPosistion, direction))
                    {
                        updateMap(oldPosistion, NewPosistion);
                        posistion = NewPosistion;
                    }

                    break;

                case "B":
                    NewPosistion[0]++;
                    if (IsMoveValid(NewPosistion, direction))
                    {
                        updateMap(oldPosistion, NewPosistion);
                        posistion = NewPosistion;
                    }

                    break;

                case "R":
                    NewPosistion[1]++;
                    if (IsMoveValid(NewPosistion, direction))
                    {
                        updateMap(oldPosistion, NewPosistion);
                        posistion = NewPosistion;
                    }

                    break;

                case "L":
                    NewPosistion[1]--;
                    if (IsMoveValid(NewPosistion, direction))
                    {
                        updateMap(oldPosistion, NewPosistion);
                        posistion = NewPosistion;
                    }

                    break;

                default:
                    break;
            }
        }


        private bool canDiamondMove(string direction)
        {
            int[] currentPosition = new int[2];
            currentPosition = (int[])posistion.Clone();
            switch (direction)
            {
                case "F":
                    currentPosition[0] -= 2;
                    if (canDimondBePushed(currentPosition))
                        return true;
                    break;
                case "B":
                    currentPosition[0] += 2;
                    if (canDimondBePushed(currentPosition))
                        return true;
                    break;
                case "R":
                    currentPosition[1] += 2;
                    if (canDimondBePushed(currentPosition))
                        return true;
                    break;
                case "L":
                    currentPosition[1] -= 2;
                    if (canDimondBePushed(currentPosition))
                        return true;
                    break;
            }
            return false;
        }



        private bool IsMoveValid(int[] NewPosistion, string direction)
        {
            switch (readMap(NewPosistion))
            {
                case "+":
                    return true;
                case "W":
                    return false;
                case "D":
                    return canDiamondMove(direction);
                case "G":
                    return true;
                default:
                    return false;
            }
        }

        private bool canDimondBePushed(int[] newPosition)
        {

            switch (readMap(newPosition))
            {
                case "+":
                    return true;
                case "W":
                    return false;
                case "D":
                    return false;
                case "G":
                    return true;
                default:
                    return false;
            }

        }

        private void updateMap(int[] oldPosistion, int[] newPosistion)
        {
            string[,] newMap = (string[,])CurrentMap.Clone();

            if (readMap(newPosistion) == "D")
            {
                if (oldPosistion[0] < newPosistion[0])
                {
                    upDatePositionWithDiamond(newPosistion, "RIGHT");
                }
                else if (oldPosistion[0] > newPosistion[0])
                {
                    upDatePositionWithDiamond(newPosistion, "LEFT");
                }
                else if (oldPosistion[1] < newPosistion[1])
                {
                    upDatePositionWithDiamond(newPosistion, "UP");
                }
                else if (oldPosistion[1] > newPosistion[1])
                {
                    upDatePositionWithDiamond(newPosistion, "DOWN");
                }
            }

            if (MapHandler.readStaticMap(oldPosistion) == "G")
            {
                CurrentMap[oldPosistion[0], oldPosistion[1]] = "G";
            }

            CurrentMap[newPosistion[0], newPosistion[1]] = "P";

        }

        private void upDatePositionWithDiamond(int[] newPosition, string dir)
        {
            int[] newDiamondPosition = newPosition;
            newDiamondPosition[0] = newPosition[0];
            newDiamondPosition[1] = newPosition[1];

            switch (dir)
            {
                case "UP":
                    newDiamondPosition[1]--;
                    break;
                case "DOWN":
                    newDiamondPosition[1]++;
                    break;
                case "RIGHT":
                    newDiamondPosition[0]++;
                    break;
                case "LEFT":
                    newDiamondPosition[0]--;
                    break;
                default:
                    return;
            }
            CurrentMap[newDiamondPosition[0], newDiamondPosition[1]] = "D";
        }

        private string readMap(int[] readPosistion)
        {
            if (readPosistion[0] < 0 || readPosistion[1] < 0 || readPosistion[0] >= CurrentMap.GetLength(0) || readPosistion[1] >= CurrentMap.GetLength(1))
                return "Not in side the map";

            else return CurrentMap[readPosistion[0], readPosistion[1]];
        }

        public string TestreadMap(int[] readPosistion)
        {
            return readMap(readPosistion);
        }

        public bool TestIsMoveValid(int[] readPosistion, string dire)
        {
            return IsMoveValid(readPosistion, dire);
        }
    }
}
