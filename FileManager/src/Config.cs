using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс настроек: вспомогательных переменных и функций.
    /// </summary>
    public static class Config
    {
        public static PlatformID Platform = Environment.OSVersion.Platform;
        public static Char DirSep = Path.DirectorySeparatorChar;
        public static string historyPath = Directory.GetCurrentDirectory() + @"/OperationsHistory.txt";
        public static Encoding systemEncoding = Console.OutputEncoding;

        public static bool SetCurrentDirectory(string path)
        {
            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                History.WriteLineBoth(Errors.DirNotFoundError);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                History.WriteLineBoth(Errors.AccessError);
                return false;
            }
            catch
            {
                History.WriteLineBoth(Errors.IncorrectPathError);
            }

            return true;
        }
    }

    /// <summary>
    /// Класс вспомогательных сообщений.
    /// </summary>
    public static class Messages
    {
        public const string WelcomeMessage = "Привет! Текущая директория: ";
        public const string ByeMessage = "Пока(";
        public const string DriveCdStartMessage = "Выберите диск:";
        public const string ActionCdCompeted = "Вы перешли в ";
        public const string DirCdStartMessage = "Введите путь к папке для перехода:";
        public const string FileLsStartMessage = "Просмотр файлов. Выберите файл для вывода его содержимого.";
        public const string FileEncodingStartMessage = "Выберите кодировку:";
        public const string FileCpStart1Message = "Введите путь к файлу:";
        public const string FileCpStart2Message = "Введите путь копирования:";
        public const string FileMvStart1Message = "Введите путь к файлу:";
        public const string FileMvStart2Message = "Введите путь перемещения: ";
        public const string FileRmStartMessage = "Введите путь к файлу, который вы хотите удалить:";
        public const string FileTouchStartMessage = "Введите путь и название файла, который вы хотите создать:";

        public static string FileTouchEditMessage = "Режим построкого ввода: отредактируйте, " +
                                                    "если необходимо, файл и по окончании нажмите Esc в начале ввода строки.\n";

        public static string FileConc1Message = "Введите путь к результирующему файлу, который будет создан:";
        public static string FileConc2Message = "Введите путь к первому файлу из конкатенации:";
        public static string FileConc3Message = "Введите путь ко второму файлу из конкатенации:";
    }

    /// <summary>
    /// Класс ошибок.
    /// </summary>
    public static class Errors
    {
        public const string UnixError = "У Unix-подобных систем нет дисков.\n";
        public const string DirNotFoundError = "Папка не найдена.\n";
        public const string EmptyListError = "Пустая папка.\n";
        public const string AccessError = "Отказано в доступе.\n";
        public const string IncorrectPathError = "Плохой путь.\n";
    }
}