using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager
{
    public static class Operations
    {
        public static Dictionary<int, string> operationsDict = new Dictionary<int, string>()
        {
            [0] = "Просмотр списка дисков компьютера и выбор диска.",
            [1] = "Переход в другую директорию (выбор папки).",
            [2] = "Просмотр списка файлов в директории.",
            [3] = "Вывод содержимого текстового файла в консоль в кодировке UTF-8."
        };

        public static int PrintOperationsMenu()
        {
            List<string> operationList = new List<string>(operationsDict.Values);
            return Menu.PrintMenu(operationList, Messages.welcomeMessage);
        }


        public static int PrintDriveCdMenu(List<string> driveList)
        {
            return Menu.PrintMenu(driveList, Messages.driveCdStartMessage);
        }

        public static void DriveCd()
        {
            if (Config.Platform == PlatformID.Win32NT)
            {
                List<string> driveList = new List<string>(DriveInfo.GetDrives().Select(x => x.ToString()));
                int driveItr = PrintDriveCdMenu(driveList);

                if (driveItr == -1)
                {
                    return;
                }

                if (!Config.SetCurrentDirectory(driveList[driveItr]))
                {
                    return;
                }

                History.WriteBoth(Messages.actionCdCompeted);
                History.WriteLineBoth(Directory.GetCurrentDirectory());
            }
            else
            {
                History.WriteLineBoth(Errors.unixError);
            }
        }

        public static void DirCd()
        {
            History.WriteLineBoth(Messages.dirCdStartMessage);
            string dirName = Console.ReadLine();
            History.WriteLine(dirName);
            if (!Config.SetCurrentDirectory(dirName))
            {
                return;
            }
            History.WriteBoth(Messages.actionCdCompeted);
            History.WriteLineBoth(Directory.GetCurrentDirectory());
        }
    }
}