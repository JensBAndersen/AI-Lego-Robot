using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public static class MapHandler
    {
        private static string[,] mapStaticObjects = new string[3, 3] { { "G", "+", "+" },
                                                                       { "+", "+", "+" },
                                                                       { "+", "W", "+" } };

        public static string readMap(int[] posistion)
        {
            if ( posistion[0] < 0 || posistion[1] < 0 || posistion[0] > Program.map.GetLength(0) || posistion[1] > Program.map.GetLength(1))
                return "Not in side the map";

            else return Program.map[posistion[0], posistion[1]];
        }

        public static string readMap(int[] posistion, string[,]currentMap)
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