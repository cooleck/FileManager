using System;
using System.IO;
using System.Collections.Generic;

namespace FileManager
{
    public static class History
    {
        private static string _sep = "";

        public static void MakeSep(int cnt)
        {
            for (int i = 0; i < cnt; ++i)
            {
                _sep += '\n';
            }
        }

        public static void PrintHistory()
        {
            Console.WriteLine(_sep);
            
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

        public static void AddMenuToHistory(List<string> optionsList, int cursorItr)
        {
            TextWriter consoleOut = Console.Out;
            
            using (FileStream historyFile = new FileStream(@"OperationsHistory.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(historyFile))
                {
                    Menu.PrintListOfOptions(optionsList, cursorItr, writer, true);
                }
            }
            
            Console.SetOut(consoleOut);
        }

        public static void WriteBoth(Object str)
        {
            Console.Write(str);
            Write(str);
        }
        
        public static void WriteLineBoth(Object str)
        {
            Console.WriteLine(str);
            WriteLine(str);
        }
        
        public static void Write(Object str)
        {
            using (StreamWriter historyFile = new StreamWriter(@"OperationsHistory.txt", true))
            {
                historyFile.Write(str);
            }
        }

        public static void WriteLine(Object str)
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