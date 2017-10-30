using System;
using System.IO;

namespace DriveInformation
{
    public class Program
    {
        public static void Main()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive name: {drive.Name}");
                    Console.WriteLine($"Format: {drive.DriveFormat}");
                    Console.WriteLine($"Type: {drive.DriveType}");
                    Console.WriteLine($"Root directory: {drive.RootDirectory}");
                    Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                    Console.WriteLine($"Free space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Available space: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Total size: {drive.TotalSize}");

                    Console.WriteLine();
                }
            }
        }
    }
}
