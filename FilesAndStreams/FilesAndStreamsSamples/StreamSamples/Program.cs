using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamSamples
{
    public class Program
    {
        const int RECORDSIZE = 44;
        const string SampleFileDataPath = "./SampleFile.data";

        public static async Task Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "-rs")
            {
                ReadFileUsingFileStream(args[1]);
            }
            else if (args.Length == 1 && args[0] == "-w")
            {
                WriteTextFile();
            } 
            else if (args.Length == 3 && args[0] == "-cs")
            {
                CopyUsingStreams(args[1], args[2]);
            }
            else if (args.Length == 3 && args[0] == "-cs2")
            {
                CopyUsingStreams2(args[1], args[2]);
            }
            else if (args.Length == 1 && args[0] == "-sample")
            {
                await CreateSampleFileAsync(1500000);
            }
            else if (args.Length == 1 && args[0] == "-r")
            {
                RandomAccessSample();
            }
            else
            {
                ShowUsage();
            }
            Console.WriteLine("ready");
        }

        public static void ShowUsage()
        {
            Console.WriteLine("Usage: StreamSamples [option] [Filename]");
            Console.WriteLine("Options:");
            Console.WriteLine("\t-rs filename\tRead File Using Streams");
            Console.WriteLine("\t-w filename\tWrite Text File");
            Console.WriteLine("\t-cs sourcefilename targetfilename\tCopy Using Streams");
            Console.WriteLine("\t-cs2 sourcefilename targetfilename\tCopy Using Streams 2");
            Console.WriteLine("\t-sample\tCreate Sample File");
            Console.WriteLine("\t-r\tRandom Access Sample");
        }

        public static void WriteTextFile()
        {
            string tempTextFileName = Path.ChangeExtension(Path.GetTempFileName(), "txt");
            using (FileStream stream = File.OpenWrite(tempTextFileName))
            {
                //// write BOM
                //stream.WriteByte(0xef);
                //stream.WriteByte(0xbb);
                //stream.WriteByte(0xbf);

                byte[] preamble = Encoding.UTF8.GetPreamble();
                stream.Write(preamble, 0, preamble.Length);

                string hello = "Hello, World!";
                byte[] buffer = Encoding.UTF8.GetBytes(hello);
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine($"file {stream.Name} written");
            }
        }

        public static void RandomAccessSample()
        {
            try
            {
                using (FileStream stream = File.OpenRead(SampleFileDataPath))
                {
                    byte[] buffer = new byte[RECORDSIZE];
                    do
                    {
                        try
                        {
                            Console.Write("record number (or 'bye' to end): ");
                            string line = Console.ReadLine();
                            if (line.ToUpper().CompareTo("BYE") == 0) break;

                            if (int.TryParse(line, out int record))
                            {
                                stream.Seek((record - 1) * RECORDSIZE, SeekOrigin.Begin);
                                stream.Read(buffer, 0, RECORDSIZE);
                                string s = Encoding.UTF8.GetString(buffer);
                                Console.WriteLine($"record: {s}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } while (true);
                    Console.WriteLine("finished");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Create the sample file using the option -sample first");
            }
        }

        public static async Task CreateSampleFileAsync(int nRecords)
        {
            using (FileStream stream = File.Create(SampleFileDataPath))
            using (var writer = new StreamWriter(stream))
            {
                var r = new Random();

                var records = Enumerable.Range(1, nRecords).Select(x => new
                {
                    Number = x,
                    Text = $"Sample text {r.Next(200)}",
                    Date = new DateTime(Math.Abs((long)((r.NextDouble() * 2 - 1) * DateTime.MaxValue.Ticks)))
                });

                foreach (var rec in records)
                {
                    string date = rec.Date.ToString("d", CultureInfo.InvariantCulture);
                    string s = $"#{rec.Number,8};{rec.Text,-20};{date}#{Environment.NewLine}";
                    await writer.WriteAsync(s);
                }
            }
        }

        public static void CopyUsingStreams(string inputFile, string outputFile)
        {
            const int BUFFERSIZE = 4096;
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            {
                byte[] buffer = new byte[BUFFERSIZE];
                bool completed = false;
                do
                {
                    int nRead = inputStream.Read(buffer, 0, BUFFERSIZE);
                    if (nRead == 0) completed = true;
                    outputStream.Write(buffer, 0, nRead);
                } while (!completed);
            }
        }

        public static void CopyUsingStreams2(string inputFile, string outputFile)
        {
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            {
                inputStream.CopyTo(outputStream);
            }
        }

        public static void ReadFileUsingFileStream(string fileName)
        {
            const int BUFFERSIZE = 4096;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ShowStreamInformation(stream);
                Encoding encoding = GetEncoding(stream);


                byte[] buffer = new byte[BUFFERSIZE];

                bool completed = false;
                do
                {
                    int nread = stream.Read(buffer, 0, BUFFERSIZE);
                    if (nread == 0) completed = true;
                    if (nread < BUFFERSIZE)
                    {
                        Array.Clear(buffer, nread, BUFFERSIZE - nread);
                    }

                    string s = encoding.GetString(buffer, 0, nread);
                    Console.WriteLine($"read {nread} bytes");
                    Console.WriteLine(s);
                } while (!completed);
            }
        }

        public static void ShowStreamInformation(Stream stream)
        {
            Console.WriteLine($"stream can read: {stream.CanRead}, can write: {stream.CanWrite}, can seek: {stream.CanSeek}, can timeout: {stream.CanTimeout}");
            Console.WriteLine($"length: {stream.Length}, position: {stream.Position}");
            if (stream.CanTimeout)
            {
                Console.WriteLine($"read timeout: {stream.ReadTimeout} write timeout: {stream.WriteTimeout} ");
            }
        }

        // read BOM
        public static Encoding GetEncoding(Stream stream)
        {
            if (!stream.CanSeek) throw new ArgumentException("require a stream that can seek");

            Encoding encoding = Encoding.ASCII;

            byte[] bom = new byte[5];
            int nRead = stream.Read(bom, offset: 0, count: 5);
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                Console.WriteLine("UTF-32");
                stream.Seek(4, SeekOrigin.Begin);
                return Encoding.UTF32;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                Console.WriteLine("UTF-16, little endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.Unicode;
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                Console.WriteLine("UTF-16, big endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.BigEndianUnicode;
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                Console.WriteLine("UTF-8");
                stream.Seek(3, SeekOrigin.Begin);
                return Encoding.UTF8;
            }
            stream.Seek(0, SeekOrigin.Begin);
            return encoding;
        }
    }
}
