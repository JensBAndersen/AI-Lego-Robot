using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class State
    {
        public int[,] mapstate { get; private set; }

        public State(int[,] newMap)
        {
            mapstate = new int[Program.map.GetLength(0), Program.map.GetLength(1)];
  
            Array.Copy(newMap, 0, mapstate, 0, newMap.Length);
        }
    }
}
