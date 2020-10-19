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
            while ((operationCode = Operations.PrintOperationsMenu()) != -1) {

                switch (operationCode)
                {
                    case 0:
                        Operations.DriveCd();
                        break;

                    case 1:
                        Operations.DirCd();
                        break;
                    default:
                        throw new Exception("Operation doesn't exist!");
                }
            }
        }
    }
}