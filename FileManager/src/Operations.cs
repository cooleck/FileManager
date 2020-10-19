using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    public static class Operations
    {
        public static Dictionary<int, string> operationsDict = new Dictionary<int, string>()
        {
            [0] = "Просмотр списка дисков компьютера и выбор диска.",
            [1] = "Переход в другую директорию (выбор папки).",
            [2] = "Просмотр списка файлов в директории.",
            [3] = "Копирование файла.",
            [4] = "Перемещение файла."
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
            List<string> fileListOfFullPath = new List<string>(Directory.GetFiles(Directory.GetCurrentDirectory()));
            List<string> fileList = new List<string>(fileListOfFullPath.Select(x => Path.GetFileName(x)));
            int fileItr;
            while ((fileItr = PrintFileListMenu(fileList)) != -1)
            {

                int encodingItr = PrintEncodingMenu();

                switch (encodingItr)
                {
                    case 0:
                        FileCat(fileListOfFullPath[fileItr], Encoding.UTF8);
                        return;
                    
                    case 1:
                        FileCat(fileListOfFullPath[fileItr], Encoding.UTF7);
                        return;
                    
                    case 2:
                        FileCat(fileListOfFullPath[fileItr], Encoding.UTF32);
                        return;
                    
                    case 3:
                        FileCat(fileListOfFullPath[fileItr], Encoding.ASCII);
                        return;
                }
            }
        }

        public static int PrintEncodingMenu()
        {
            return Menu.PrintMenu(encodingsList, Messages.fileEncodingStartMessage);
        }

        public static void FileCat(string path, Encoding encoding)
        {
            using (StreamReader file = new StreamReader(path))
            {
                string s;
                while ((s = file.ReadLine()) != null)
                {
                    History.WriteLineBoth(s, encoding);
                    s = file.ReadLine();
                }
            }
        }

        public static void FileCp()
        {
            History.WriteLineBoth(Messages.fileCpStart1Message);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                History.WriteLineBoth(Errors.incorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);
            
            History.WriteLineBoth(Messages.fileCpStart2Message);
            
            string destPath = Console.ReadLine();
            History.WriteLine(destPath);
            if (!File.Exists(destPath))
            {
                if (!Directory.Exists(destPath))
                {
                    History.WriteLineBoth(Errors.incorrectPathError);
                    return;
                }

                destPath = Path.GetFullPath(destPath);
                destPath = Path.GetFullPath(destPath + "/" + Path.GetFileName(filePath));
            }
            else
            {
                destPath = Path.GetFullPath(destPath);
            }

            try
            {
                File.Copy(filePath, destPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                History.WriteLineBoth(Errors.accessError);
                return;
            }
            catch
            {
                History.WriteLineBoth(Errors.incorrectPathError);
                return;
            }
            
            History.WriteLineBoth($"Файл {filePath} скопирован в {destPath}");
        }

        public static void FileMv()
        {
            History.WriteLineBoth(Messages.fileMvStart1Message);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                History.WriteLineBoth(Errors.incorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);
            
            History.WriteLineBoth(Messages.fileMvStart2Message);
            
            string destPath = Console.ReadLine();
            History.WriteLine(destPath);

            if (!Directory.Exists(destPath))
            {
                History.WriteLineBoth(Errors.incorrectPathError);
                return;
            }

            destPath = Path.GetFullPath(destPath);
            destPath = Path.GetFullPath(destPath + "/" + Path.GetFileName(filePath));

            try
            {
                File.Move(filePath, destPath);
            }
            catch (UnauthorizedAccessException)
            {
                History.WriteLineBoth(Errors.accessError);
                return;
            }
            catch
            {
                History.WriteLineBoth(Errors.incorrectPathError);
                return;
            }
            
            History.WriteLineBoth($"Файл {filePath} перемещен в {Path.GetDirectoryName(destPath)}");
        }
    }
}