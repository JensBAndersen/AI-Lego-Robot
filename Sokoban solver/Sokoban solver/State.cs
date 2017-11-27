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

        public bool Equals(State state)
        {
            if (state.ToString().GetHashCode() == this.ToString().GetHashCode())
            {
                //var equal = this.savedMap.Rank == state.savedMap.Rank &&
                //Enumerable.Range(0, this.savedMap.Rank).All(dimension => this.savedMap.GetLength(dimension) == state.savedMap.GetLength(dimension)) &&
                //this.savedMap.Cast<string>().SequenceEqual(state.savedMap.Cast<string>());

                var equal = this.ToString().Equals(state.ToString());

                if (equal)
                {
                    return true;
                }
            }
            return false;
        }

        public new string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in savedMap)
            {
                builder.Append(value);
            }
            return builder.ToString();
        }
    }
}
