using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_solver
{
    public static class MapHandler
    {
        private static string[,] mapStaticObjects = Program.map2;

        public static string readMap(int[] posistion)
        {
            if (posistion[0] < 0 || posistion[1] < 0 || posistion[0] > Program.map2.GetLength(0) || posistion[1] > Program.map2.GetLength(1))
                return "Not in side the map";

            else return Program.map2[posistion[0], posistion[1]];
        }

        public static string readMap(int[] posistion, string[,] currentMap)
        {
            if (posistion[0] < 0 || posistion[1] < 0 || posistion[0] > currentMap.GetLength(0) || posistion[1] > currentMap.GetLength(1))
                return "Not in side the map";

            else return currentMap[posistion[0], posistion[1]];
        }

        public static string readStaticMap(int[] posistion)
        {
            if (posistion[0] < 0 || posistion[1] < 0 || posistion[0] > mapStaticObjects.GetLength(0) || posistion[1] > mapStaticObjects.GetLength(1))
                return "Not in side the map";

            else return mapStaticObjects[posistion[0], posistion[1]];
        }
    }
}
