using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_solver
{
    public class State
    {
        public string[,] savedMap { get; private set; }

        public State(string[,] newMap)
        {
            savedMap = new string[newMap.GetLength(0), newMap.GetLength(1)];

            savedMap = (string[,])newMap.Clone();
        }
    }
}
