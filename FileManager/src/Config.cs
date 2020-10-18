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

    public static class Operaions
    {
        public static Dictionary<string, string> operationsDict = new Dictionary<string, string>()
        {
            ["operationListOfDisks"] = "Просмотр списка дисков компьютера и выбор диска.",
            ["operationCdDir"] = "Переход в другую директорию (выбор папки).",
            ["operationLs"] = "Просмотр списка файлов в директории.",
            ["operationCatUTF-8"] = "Вывод содержимого текстового файла в консоль в кодировке UTF-8."
        };

        private static int PrintListOfOperations(int cursorItr)
        {
            int itr = 0;
            int cursorPosition = -1;
            
            foreach (var opetaion in operationsDict)
            {
                if (itr == cursorItr)
                {
                    cursorPosition = Console.CursorTop;
                }

                Console.WriteLine("{0}. {1}", itr + 1, opetaion.Value);
                itr++;
            }

            return cursorPosition;
        }

        public static int operationMenu()
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