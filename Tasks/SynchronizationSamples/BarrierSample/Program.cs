using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierSample
{
    class Program
    {
        static void Main()
        {
            const int numberTasks = 2;
            const int partitionSize = 1000000;
            const int loops = 5;
            var taskResults = new Dictionary<int, int[][]>();
            var data = new List<string>[loops];
            for (int i = 0; i < loops; i++)
            {
                data[i] = new List<string>(FillData(partitionSize * numberTasks));
            }

            var barrier = new Barrier(1);
            LogBarrierInformation("initial participants in barrier", barrier);

            for (int i = 0; i < numberTasks; i++)
            {
                barrier.AddParticipant();

                int jobNumber = i;
                taskResults.Add(i, new int[loops][]);
                for (int loop = 0; loop < loops; loop++)
                {
                    taskResults[i][loop] = new int[26];
                }
                Console.WriteLine($"Main - starting task job {jobNumber}");
                Task.Run(() => CalculationInTask(jobNumber, partitionSize, barrier, data, loops, taskResults[jobNumber]));
            }

            for (int loop = 0; loop < 5; loop++)
            {
                LogBarrierInformation("main task, start signaling and wait", barrier);
                barrier.SignalAndWait();
                LogBarrierInformation("main task waiting completed", barrier);
                //                var resultCollection = tasks[0].Result.Zip(tasks[1].Result, (c1, c2) => c1 + c2);
                int[][] resultCollection1 = taskResults[0];
                int[][] resultCollection2 = taskResults[1];
                var resultCollection = resultCollection1[loop].Zip(resultCollection2[loop], (c1, c2) => c1 + c2);

                char ch = 'a';
                int sum = 0;
                foreach (var x in resultCollection)
                {
                    Console.WriteLine($"{ch++}, count: {x}");
                    sum += x;
                }

                LogBarrierInformation($"main task finished loop {loop}, sum: {sum}", barrier);
            }

            Console.WriteLine("at the end");
            Console.ReadLine();
        }

        public static IEnumerable<string> FillData(int size)
        {
            var r = new Random();
            return Enumerable.Range(0, size).Select(x => GetString(r));
        }

        private static string GetString(Random r)
        {
            var sb = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                sb.Append((char)(r.Next(26) + 97));
            }
            return sb.ToString();
        }

        private static void CalculationInTask(int jobNumber, int partitionSize, Barrier barrier, IList<string>[] coll, int loops, int[][] results)
        {

            LogBarrierInformation("CalculationInTask started", barrier);

            for (int i = 0; i < loops; i++)
            {
                var data = new List<string>(coll[i]);

                int start = jobNumber * partitionSize;
                int end = start + partitionSize;
                Console.WriteLine($"Task {Task.CurrentId} in loop {i}: partition from {start} to {end}");

                for (int j = start; j < end; j++)
                {
                    char c = data[j][0];
                    results[i][c - 97]++;
                }

                Console.WriteLine($"Calculation completed from task {Task.CurrentId} in loop {i}. " +
                    $"{results[i][0]} times a, {results[i][25]} times z");

                LogBarrierInformation("sending signal and wait for all", barrier);
                barrier.SignalAndWait();
                LogBarrierInformation("waiting completed", barrier);
            }

            barrier.RemoveParticipant();
            LogBarrierInformation("finished task, removed participant", barrier);
        }


        private static void LogBarrierInformation(string info, Barrier barrier)
        {
            Console.WriteLine($"Task {Task.CurrentId}: {info}. {barrier.ParticipantCount} current and {barrier.ParticipantsRemaining} remaining participants, phase {barrier.CurrentPhaseNumber}");
        }

    }
}
