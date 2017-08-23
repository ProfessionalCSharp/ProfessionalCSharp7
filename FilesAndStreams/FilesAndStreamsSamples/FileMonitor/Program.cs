using System;
using System.IO;

namespace FileMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WatchFiles("c:/test", "*.txt");
            Console.ReadLine();
        }

        public static void WatchFiles(string path, string filter)
        {
            var watcher = new FileSystemWatcher(path, filter)
            {
                IncludeSubdirectories = true
            };
            watcher.Created += OnFileChanged;
            watcher.Changed += OnFileChanged;
            watcher.Deleted += OnFileChanged;
            watcher.Renamed += OnFileRenamed;

            watcher.EnableRaisingEvents = true;
            Console.WriteLine("watching file changes...");
        }


        private static void OnFileRenamed(object sender, RenamedEventArgs e) =>
            Console.WriteLine($"file {e.OldName} {e.ChangeType} to {e.Name}");

        private static void OnFileChanged(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"file {e.Name} {e.ChangeType}");
    }
}
