using System;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.CleanHistory();
            Console.Clear();
            int operationCode;
            while ((operationCode = Menu.OperationMenu()) != -1) {

                switch (operationCode)
                {
                    case 0:
                        History.WriteLineBoth("Check");
                        break;

                    default:
                        throw new Exception("Operation doesn't exist!");
                }
                
                Console.WriteLine(Messages.continueMessage);
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Console.WriteLine(Messages.byeMessage);
                    return;
                }
            }
        }
    }
}