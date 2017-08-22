using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace SimpleDataFlowSample
{
    class Program
    {
        // Simple Action block
        //public static void Main(string[] args)
        //{
        //    var processInput = new ActionBlock<string>(s =>
        //    {
        //        WriteLine($"user input: {s}");
        //    });
        //
        //    bool exit = false;
        //    while (!exit)
        //    {
        //        string input = ReadLine();
        //        if (string.Compare(input, "exit", ignoreCase: true) == 0)
        //        {
        //            exit = true;
        //        }
        //        else
        //        {
        //            processInput.Post(input);
        //        }
        //    }           
        //}

        // source and target blocks
        static void Main()
        {
            Task t1 = Task.Run(() => Producer());
            Task t2 = Task.Run(async () => await ConsumerAsync());
            Task.WaitAll(t1, t2);
        }

        private static BufferBlock<string> s_buffer = new BufferBlock<string>();

        public static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    s_buffer.Post(input);
                }
            }
        }

        public static async Task ConsumerAsync()
        {
            while (true)
            {
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine($"user input: {data}");
            }
        }
    }
}
