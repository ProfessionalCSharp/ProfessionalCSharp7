using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

namespace PipesWriter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string pipeName = args.Length >= 1 ? args[0] : "SamplePipe";
            string serverName = args.Length >= 2 ? args[1] : "localhost";
            if (pipeName == "anon")
            {
                AnonymousWriter();
            }
            else
            {
                // PipesWriter(serverName, pipeName);
                PipesWriter2(serverName, pipeName);
            }
        }

        private static void AnonymousWriter()
        {
            Console.WriteLine("using anonymous pipe");
            Console.Write("pipe handle: ");
            string pipeHandle = Console.ReadLine();
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

        private static void PipesWriter2(string serverName, string pipeName)
        {
            var pipeWriter = new NamedPipeClientStream(serverName, pipeName, PipeDirection.Out);
            using (var writer = new StreamWriter(pipeWriter))
            {
                pipeWriter.Connect();
                Console.WriteLine("writer connected");

                bool completed = false;
                while (!completed)
                {
                    string input = Console.ReadLine();
                    if (input == "bye") completed = true;

                    writer.WriteLine(input);
                    writer.Flush();
                }
            }
            Console.WriteLine("completed writing");
        }

        private static void PipesWriter(string serverName, string pipeName)
        {
            try
            {
                using (var pipeWriter = new NamedPipeClientStream(serverName, pipeName, PipeDirection.Out))
                {
                    pipeWriter.Connect();
                    Console.WriteLine("writer connected");

                    bool completed = false;
                    while (!completed)
                    {
                        string input = Console.ReadLine();
                        if (input == "bye") completed = true;

                        byte[] buffer = Encoding.UTF8.GetBytes(input);
                        pipeWriter.Write(buffer, 0, buffer.Length);
                    }
                }
                Console.WriteLine("completed writing");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
