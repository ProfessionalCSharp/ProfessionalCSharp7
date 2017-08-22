using System;
using System.Threading;
using System.Threading.Tasks;

namespace LockAcrossAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunUseSemaphoreAsync();
            await RunUseAsyncSempahoreAsync();
            Console.ReadLine();
        }

        //private static object s_syncLock = new object();
        //static async Task IncorrectLockAsync()
        //{
        //    lock (s_syncLock)
        //    {
        //        Console.WriteLine($"{nameof(IncorrectLockAsync)} started");
        //        await Task.Delay(500);  // compiler error: cannot await in the body of a lock statement
        //        Console.WriteLine($"{nameof(IncorrectLockAsync)} ending");
        //    }
        //}


        static async Task RunUseSemaphoreAsync()
        {
            Console.WriteLine(nameof(RunUseSemaphoreAsync));
            string[] messages = { "one", "two", "three", "four", "five", "six" };
            Task[] tasks = new Task[messages.Length];

            for (int i = 0; i < messages.Length; i++)
            {
                string message = messages[i];

                tasks[i] = Task.Run(async () =>
                {
                    await LockWithSemaphore(message);
                });
            }

            await Task.WhenAll(tasks);
            Console.WriteLine();
        }

        static async Task RunUseAsyncSempahoreAsync()
        {
            Console.WriteLine(nameof(RunUseAsyncSempahoreAsync));
            string[] messages = { "one", "two", "three", "four", "five", "six" };
            Task[] tasks = new Task[messages.Length];

            for (int i = 0; i < messages.Length; i++)
            {
                string message = messages[i];

                tasks[i] = Task.Run(async () =>
                {
                    await UseAsyncSemaphore(message);
                });
            }

            await Task.WhenAll(tasks);
            Console.WriteLine();
        }

        private static SemaphoreSlim s_asyncLock = new SemaphoreSlim(1);
        static async Task LockWithSemaphore(string title)
        {
            Console.WriteLine($"{title} waiting for lock");
            await s_asyncLock.WaitAsync();
            try
            {
                Console.WriteLine($"{title} {nameof(LockWithSemaphore)} started");
                await Task.Delay(500);
                Console.WriteLine($"{title} {nameof(LockWithSemaphore)} ending");
            }
            finally
            {
                s_asyncLock.Release();
            }
        }

        private static AsyncSemaphore s_asyncSemaphore = new AsyncSemaphore();
        static async Task UseAsyncSemaphore(string title)
        {
            using (await s_asyncSemaphore.WaitAsync())
            {
                Console.WriteLine($"{title} {nameof(LockWithSemaphore)} started");
                await Task.Delay(500);
                Console.WriteLine($"{title} {nameof(LockWithSemaphore)} ending");
            }
        }
    }
}
