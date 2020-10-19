using System;
using System.IO;

namespace FileManager
{
    public static class History
    {
        private static string sep = "";

        public static void MakeSep(int cnt)
        {
            for (int i = 0; i < cnt; ++i)
            {
                sep += '\n';
            }
        }

        public static void PrintHistory()
        {
            Console.WriteLine(sep);
            
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
            TextWriter consoleOut = Console.Out;
            
            using (FileStream historyFile = new FileStream(@"OperationsHistory.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(historyFile))
                {
                    Menu.PrintListOfOperations(cursorItr, writer, true);
                }
            }
            
            Console.SetOut(consoleOut);
        }

        public static void WriteLineBoth(string str)
        {
            Console.WriteLine(str);
            WriteLine(str);
        }

        public static void WriteLine(string str)
        {
            using (StreamWriter historyFile = new StreamWriter(@"OperationsHistory.txt", true))
            {
                historyFile.WriteLine(str);
            }
        }
        
        public static void CleanHistory()
        {
            File.WriteAllText(@"OperationsHistory.txt", String.Empty);
        }
    }
}