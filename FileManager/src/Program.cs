using System;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Очищаем консоль.
            Console.Clear();
            int operationCode;

            // Цикл обновляющий реалищующий обновление и выбор доступных операций.
            while ((operationCode = Operations.PrintOperationsMenu()) != -1)
            {
                switch (operationCode)
                {
                    case 0:
                        Operations.DriveCd();
                        break;

                    case 1:
                        Operations.DirCd();
                        break;

                    case 2:
                        Operations.FileLs();
                        break;

                    case 3:
                        Operations.FileCp();
                        break;

                    case 4:
                        Operations.FileMv();
                        break;

                    case 5:
                        Operations.FileRm();
                        break;

                    case 6:
                        Operations.FileTouch();
                        break;

                    case 7:
                        Operations.FileConcat();
                        break;

                    default:
                        throw new Exception("Operation doesn't exist!");
                }
            }

            // Очищаем историю действий.
            History.CleanHistory();
        }
    }
}