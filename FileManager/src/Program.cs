using System;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            History.CleanHistory();
            History.MakeSep(1000);
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
            }
        }
    }
}