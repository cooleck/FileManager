using System;
using System.IO;

namespace FileManager
{
    public static class History
    {
        public static void PrintHistory()
        {
            Console.Clear();
            using (StreamReader historyFile = new StreamReader(@"OperationsHistory.txt"))
            {
                string s;
                while ((s = historyFile.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public static void AddMenuToHistory(int cursorItr)
        {
            using (FileStream historyFile = new FileStream(@"OperationsHistory.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(historyFile))
                {
                    TextWriter consoleOut = Console.Out;
                    Menu.PrintListOfOperations(cursorItr, writer);
                    Console.SetOut(consoleOut);
                }
            }
        }

        public static void WriteLineBoth(string str)
        {
            Console.WriteLine(str);
            WriteLine(str);
        }

        public static void WriteLine(string str)
        {
            using (FileStream historyFile = new FileStream(@"OperationsHistory.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(historyFile))
                {
                    TextWriter consoleOut = Console.Out;
                    Console.SetOut(writer);
                    Console.WriteLine(str);
                    Console.SetOut(consoleOut);
                }
            }
        }
    }
}