using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

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

        public static void WriteBoth(Object str = null)
        {
            Console.Write(str);
            Write(str);
        }
        
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
        
        public static void Write(Object str = null)
        {
            using (StreamWriter historyFile = new StreamWriter(Config.historyPath, true))
            {
                historyFile.Write(str);
            }
        }

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
        
        public static void CleanHistory()
        {
            File.WriteAllText(Config.historyPath, String.Empty);
        }
    }
}