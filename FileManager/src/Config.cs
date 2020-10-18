using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileManager
{
    public static class Messages
    {
        public static string welcomeMessage = "Привет";
        public static string byeMessage = "Пока";
    }

    public static class Menu
    {
        public static Dictionary<int, string> operationsDict = new Dictionary<int, string>()
        {
            [0] = "Просмотр списка дисков компьютера и выбор диска.",
            [1] = "Переход в другую директорию (выбор папки).",
            [2] = "Просмотр списка файлов в директории.",
            [3] = "Вывод содержимого текстового файла в консоль в кодировке UTF-8."
        };

        private static int PrintListOfOperations(int cursorItr)
        {
            int cursorPosition = -1;
            
            foreach (var opetaion in operationsDict)
            {
                if (opetaion.Key == cursorItr)
                {
                    cursorPosition = Console.CursorTop;
                }

                Console.WriteLine("{0}. {1}", opetaion.Key + 1, opetaion.Value);
            }

            return cursorPosition;
        }

        public static int OperationMenu()
        {
            int cursorItr = 0;
            int dictLength = operationsDict.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(Messages.welcomeMessage);
                int cursorPosition = PrintListOfOperations(cursorItr);
                Console.SetCursorPosition(0, cursorPosition);
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
                        Console.SetCursorPosition(0, Console.CursorTop + dictLength - cursorItr + 1);
                        return cursorItr;

                    case ConsoleKey.Escape:
                        Console.SetCursorPosition(0, Console.CursorTop + dictLength - cursorItr + 1);
                        Console.WriteLine(Messages.byeMessage);
                        return -1;
                }
            }
        }
    }
}