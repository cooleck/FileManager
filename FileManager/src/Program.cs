using System;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            int operationCode;
            while ((operationCode = Menu.OperationMenu()) != -1) {

                switch (operationCode)
                {
                    case 0:
                        

                    default:
                        throw new Exception("Operation doesn't exist!");
                }
            }
        }
    }
}