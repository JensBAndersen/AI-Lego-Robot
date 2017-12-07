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
                var test = translate(CommandString[i].ToString());
                RobotCommands += test;
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
                            case "f":
                                return "s";
                            case "F":
                                return "F";
                            case "b":
                                dir = "S";
                                return "LLs";
                            case "B":
                                dir = "S";
                                return "LLF";
                            case "r":
                                dir = "E";
                                return "Rs";
                            case "R":
                                dir = "E";
                                return "RF";
                            case "l":
                                dir = "W";
                                return "Ls";
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
                            case "f":
                                dir = "N";
                                return "LLs";
                            case "F":
                                dir = "N";
                                return "LLF";
                            case "b":
                                return "s";
                            case "B":
                                return "F";
                            case "r":
                                dir = "E";
                                return "Ls";
                            case "R":
                                dir = "E";
                                return "LF";
                            case "l":
                                dir = "W";
                                return "Rs";
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
                            case "f":
                                dir = "N";
                                return "Rs";
                            case "F":
                                dir = "N";
                                return "RF";
                            case "b":
                                dir = "S";
                                return "Ls";
                            case "B":
                                dir = "S";
                                return "LF";
                            case "r":
                                dir = "E";
                                return "LLs";
                            case "R":
                                dir = "E";
                                return "LLF";
                            case "l":
                                return "s";
                            case "L":
                                return "F";
                        }
                        break;
                    }

                case "E":
                    {
                        switch (Command)
                        {
                            case "f":
                                dir = "N";
                                return "Ls";
                            case "F":
                                dir = "N";
                                return "LF";
                            case "b":
                                dir = "S";
                                return "Rs";
                            case "B":
                                dir = "S";
                                return "RF";
                            case "r":
                                dir = "E";
                                return "s";
                            case "R":
                                dir = "E";
                                return "F";
                            case "l":
                                dir = "W";
                                return "LLs";
                            case "L":
                                dir = "W";
                                return "LLF";
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
