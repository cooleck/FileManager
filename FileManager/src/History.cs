using System;
using System.IO;

namespace FileManager
{
    public static class History
    {
        public static void PrintHistory()
        {
            for (int i = 0; i < 1000; ++i)
            {
                Console.WriteLine('\n');
            }
            
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
            using (StreamWriter historyFile = new StreamWriter(@"OperationsHistory.txt", true))
            {

                foreach (var operation in Menu.operationsDict)
                {
                    if (operation.Key == cursorItr)
                    {
                        historyFile.Write($"{operation.Key + 1}.", true);
                        historyFile.WriteLine($" {operation.Value} \t *");
                    }
                    else
                    {
                        historyFile.WriteLine("{0}. {1}", operation.Key + 1, operation.Value);
                    }
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
            using (StreamWriter historyFile = new StreamWriter(@"OperationsHistory.txt", true))
            {
                historyFile.WriteLine(str);
            }
        }
    }
}