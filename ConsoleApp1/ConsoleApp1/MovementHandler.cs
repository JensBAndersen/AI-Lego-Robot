using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class MovementHandler
    {
        public readonly string[,] CurrentMap;
        public readonly int[] posistion;

        public MovementHandler(string[,] startingMap, int[] startingPosistion)
        {
            Array.Copy(startingMap, 0, CurrentMap, 0, startingMap.Length);
            posistion = (int[])startingPosistion.Clone();
        }

        public void makeMove(string direction)
        {
            int[] oldPosistion = new int[2];
            oldPosistion[0] = posistion[0];
            oldPosistion[1] = posistion[1];

            switch (direction)
            {
                case "F":
                    posistion[0]--;
                    if (!IsMoveValid(posistion, direction))

                    updateMap(oldPosistion, posistion);
                    break;
                case "B":
                    posistion[0]++;
                    if (!IsMoveValid(posistion, direction))

                    updateMap(oldPosistion, posistion);
                    break;

                case "R":
                    posistion[1]++;
                    if (!IsMoveValid(posistion, direction))

                    updateMap(oldPosistion, posistion);
                    break;

                case "L":
                    posistion[1]--;
                    if (!IsMoveValid(posistion, direction))

                    updateMap(oldPosistion, posistion);
                    break;

                default:
                    break;
            }
        }


        private bool canDiamondMove(int[] posistion, string direction)
        {
            switch (direction)
            {
                case "F":
                    posistion[0] -= 2;
                    if (canDimondBePushed(posistion, direction))
                        return true;
                    break;
                case "B":
                    posistion[0] += 2;
                    if (canDimondBePushed(posistion, direction))
                        return true;
                    break;
                case "R":
                    posistion[0] += 2;
                    if (canDimondBePushed(posistion, direction))
                        return true;
                    break;
                case "L":
                    posistion[0] -= 2;
                    if (canDimondBePushed(posistion, direction))
                        return true;
                    break;
            }
            return false;
        }



        private bool IsMoveValid(int[] posistion, string direction = "")
        {
            switch (MapHandler.readMap(posistion))
            {
                case "+":
                    return true;
                case "W":
                    return false;
                case "D":
                    return canDiamondMove(posistion, direction);
                case "G":
                    return true;
                default:
                    return false;
            }
        }

        private bool canDimondBePushed(int[] posistion, string direction = "")
        {

            switch (direction)
            {
                case "F":
                    posistion[0]--;
                    break;
                case "B":
                    posistion[0]++;
                    break;
                case "R":
                    posistion[1]++;
                    break;
                case "L":
                    posistion[1]--;
                    break;
                default:
                    break;
            }

            switch (MapHandler.readMap(posistion))
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

            if (MapHandler.readMap(newPosistion, CurrentMap) == "D")
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
                    upDatePositionWithDiamond(newPosistion, "DOWN" );
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
    }
}
