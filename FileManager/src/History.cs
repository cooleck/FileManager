using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс работы с историей действий.
    /// </summary>
    public static class History
    {
        private static string space = "";

        /// <summary>
        /// Вывод пробелов после Console.Clear().
        /// </summary>
        /// <param name="cnt"></param>
        public static void MakeSep(int cnt)
        {
            for (int i = 0; i < cnt; ++i)
            {
                space += '\n';
            }
        }

        /// <summary>
        /// Вывод истории действий.
        /// </summary>
        public static void PrintHistory()
        {
            Console.WriteLine(space);

            Console.Clear();

            using (StreamReader historyFile = new StreamReader(Config.historyPath))
            {
                string s;
                while ((s = historyFile.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Добавить меню выбора в историю.
        /// </summary>
        /// <param name="optionsList">Список опций добавляемого меню.</param>
        /// <param name="cursorItr">Номер выбранного элемента.</param>
        public static void AddMenuToHistory(List<string> optionsList, int cursorItr)
        {
            TextWriter consoleOut = Console.Out;

            using (FileStream historyFile = new FileStream(Config.historyPath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(historyFile))
                {
                    Menu.PrintListOfOptions(optionsList, cursorItr, writer, true);
                }
            }

            Console.SetOut(consoleOut);
        }

        // Записать Object в историю действий и вывести в стандартный поток
        // без переноса каретки на новую строку.
        public static void WriteBoth(Object str = null)
        {
            Console.Write(str);
            Write(str);
        }

        // Записать Object в историю действий и вывести в стандартный поток
        // с переносом каретки на новую строку.
        public static void WriteLineBoth(Object str = null, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Config.systemEncoding;
            }

            Console.OutputEncoding = encoding;
            Console.WriteLine(str);
            Console.OutputEncoding = Config.systemEncoding;
            WriteLine(str, encoding);
        }

        // Записать Object в историю действий
        // без переноса каретки на новую строку.
        public static void Write(Object str = null)
        {
            using (StreamWriter historyFile = new StreamWriter(Config.historyPath, true))
            {
                historyFile.Write(str);
            }
        }

        // Записать Object в историю действий
        // с переносом каретки на новую строку.
        public static void WriteLine(Object str = null, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Config.systemEncoding;
            }

            using (StreamWriter historyFile = new StreamWriter(Config.historyPath, true, encoding))
            {
                historyFile.WriteLine(str);
            }
        }

        // Очистить историю.
        public static void CleanHistory()
        {
            File.WriteAllText(Config.historyPath, String.Empty);
        }
    }
}