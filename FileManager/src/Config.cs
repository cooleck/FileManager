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
        public static PlatformID platform = Environment.OSVersion.Platform;
        public static string currentDirectory = Directory.GetCurrentDirectory();
    }
    
    public static class Messages
    {
        public static string welcomeMessage = "Привет";
        public static string byeMessage = "Пока";
        public static string continueMessage = "Нажмите любую кнопку для продолжениея или Esc для выхода.";
        public static string driveCdStartMessage = "Выберите диск";
        public static string actionCdCompeted = "Вы перешли в ";
    }

    public static class Errors
    {
        public static string unixError = "У Unix нет дисков!";
    }
}