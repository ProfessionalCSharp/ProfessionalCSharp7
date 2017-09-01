using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CompressFileSample
{
    class Program
    {
        static void Main()
        {
            CompressFile("./test.txt", "./test.txt.gzip");
            DecompressFile("./test.txt.gzip");

            CompressFileWithBrotli("./test.txt", "./test.txt.brotli");
            DecompressFileWithBrotli("./test.txt.brotli");

            CreateZipFile("../StreamSamples/", "./test.zip");
        }

        public static void CreateZipFile(string directory, string zipFile)
        {
            InitSampleFilesForZip(directory);
            string destDirectory = Path.GetDirectoryName(zipFile);
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            FileStream zipStream = File.Create(zipFile);
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    using (FileStream inputStream = File.OpenRead(file))
                    using (Stream outputStream = entry.Open())
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }

        private static void InitSampleFilesForZip(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);

                for (int i = 0; i < 10; i++)
                {
                    string destFileName = Path.Combine(directory, $"test{i}.txt");

                    File.Copy("Test.txt", destFileName);
                }

            } // else nothing to do, using existing files from the directory
        }

        public static void DecompressFile(string fileName)
        {
            FileStream inputStream = File.OpenRead(fileName);
            using (MemoryStream outputStream = new MemoryStream())
            using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                compressStream.CopyTo(outputStream);
                outputStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 4096, leaveOpen: true))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }

        public static void CompressFile(string fileName, string compressedFileName)
        {
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                FileStream outputStream = File.OpenWrite(compressedFileName);
                using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
                {
                    inputStream.CopyTo(compressStream);
                }
            }
        }

        public static void CompressFileWithBrotli(string fileName, string compressedFileName)
        {
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                FileStream outputStream = File.OpenWrite(compressedFileName);
                using (var compressStream = new BrotliStream(outputStream, CompressionMode.Compress))
                {
                    inputStream.CopyTo(compressStream);
                }
            }
        }

        public static void DecompressFileWithBrotli(string fileName)
        {
            FileStream inputStream = File.OpenRead(fileName);
            using (MemoryStream outputStream = new MemoryStream())
            using (var compressStream = new BrotliStream(inputStream, CompressionMode.Decompress))
            {
                compressStream.CopyTo(outputStream);
                outputStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 4096, leaveOpen: true))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
        }
    }
}
