using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSample
{
    public class Calculator
    {
        private ManualResetEventSlim _mEvent;

        public int Result { get; private set; }

        public Calculator(ManualResetEventSlim ev)
        {
            _mEvent = ev;
        }

        public void Calculation(int x, int y)
        {
            Console.WriteLine($"Task {Task.CurrentId} starts calculation");
            Task.Delay(new Random().Next(3000)).Wait();
            Result = x + y;

            // signal the event-completed!
            Console.WriteLine($"Task {Task.CurrentId} is ready");
            _mEvent.Set();
        }
    }

}
