using System;
using System.IO;

namespace FileManager
{
    public static class Operations
    {
        public static void DriverCd()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.WriteLine(drives);
        }
    }
}