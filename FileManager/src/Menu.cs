using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FileManager
{
    public static class Menu
    {
        public static void PrintListOfOptions(List<string> optionList, int cursorItr, TextWriter consoleOut,
            bool isHistory = false)
        {
            Console.SetOut(consoleOut);

            int itr = 0;
            
            foreach (var option in optionList)
            {
                if (itr == cursorItr)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write($"{itr + 1}.", true);
                    Console.ResetColor();
                    Console.Write($" {option}");
                    if (isHistory)
                    {
                        Console.Write("\t <<");
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0}. {1}", itr + 1, option);
                }

                itr++;
            }
        }

        public static int PrintMenu(List<string> optionsList, string startMessage)
        {
            Console.CursorVisible = false;
            int cursorItr = 0;
            int optionsListLength = optionsList.Count;

            while (true)
            {
                History.PrintHistory();
                Console.WriteLine(startMessage);
                PrintListOfOptions(optionsList, cursorItr, Console.Out);
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        cursorItr = (cursorItr - 1 + optionsListLength) % optionsListLength;
                        break;

                    case ConsoleKey.DownArrow:
                        cursorItr = (cursorItr + 1) % optionsListLength;
                        break;

                    case ConsoleKey.Enter:
                        History.WriteLine(startMessage);
                        History.AddMenuToHistory(optionsList, cursorItr);
                        Console.CursorVisible = true;
                        return cursorItr;

                    case ConsoleKey.Escape:
                        Console.WriteLine();
                        Console.WriteLine(Messages.byeMessage);
                        Console.CursorVisible = true;
                        return -1;
                }
            }
        }
    }
}