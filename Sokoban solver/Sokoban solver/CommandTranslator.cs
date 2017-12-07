using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_solver
{
    public static class CommandTranslator
    {
        public static string dir = "N";

        private static string Translate(string CommandString)
        {
            string RobotCommands = "";
            for (int i = 0; i < CommandString.Length; i++)
            {
                RobotCommands += translate(CommandString[i].ToString());
            }
            return RobotCommands;
        }

        public static void ReadCommandsToFile(string CommandString)
        {
            string text = Translate(CommandString);
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(@"C:\Users\Jens Andersen\Desktop\testJens.txt", text);
        }

        private static string translate(string Command)
        {

            switch (dir)
            {
                case "N":
                    {
                        switch (Command)
                        {
                            case "F":
                                return "F";
                            case "B":
                                return "B";
                            case "R":
                                dir = "E";
                                return "RF";
                            case "L":
                                dir = "W";
                                return "LF";
                        }
                        break;
                    }

                case "S":
                    {
                        switch (Command)
                        {
                            case "F":
                                return "B";
                            case "B":
                                return "F";
                            case "R":
                                dir = "E";
                                return "LF";
                            case "L":
                                dir = "W";
                                return "RF";
                        }
                        break;
                    }

                case "W":
                    {
                        switch (Command)
                        {
                            case "F":
                                return "LF";
                            case "B":
                                return "RF";
                            case "R":
                                dir = "E";
                                return "F";
                            case "L":
                                dir = "W";
                                return "B";
                        }
                        break;
                    }

                case "E":
                    {
                        switch (Command)
                        {
                            case "F":
                                return "RF";
                            case "B":
                                return "LF";
                            case "R":
                                dir = "E";
                                return "B";
                            case "L":
                                dir = "W";
                                return "F";
                        }
                        break;
                    }

                default:
                    return "";
            }
            return "";
        }
    }
}
