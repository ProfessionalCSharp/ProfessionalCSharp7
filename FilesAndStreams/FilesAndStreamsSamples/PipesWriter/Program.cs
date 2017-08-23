using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace PipesWriter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string pipeName = args.Length >= 1 ? args[0] : "SamplePipe";
            if (pipeName == "anon")
            {
                AnonymousWriter();
            }
            else
            {
                // PipesWriter(pipeName);
                PipesWriter2(pipeName);
            }
        }

        private static void AnonymousWriter()
        {
            WriteLine("using anonymous pipe");
            Write("pipe handle: ");
            string pipeHandle = ReadLine();
            using (var pipeWriter = new AnonymousPipeClientStream(PipeDirection.Out, pipeHandle))
            using (var writer = new StreamWriter(pipeWriter))
            {
                for (int i = 0; i < 100; i++)
                {
                    writer.WriteLine($"Message {i}");
                    Task.Delay(500).Wait();
                }
            }
        }

        private static void PipesWriter2(string pipeName)
        {
            var pipeWriter = new NamedPipeClientStream("TheRocks", pipeName, PipeDirection.Out);
            using (var writer = new StreamWriter(pipeWriter))
            {
                pipeWriter.Connect();
                WriteLine("writer connected");

                bool completed = false;
                while (!completed)
                {
                    string input = ReadLine();
                    if (input == "bye") completed = true;

                    writer.WriteLine(input);
                    writer.Flush();
                }
            }
            WriteLine("completed writing");
        }

        private static void PipesWriter(string pipeName)
        {
            try
            {
                using (var pipeWriter = new NamedPipeClientStream("TheRocks", pipeName, PipeDirection.Out))
                {
                    pipeWriter.Connect();
                    WriteLine("writer connected");

                    bool completed = false;
                    while (!completed)
                    {
                        string input = ReadLine();
                        if (input == "bye") completed = true;

                        byte[] buffer = Encoding.UTF8.GetBytes(input);
                        pipeWriter.Write(buffer, 0, buffer.Length);
                    }
                }
                WriteLine("completed writing");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
