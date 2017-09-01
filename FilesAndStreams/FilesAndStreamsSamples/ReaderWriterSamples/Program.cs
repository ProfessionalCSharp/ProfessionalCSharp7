using System;
using System.IO;
using System.Text;

namespace ReaderWriterSamples
{
    public class Program
    {
        public static void Main()
        {
            ReadFileUsingReader("./Program.cs");
            Console.WriteLine();
            string textFile = Path.ChangeExtension(Path.GetTempFileName(), "txt");
            WriteFileUsingWriter(textFile, new string[] { "one", "two" });
            Console.WriteLine($"Written temp file {textFile}");

            string binFile = Path.ChangeExtension(Path.GetTempFileName(), "bin");
            Console.WriteLine($"writing to {binFile}");
            WriteFileUsingBinaryWriter(binFile);
            Console.WriteLine($"written to {binFile}");
            ReadFileUsingBinaryReader(binFile);
        }

        public static void WriteFileUsingWriter(string fileName, string[] lines)
        {
            var outputStream = File.OpenWrite(fileName);
            using (var writer = new StreamWriter(outputStream))
            {
                byte[] preamble = Encoding.UTF8.GetPreamble();
                outputStream.Write(preamble, 0, preamble.Length);
                writer.Write(lines);
            }
        }

        public static void WriteFileUsingBinaryWriter(string binFile)
        {
            var outputStream = File.Create(binFile);
            using (var writer = new BinaryWriter(outputStream))
            {
                double d = 47.47;
                int i = 42;
                long l = 987654321;
                string s = "sample";

                writer.Write(d);
                writer.Write(i);
                writer.Write(l);
                writer.Write(s);
            }
        }

        public static void ReadFileUsingBinaryReader(string binFile)
        {
            var inputStream = File.Open(binFile, FileMode.Open);
            using (var reader = new BinaryReader(inputStream))
            {
                double d = reader.ReadDouble();

                int i = reader.ReadInt32();
                long l = reader.ReadInt64();
                string s = reader.ReadString();
                Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
            }
        }

        public static void ReadFileUsingReader(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }
    }
}
