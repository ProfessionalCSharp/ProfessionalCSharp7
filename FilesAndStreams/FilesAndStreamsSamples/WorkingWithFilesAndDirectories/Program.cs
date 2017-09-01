using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WorkingWithFilesAndDirectories
{
    public class Program
    {

        const string Sample1FileName = "Sample1.md";
        const string Sample2FileName = "Sample2.md";

        private static readonly string[,] options =
        {
            { "-d", nameof(GetDocumentsFolder) },
            { "-fi", nameof(FileInformation) },
            { "-p", nameof(ChangeFileProperties) },
            { "-c", nameof(CreateAFile) },
            { "-copy1", nameof(CopyFile1) },
            { "-copy2", nameof(CopyFile2) },
            { "-r", nameof(ReadingAFileLineByLine) },
            { "-w", nameof(WriteAFile) },
            { "-dd", nameof(DeleteDuplicateFiles) }
        };

        static void Main(string[] args)
        {
            var ops = Enumerable.Range(0, options.GetLength(0)).Select(i => options[i, 0]);
            if (args.Length == 0 || args.Length > 2 || !ops.Contains(args[0]))
            {
                ShowUsage();
                return;
            }

            if (args[0] == "-r" && args.Length == 2)
            {
                ReadingAFileLineByLine(args[1]);
            }
            else if (args[0] == "-dd" && args.Length == 2)
            {
                DeleteDuplicateFiles(args[1], checkOnly: true);
            }
            else
            {
                switch (args[0])
                {
                    case "-d":
                        GetDocumentsFolder();
                        break;
                    case "-fi":
                        FileInformation("./Program.cs");
                        break;
                    case "-p":
                        ChangeFileProperties();
                        break;
                    case "-c":
                        CreateAFile();
                        break;
                    case "-copy1":
                        CopyFile1();
                        break;
                    case "-copy2":
                        CopyFile2();
                        break;
                    case "-w":
                        WriteAFile();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage: WorkingWithFilesAndFolders [options] [filename|directory]");
            for (int i = 0; i < options.GetLength(0); i++)
            {
                Console.WriteLine($"\t{options[i, 0]}\t{options[i, 1]}");
            }

        }

        private static void FileInformation(string fileName)
        {
            var file = new FileInfo(fileName);
            Console.WriteLine($"Name: {file.Name}");
            Console.WriteLine($"Directory: {file.DirectoryName}");
            Console.WriteLine($"Read only: {file.IsReadOnly}");
            Console.WriteLine($"Extension: {file.Extension}");
            Console.WriteLine($"Length: {file.Length}");
            Console.WriteLine($"Creation time: {file.CreationTime:F}");
            Console.WriteLine($"Access time: {file.LastAccessTime:F}");
            Console.WriteLine($"File attributes: {file.Attributes}");
        }

        private static void DeleteDuplicateFiles(string directory, bool checkOnly)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories);
            string previousFileName = string.Empty;
            foreach (string fileName in fileNames)
            {
                string previousName = Path.GetFileNameWithoutExtension(previousFileName);
                if (!string.IsNullOrEmpty(previousFileName) &&
                    previousName.EndsWith("Copy") &&
                    fileName.StartsWith(previousFileName.Substring(0, previousFileName.LastIndexOf(" - Copy"))))
                {
                    var copiedFile = new FileInfo(previousFileName);
                    var originalFile = new FileInfo(fileName);
                    if (copiedFile.Length == originalFile.Length)
                    {
                        Console.WriteLine($"delete {copiedFile.FullName}");
                        if (!checkOnly)
                        {
                            copiedFile.Delete();
                        }
                    }
                }
                previousFileName = fileName;
            }
        }

        private static void WriteAFile()
        {
            string fileName = Path.Combine(GetDocumentsFolder(), "movies.txt");
            string[] movies =
            {
                "Snow White And The Seven Dwarfs",
                "Gone With The Wind",
                "Casablanca",
                "The Bridge On The River Kwai",
                "Some Like It Hot"
            };

            File.WriteAllLines(fileName, movies);

            string[] moreMovies =
            {
                "Psycho",
                "Easy Rider",
                "Star Wars",
                "The Matrix"
            };
            File.AppendAllLines(fileName, moreMovies);
        }

        private static void ReadingAFileLineByLine(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            int i = 1;
            foreach (var line in lines)
            {
                Console.WriteLine($"{i++}. {line}");
            }

            IEnumerable<string> lines2 = File.ReadLines(fileName);
            i = 1;
            foreach (var line in lines2)
            {
                Console.WriteLine($"{i++}. {line}");
            }
        }

        private static string GetDocumentsFolder() =>
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        private static void ChangeFileProperties()
        {
            string fileName = Path.Combine(GetDocumentsFolder(), Sample1FileName);
            var file = new FileInfo(fileName);
            if (!file.Exists)
            {
                Console.WriteLine($"Create the file {Sample1FileName} before calling this method");
                Console.WriteLine("You can do this by invoking this program with the -c argument");
                return;
            }

            Console.WriteLine($"creation time: {file.CreationTime:F}");
            file.CreationTime = new DateTime(2025, 12, 24, 15, 0, 0);
            Console.WriteLine($"creation time: {file.CreationTime:F}");
        }

        private static void CreateAFile()
        {
            string fileName = Path.Combine(GetDocumentsFolder(), Sample1FileName);
            File.WriteAllText(fileName, "Hello, World!");
        }

        private static void CopyFile1()
        {
            string fileName1 = Path.Combine(GetDocumentsFolder(), Sample1FileName);
            string fileName2 = Path.Combine(GetDocumentsFolder(), Sample2FileName);

            var file1 = new FileInfo(fileName1);
            if (file1.Exists)
            {
                file1.CopyTo(fileName2);
            }
        }

        private static void CopyFile2()
        {
            string fileName1 = Path.Combine(GetDocumentsFolder(), Sample1FileName);
            string fileName2 = Path.Combine(GetDocumentsFolder(), Sample2FileName);

            if (File.Exists(fileName1))
            {
                File.Copy(fileName1, fileName2);
            }
        }
    }
}
