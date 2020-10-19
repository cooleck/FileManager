using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;

namespace FileManager
{
    public static class Config
    {
        public static PlatformID Platform = Environment.OSVersion.Platform;
        public static Char DirSep = Path.DirectorySeparatorChar;
        public static string historyPath = Directory.GetCurrentDirectory() + @"/OperationsHistory.txt";

        public static bool SetCurrentDirectory(string path)
        {
            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                History.WriteLineBoth(Errors.dirNotFoundError);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                History.WriteLineBoth(Errors.accessError);
                return false;
            }

            return true;
        }
    }
    
    public static class Messages
    {
        public static string welcomeMessage = "Привет";
        public static string byeMessage = "Пока";
        public static string driveCdStartMessage = "Выберите диск";
        public static string actionCdCompeted = "Вы перешли в ";
        public static string dirCdStartMessage = "Выберите папку";
        public static string fileLsStartMessage = "Просмотр файлов. Выберите файл для вывода его содержимого.";
    }

    public static class Errors
    {
        public static string unixError = "У Unix нет дисков!";
        public static string dirNotFoundError = "Папка не найдена";
        public static string accessError = "Отказано в доступе";
    }
}