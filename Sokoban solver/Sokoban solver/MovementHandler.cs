﻿using System;
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
        public string Moves { get; private set; }

        public MovementHandler(string[,] startingMap, int[] startingPosistion)
        {
            CurrentMap = (string[,])startingMap.Clone();
            posistion = (int[])startingPosistion.Clone();
            Moves = string.Empty;
        }
        public MovementHandler(MovementHandler CopyFrom)
        {
            CurrentMap = (string[,])CopyFrom.CurrentMap.Clone();
            posistion = (int[])CopyFrom.posistion.Clone();
            Moves = CopyFrom.Moves;
        }

        public void makeMove(string direction)
        {
            int[] oldPosistion = (int[])posistion.Clone();

            int[] NewPosistion = (int[])posistion.Clone();

            switch (direction)
            {
                case "F":
                    NewPosistion[0]--;
                    switch (IsMoveValid(NewPosistion, direction))
                    {
                        case "t":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "f";
                            break;
                        case "D":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "F";
                            break;
                    }

                    break;

                case "B":
                    NewPosistion[0]++;
                    switch (IsMoveValid(NewPosistion, direction))
                    {
                        case "t":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "b";
                            break;
                        case "D":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "B";
                            break;
                    }


                    break;

                case "R":
                    NewPosistion[1]++;
                    switch (IsMoveValid(NewPosistion, direction))
                    {
                        case "t":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "r";
                            break;
                        case "D":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "R";
                            break;
                    }

                    break;

                case "L":
                    NewPosistion[1]--;
                    switch (IsMoveValid(NewPosistion, direction))
                    {
                        case "t":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "l";
                            break;
                        case "D":
                            updateMap(oldPosistion, NewPosistion);
                            posistion = (int[])NewPosistion.Clone();
                            Moves += "L";
                            break;
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



        private string IsMoveValid(int[] NewPosistion, string direction)
        {
            switch (readMap(NewPosistion))
            {
                case "+":
                    return "t";
                case "W":
                    return "f";
                case "D":
                    if (canDiamondMove(direction))
                    {
                        return "D";
                    }
                    return "f";
                case "G":
                    return "t";
                default:
                    return "f";
            }
        }

        private bool canDimondBePushed(int[] newPosition)
        {

            switch (readMap(newPosition))
            {
                case "+":
                    if (checkForDeadLock(newPosition))
                    {
                        return true;
                    }
                    return false;
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

        public bool testCheckForDeadLock(int[] newPosition)
        {
            return checkForDeadLock(newPosition);
        }

        private bool checkForDeadLock(int[] newPosition)
        {

            var Up = (int[])newPosition.Clone();
            var Down = (int[])newPosition.Clone();
            var Right = (int[])newPosition.Clone();
            var Left = (int[])newPosition.Clone();

            Up[0]++;
            Down[0]--;
            Right[1]++;
            Left[1]--;

            if(readMap(Up) == "W" || readMap(Up) == "Not in side the map")
            {
                if(readMap(Right) == "W" || readMap(Right) == "Not in side the map" || readMap(Left) == "W" || readMap(Left) == "Not in side the map")
                {
                    return false;
                }
            }

            if (readMap(Down) == "W" || readMap(Down) == "Not in side the map")
            {
                if (readMap(Right) == "W" || readMap(Right) == "Not in side the map" || readMap(Left) == "W" || readMap(Left) == "Not in side the map")
                {
                    return false;
                }
            }
            return true;
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
                    upDatePositionWithDiamond(newPosistion, "DOWN");
                }
                else if (oldPosistion[1] > newPosistion[1])
                {
                    upDatePositionWithDiamond(newPosistion, "UP");
                }
            }

            CurrentMap[newPosistion[0], newPosistion[1]] = "P";

            if (MapHandler.readStaticMap(oldPosistion) == "G")
            {
                CurrentMap[oldPosistion[0], oldPosistion[1]] = "G";
            }
            else if (MapHandler.readStaticMap(oldPosistion) == "+" || MapHandler.readStaticMap(oldPosistion) == "D")
            {
                CurrentMap[oldPosistion[0], oldPosistion[1]] = "+";
            }
        }

        private void upDatePositionWithDiamond(int[] newPosition, string dir)
        {
            int[] newDiamondPosition = (int[])newPosition.Clone();

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

        public string TestIsMoveValid(int[] readPosistion, string dire)
        {
            return IsMoveValid(readPosistion, dire);
        }
    }
}
