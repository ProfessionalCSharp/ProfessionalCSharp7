using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Foundations
{
    class Program
    {

        private static readonly Command[] commands =
        {
            new Command("-async", nameof(CallerWithAsync), CallerWithAsync),
            new Command("-async2", nameof(CallerWithAsync2), CallerWithAsync2),
            new Command("-awaiter", nameof(CallerWithAwaiter), CallerWithAwaiter),
            new Command("-cont", nameof(CallerWithContinuationTask), CallerWithContinuationTask),
            new Command("-masync", nameof(MultipleAsyncMethods), MultipleAsyncMethods),
            new Command("-comb", nameof(MultipleAsyncMethodsWithCombinators1), MultipleAsyncMethodsWithCombinators1),
            new Command("-comb2", nameof(MultipleAsyncMethodsWithCombinators2), MultipleAsyncMethodsWithCombinators2),
            new Command("-val", nameof(UseValueTask), UseValueTask),
            new Command("-casync", nameof(ConvertingAsyncPattern), ConvertingAsyncPattern)
        };

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }

            Command command = commands.SingleOrDefault(c => c.Option == args[0]);
            if (command == null)
            {
                ShowUsage();
                return;
            }
            command.Action();
            
            Console.ReadLine();
        }


        private static void ShowUsage()
        {
            Console.WriteLine("Usage: Foundations [options]");
            Console.WriteLine();
            foreach (var command in commands)
            {
                Console.WriteLine($"{command.Option} {command.Text}");
            }
        }

        private static async void UseValueTask()
        {
            TraceThreadAndTask($"start {nameof(UseValueTask)}");
            string result = await GreetingValueTask2Async("Katharina");
            Console.WriteLine(result);
            TraceThreadAndTask($"first result {nameof(UseValueTask)}");
            string result2 = await GreetingValueTask2Async("Katharina");
            Console.WriteLine(result2);
            TraceThreadAndTask($"ended {nameof(UseValueTask)}");
        }

        private static async void ConvertingAsyncPattern()
        {
            HttpWebRequest request = WebRequest.Create("http://www.microsoft.com") as HttpWebRequest;

            using (WebResponse response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse(null, null), request.EndGetResponse))
            {
                Stream stream = response.GetResponseStream();
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
        }

        private static async void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {s1}{Environment.NewLine} Result 2: {s2}");
        }

        private static async void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            await Task.WhenAll(t1, t2);
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {t1.Result}{Environment.NewLine} Result 2: {t2.Result}");
        }

        private static async void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            string[] result = await Task.WhenAll(t1, t2);
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {result[0]}{Environment.NewLine} Result 2: {result[1]}");
        }

        private static void CallerWithContinuationTask()
        {
            TraceThreadAndTask($"started {nameof(CallerWithContinuationTask)}");

            var t1 = GreetingAsync("Stephanie");

            t1.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);

                TraceThreadAndTask($"ended {nameof(CallerWithContinuationTask)}");
            });
        }

        private static void CallerWithAwaiter()
        {
            TraceThreadAndTask($"starting {nameof(CallerWithAwaiter)}");
            TaskAwaiter<string> awaiter = GreetingAsync("Matthias").GetAwaiter();
            awaiter.OnCompleted(OnCompleteAwaiter);

            void OnCompleteAwaiter()
            {
                Console.WriteLine(awaiter.GetResult());
                TraceThreadAndTask($"ended {nameof(CallerWithAwaiter)}");
            }
        }

        private static async void CallerWithAsync()
        {
            TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
            string result = await GreetingAsync("Stephanie");
            Console.WriteLine(result);
            TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
        }

        private static async void CallerWithAsync2()
        {
            TraceThreadAndTask($"started {nameof(CallerWithAsync2)}");
            Console.WriteLine(await GreetingAsync("Stephanie"));
            TraceThreadAndTask($"ended {nameof(CallerWithAsync2)}");
        }

        static Task<string> GreetingAsync(string name) =>
            Task.Run(() =>
            {
                TraceThreadAndTask($"running {nameof(GreetingAsync)}");
                return Greeting(name);
            });

        private readonly static Dictionary<string, string> names = new Dictionary<string, string>();

        static async ValueTask<string> GreetingValueTaskAsync(string name)
        {
            if (names.TryGetValue(name, out string result))
            {
                return result;
            }
            else
            {
                result = await GreetingAsync(name);
                names.Add(name, result);
                return result;                
            }
        }

        static ValueTask<string> GreetingValueTask2Async(string name)
        {
            if (names.TryGetValue(name, out string result))
            {
                return new ValueTask<string>(result);
            }
            else
            {
                Task<string> t1 =  GreetingAsync(name);
                
                TaskAwaiter<string> awaiter = t1.GetAwaiter();
                awaiter.OnCompleted(OnCompletion);
                return new ValueTask<string>(t1);

                void OnCompletion()
                {
                    names.Add(name, awaiter.GetResult());
                }
            }
        }

        static string Greeting(string name)
        {
            TraceThreadAndTask($"running {nameof(Greeting)}");
            Task.Delay(3000).Wait();
            return $"Hello, {name}";
        }

        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;

            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
        }
    }
}