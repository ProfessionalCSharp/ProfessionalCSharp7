using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using static System.Console;

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
                WriteLine("using anonymous pipe");
                string pipeHandle = reader.GetClientHandleAsString();
                WriteLine($"pipe handle: {pipeHandle}");

                byte[] buffer = new byte[256];
                int nRead = reader.Read(buffer, 0, 256);
                
                string line = Encoding.UTF8.GetString(buffer, 0, 256);
                WriteLine(line);
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
                    WriteLine("reader connected");

                    bool completed = false;
                    while (!completed)
                    {
                        string line = reader.ReadLine();
                        WriteLine(line);
                        if (line == "bye") completed = true;
                    }
                }
                WriteLine("completed reading");
                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        private static void PipesReader(string pipeName)
        {
            try
            {
                using (var pipeReader = new NamedPipeServerStream(pipeName, PipeDirection.In))
                {
                    pipeReader.WaitForConnection();
                    WriteLine("reader connected");
                    const int BUFFERSIZE = 256;

                    bool completed = false;
                    while (!completed)
                    {
                        byte[] buffer = new byte[BUFFERSIZE];
                        int nRead = pipeReader.Read(buffer, 0, BUFFERSIZE);
                        string line = Encoding.UTF8.GetString(buffer, 0, nRead);
                        WriteLine(line);
                        if (line == "bye") completed = true;
                    }
                }
                WriteLine("completed reading");
                ReadLine();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
