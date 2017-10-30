using System;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace PipesReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string pipeName = args.Length == 1 ? args[0] : "SamplePipe";
            if (pipeName == "anon")
            {
                AnonymousReader();
            }
            else
            {
                PipesReader2(pipeName);
            }
        }

        private static void AnonymousReader()
        {
            using (var reader = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable))
            {
                Console.WriteLine("using anonymous pipe");
                string pipeHandle = reader.GetClientHandleAsString();
                Console.WriteLine($"pipe handle: {pipeHandle}");

                byte[] buffer = new byte[256];
                int nRead = reader.Read(buffer, 0, 256);
                
                string line = Encoding.UTF8.GetString(buffer, 0, 256);
                Console.WriteLine(line);
            }
        }

        private static void PipesReader2(string pipeName)
        {
            try
            {
                var pipeReader = new NamedPipeServerStream(pipeName, PipeDirection.In);
                using (var reader = new StreamReader(pipeReader))
                {
                    pipeReader.WaitForConnection();
                    Console.WriteLine("reader connected");

                    bool completed = false;
                    while (!completed)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);
                        if (line == "bye") completed = true;
                    }
                }
                Console.WriteLine("completed reading");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PipesReader(string pipeName)
        {
            try
            {
                using (var pipeReader = new NamedPipeServerStream(pipeName, PipeDirection.In))
                {
                    pipeReader.WaitForConnection();
                    Console.WriteLine("reader connected");
                    const int BUFFERSIZE = 256;

                    bool completed = false;
                    while (!completed)
                    {
                        byte[] buffer = new byte[BUFFERSIZE];
                        int nRead = pipeReader.Read(buffer, 0, BUFFERSIZE);
                        string line = Encoding.UTF8.GetString(buffer, 0, nRead);
                        Console.WriteLine(line);
                        if (line == "bye") completed = true;
                    }
                }
                Console.WriteLine("completed reading");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
