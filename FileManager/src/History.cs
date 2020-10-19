using System;
using System.IO;
using System.Collections.Generic;

namespace FileManager
{
    public static class History
    {
        private static string space = "";

        public static void MakeSep(int cnt)
        {
            for (int i = 0; i < cnt; ++i)
            {
                space += '\n';
            }
        }

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
            using (StreamWriter historyFile = new StreamWriter(Config.historyPath, true))
            {
                historyFile.Write(str);
            }
        }

        public static void WriteLine(Object str)
        {
            using (StreamWriter historyFile = new StreamWriter(Config.historyPath, true))
            {
                historyFile.WriteLine(str);
            }
        }
        
        public static void CleanHistory()
        {
            File.WriteAllText(Config.historyPath, String.Empty);
        }
    }
}