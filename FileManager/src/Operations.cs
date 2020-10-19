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
            [1] = "Переход в другую директорию.",
            [2] = "Просмотр списка файлов в директории и вывод файла.",
            [3] = "Копирование файла.",
            [4] = "Перемещение файла.",
            [5] = "Удаление файла.",
            [6] = "Создание файла.",
            [7] = "Конкатенация файлов."
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
            return Menu.PrintMenu(operationList, Messages.WelcomeMessage, Messages.ByeMessage);
        }


        public static int PrintDriveCdMenu(List<string> driveList)
        {
            return Menu.PrintMenu(driveList, Messages.DriveCdStartMessage);
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

                History.WriteBoth(Messages.ActionCdCompeted);
                History.WriteLineBoth(Directory.GetCurrentDirectory());
                History.WriteLineBoth();
            }
            else
            {
                History.WriteLineBoth(Errors.UnixError);
            }
        }

        public static void DirCd()
        {
            History.WriteLineBoth(Messages.DirCdStartMessage);
            string dirName = Console.ReadLine();
            History.WriteLine(dirName);
            if (!Config.SetCurrentDirectory(dirName))
            {
                return;
            }
            History.WriteBoth(Messages.ActionCdCompeted);
            History.WriteLineBoth(Directory.GetCurrentDirectory());
            History.WriteLineBoth();
        }

        private static int PrintFileListMenu(List<string> fileList)
        {
            return Menu.PrintMenu(fileList, Messages.FileLsStartMessage);
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
            return Menu.PrintMenu(encodingsList, Messages.FileEncodingStartMessage);
        }

        public static void FileCat(string path, Encoding encoding)
        {
            using (StreamReader file = new StreamReader(path))
            {
                string s;
                while ((s = file.ReadLine()) != null)
                {
                    History.WriteLineBoth(s, encoding);
                }
            }
        }

        public static void FileCp()
        {
            History.WriteLineBoth(Messages.FileCpStart1Message);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);
            
            History.WriteLineBoth(Messages.FileCpStart2Message);
            
            string destPath = Console.ReadLine();
            History.WriteLine(destPath);
            if (!File.Exists(destPath))
            {
                if (!Directory.Exists(destPath))
                {
                    History.WriteLineBoth(Errors.IncorrectPathError);
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
                History.WriteLineBoth(Errors.AccessError);
                return;
            }
            catch
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }
            
            History.WriteLineBoth($"Файл {filePath} скопирован в {destPath}");
        }

        public static void FileMv()
        {
            History.WriteLineBoth(Messages.FileMvStart1Message);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);
            
            History.WriteLineBoth(Messages.FileMvStart2Message);
            
            string destPath = Console.ReadLine();
            History.WriteLine(destPath);

            if (!Directory.Exists(destPath))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
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
                History.WriteLineBoth(Errors.AccessError);
                return;
            }
            catch
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }
            
            History.WriteLineBoth($"Файл {filePath} перемещен в {Path.GetDirectoryName(destPath)}.");
        }

        public static void FileRm()
        {
            History.WriteLineBoth(Messages.FileRmStartMessage);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);

            try
            {
                File.Delete(filePath);
            }
            catch (UnauthorizedAccessException)
            {
                History.WriteLineBoth(Errors.AccessError);
                return;
            }
            catch
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }
            
            History.WriteLineBoth($"Файл {filePath} успешно удален.");
        }

        public static void FileTouch()
        {
            int encodingItr = PrintEncodingMenu();
            Encoding encoding;
            switch (encodingItr)
            {
                case 0:
                    encoding = Encoding.UTF8;
                    break;

                case 1:
                    encoding = Encoding.UTF7;
                    break;

                case 2:
                    encoding = Encoding.UTF32;
                    break;

                case 3:
                    encoding = Encoding.ASCII;
                    break;

                case -1:
                    return;
                
                default:
                    return;
            }
            
            History.WriteLineBoth(Messages.FileTouchStartMessage);
            string filePath = Console.ReadLine();
            History.WriteLine(filePath);
            if (File.Exists(filePath) || !Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }

            filePath = Path.GetFullPath(filePath);

            History.WriteLineBoth(Messages.FileTouchEditMessage);
            
            ConsoleKeyInfo key = Console.ReadKey(true);
            string s = "";
            using (StreamWriter file = new StreamWriter(filePath, false, encoding))
            {
                while (key.Key != ConsoleKey.Escape)
                {
                    s = Console.ReadLine();
                    file.WriteLine(key.KeyChar + s);
                    key = Console.ReadKey(true);
                }
            }
            
            History.WriteLineBoth($"Файл {filePath} успешно создан.");
        }

        public static void FileConcat()
        {
            History.WriteLineBoth(Messages.FileConc1Message);
            string fileRes = Console.ReadLine();
            History.WriteLine(fileRes);
            
            if (File.Exists(fileRes) || !Directory.Exists(Path.GetDirectoryName(fileRes)))
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
                return;
            }

            fileRes = Path.GetFullPath(fileRes);

            History.WriteLineBoth(Messages.FileConc2Message);
            string fileFirst = Console.ReadLine();
            History.WriteLine(fileFirst);
            if (!File.Exists(fileFirst))
            {
                return;
            }

            History.WriteLineBoth(Messages.FileConc3Message);
            string fileSecond = Console.ReadLine();
            History.WriteLine(fileSecond);
            if (!File.Exists(fileSecond))
            {
                return;
            }

            using (StreamWriter fileResStream = new StreamWriter(fileRes))
            {
                using (StreamReader fileFirstStream = new StreamReader(fileFirst))
                {
                    string s;
                    while ((s = fileFirstStream.ReadLine()) != null)
                    {
                        fileResStream.WriteLine(s);
                    }
                }

                using (StreamReader fileSecondStream = new StreamReader(fileSecond))
                {
                    string s;
                    while ((s = fileSecondStream.ReadLine()) != null)
                    {
                        fileResStream.WriteLine(s);
                    }
                }
            }
            
            History.WriteLineBoth($"Файлы {fileFirst} и {fileSecond} успешно сконкатенированы в {fileRes}.");
        }
    }
}