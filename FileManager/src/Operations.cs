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
        
        public static List<string> encodingsList = new List<string>()
        {
            "UTF-8",
            "UTF-7",
            "UTF-32",
            "ASCII"
        };

        public static int PrintOperationsMenu()
        {
            List<string> operationList = new List<string>(operationsDict.Values);
            return Menu.PrintMenu(operationList, Messages.welcomeMessage, Messages.byeMessage);
        }


        public static int PrintDriveCdMenu(List<string> driveList)
        {
            return Menu.PrintMenu(driveList, Messages.driveCdStartMessage);
        }

        public static void DriveCd()
        {
            if (Config.Platform == PlatformID.Win32NT || true)
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

        private static int PrintFileListMenu(List<string> fileList)
        {
            return Menu.PrintMenu(fileList, Messages.fileLsStartMessage);
        }

        public static void FileLs()
        {
            List<string> fileList = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory()).Select(x => Path.GetFileName(x)));
            int fileItr = PrintFileListMenu(fileList);

            if (fileItr == -1)
            {
                return;
            }

            int encodingItr = PrintEncodingMenu();

            if (encodingItr == -1)
            {
                return;
            }
        }

        public static int PrintEncodingMenu()
        {
            return Menu.PrintMenu(encodingsList, Messages.fileEncodingStartMessage);
        }
        
    }
}