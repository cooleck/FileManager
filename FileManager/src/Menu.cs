using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FileManager
{
    public static class Menu
    {
        public static Dictionary<int, string> operationsDict = new Dictionary<int, string>()
        {
            [0] = "Просмотр списка дисков компьютера и выбор диска.",
            [1] = "Переход в другую директорию (выбор папки).",
            [2] = "Просмотр списка файлов в директории.",
            [3] = "Вывод содержимого текстового файла в консоль в кодировке UTF-8."
        };

        public static void PrintListOfOperations(int cursorItr, TextWriter consoleOut, bool isHistory = false)
        {
            Console.SetOut(consoleOut);

            foreach (var operation in operationsDict)
            {
                if (operation.Key == cursorItr)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write($"{operation.Key + 1}.", true);
                    Console.ResetColor();
                    Console.Write($" {operation.Value}");
                    if (isHistory)
                    {
                        Console.Write("\t <<");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0}. {1}", operation.Key + 1, operation.Value);
                }
            }
        }

        public static int OperationMenu()
        {
            Console.CursorVisible = false;
            int cursorItr = 0;
            int dictLength = operationsDict.Count;

            while (true)
            {
                History.PrintHistory();
                Console.WriteLine(Messages.welcomeMessage);
                PrintListOfOperations(cursorItr, Console.Out);
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        cursorItr = (cursorItr - 1 + dictLength) % dictLength;
                        break;

                    case ConsoleKey.DownArrow:
                        cursorItr = (cursorItr + 1) % dictLength;
                        break;

                    case ConsoleKey.Enter:
                        History.WriteLine(Messages.welcomeMessage);
                        History.AddMenuToHistory(cursorItr);
                        return cursorItr;

                    case ConsoleKey.Escape:
                        Console.WriteLine();
                        Console.WriteLine(Messages.byeMessage);
                        return -1;
                }
            }
        }
    }
}