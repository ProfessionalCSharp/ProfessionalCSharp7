using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryMappedFilesSample
{
    public class Program
    {
        private ManualResetEventSlim _mapCreated = new ManualResetEventSlim(initialState: false);
        private ManualResetEventSlim _dataWrittenEvent = new ManualResetEventSlim(initialState: false);
        private const string MAPNAME = "SampleMap";
        private static readonly string[] options = { "-f", "-s" };

        public static void Main(string[] args)
        {
            if (args.Length != 1 || !options.Contains(args[0]))
            {
                ShowUsage();
                return;
            }
            var p = new Program();
            if (args[0] == "-f")
            {
                p.Run();
            }
            else if (args[0] == "-s")
            {
                p.RunWithStreams();
            }

            Console.ReadLine();
        }

        private static void ShowUsage()
        {
            Console.WriteLine($"{nameof(MemoryMappedFilesSample)} [-f|-s]");
            Console.WriteLine("Options:");
            Console.WriteLine("-f\tFiles");
            Console.WriteLine("-s\tStreams");
        }

        public void Run()
        {
            Task.Run(() => WriterAsync());
            Task.Run(() => Reader());
            Console.WriteLine("tasks started");
        }

        public void RunWithStreams()
        {
            Task.Run(() => WriterUsingStreamsAsync());
            Task.Run(() => ReaderUsingStreamsAsync());
            Console.WriteLine("tasks started");
        }

        private async Task WriterAsync()
        {
            try
            {
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateOrOpen(MAPNAME, 10000, MemoryMappedFileAccess.ReadWrite))
                // MemoryMappedFile mappedFile = MemoryMappedFile.CreateFromFile("./memoryMappedFile", FileMode.Create, MAPNAME, 10000);
                {
                    _mapCreated.Set(); // signal shared memory segment created
                    Console.WriteLine("shared memory segment created");

                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(0, 10000, MemoryMappedFileAccess.Write))
                    {
                        for (int i = 0, pos = 0; i < 100; i++, pos += 4)
                        {
                            accessor.Write(pos, i);
                            Console.WriteLine($"written {i} at position {pos}");
                            await Task.Delay(10);
                        }
                        _dataWrittenEvent.Set(); // signal all data written
                        Console.WriteLine("data written");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"writer {ex.Message}");
            }
        }

        private async Task WriterUsingStreamsAsync()
        {
            try
            {
                using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateOrOpen(MAPNAME, 10000, MemoryMappedFileAccess.ReadWrite))
                // MemoryMappedFile mappedFile = MemoryMappedFile.CreateFromFile("./memoryMappedFile", FileMode.Create, MAPNAME, 10000);
                {
                    _mapCreated.Set(); // signal shared memory segment created
                    Console.WriteLine("shared memory segment created");

                    MemoryMappedViewStream stream = mappedFile.CreateViewStream(0, 10000, MemoryMappedFileAccess.Write);
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.AutoFlush = true;
                        for (int i = 0; i < 100; i++)
                        {
                            string s = $"some data {i}";
                            Console.WriteLine($"writing {s} at {stream.Position}");
                            await writer.WriteLineAsync(s);
                        }
                    }
                    _dataWrittenEvent.Set(); // signal all data written
                    Console.WriteLine("data written");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"writer {ex.Message}");
            }
        }

        private void Reader()
        {
            try
            {
                Console.WriteLine("reader");
                _mapCreated.Wait();
                Console.WriteLine("reader starting");

                using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(MAPNAME, MemoryMappedFileRights.Read))
                {
                    using (MemoryMappedViewAccessor accessor = mappedFile.CreateViewAccessor(0, 10000, MemoryMappedFileAccess.Read))
                    {
                        _dataWrittenEvent.Wait();
                        Console.WriteLine("reading can start now");

                        for (int i = 0; i < 400; i += 4)
                        {
                            int result = accessor.ReadInt32(i);
                            Console.WriteLine($"reading {result} from position {i}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"reader {ex.Message}");
            }
        }

        private async Task ReaderUsingStreamsAsync()
        {
            try
            {
                Console.WriteLine("reader");
                _mapCreated.Wait();
                Console.WriteLine("reader starting");

                using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(MAPNAME, MemoryMappedFileRights.Read))
                {
                    MemoryMappedViewStream stream = mappedFile.CreateViewStream(0, 10000, MemoryMappedFileAccess.Read);
                    using (var reader = new StreamReader(stream))
                    {
                        _dataWrittenEvent.Wait();
                        Console.WriteLine("reading can start now");

                        for (int i = 0; i < 100; i++)
                        {
                            long pos = stream.Position;
                            string s = await reader.ReadLineAsync();
                            Console.WriteLine($"read {s} from {pos}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"reader {ex.Message}");
            }
        }
    }
}
