using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FileManager
{
    /// <summary>
    /// Общий класс для вывода меню выбора различных опций.
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Реализация вывода меню выбора опций.
        /// </summary>
        /// <param name="optionList">Список опций.</param>
        /// <param name="cursorItr">Порядковый номер выбранной опции.</param>
        /// <param name="consoleOut">Поток для смены стандартного потока вывода.</param>
        /// <param name="isHistory">Флаг для проверки записи в историю действий.</param>
        public static void PrintListOfOptions(List<string> optionList, int cursorItr, TextWriter consoleOut,
            bool isHistory = false)
        {
            // Смена стандартного потока вывода.
            Console.SetOut(consoleOut);

            int itr = 0;

            // Вывод меню по значению optionList.
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

            Console.WriteLine();
        }

        /// <summary>
        /// Вызывает фукнцию вывода меню и обновляет его
        /// при выборе опции, смене выбранной опции,
        /// при нажатии Esc.
        /// </summary>
        /// <param name="optionsList">Список опций для выбора.</param>
        /// <param name="startMessage">Сообщение в начале вывода меню.</param>
        /// <param name="finishMessage">Сообщение в конце вывода меню.</param>
        /// <returns></returns>
        public static int PrintMenu(List<string> optionsList, string startMessage = "", string finishMessage = "")
        {
            Console.CursorVisible = false;
            int cursorItr = 0;
            int optionsListLength = optionsList.Count;

            if (optionsListLength == 0)
            {
                History.WriteLineBoth(Errors.EmptyListError);
                Console.CursorVisible = true;
                return -1;
            }

            // Ожидание выбора пользователя.
            while (true)
            {
                History.PrintHistory();
                Console.Write(startMessage);
                Console.WriteLine(Directory.GetCurrentDirectory());
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
                        History.Write(startMessage);
                        History.WriteLine(Directory.GetCurrentDirectory());
                        History.AddMenuToHistory(optionsList, cursorItr);
                        Console.CursorVisible = true;
                        return cursorItr;

                    case ConsoleKey.Escape:
                        History.AddMenuToHistory(optionsList, optionsListLength);
                        History.WriteBoth(finishMessage);
                        Console.CursorVisible = true;
                        return -1;
                }
            }
        }
    }
}