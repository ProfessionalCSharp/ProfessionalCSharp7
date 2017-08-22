using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadingIssues
{
    public class StateObject
    {
        private int _state = 5;
        private object _sync = new object();

        public void ChangeState(int loop)
        {
            //            lock (sync)
            {
                if (_state == 5)
                {
                    _state++;
                    if (_state != 6)
                    {
                        Console.WriteLine($"Race condition occured after {loop} loops");
                        Trace.Fail($"race condition at {loop}");
                    }
                }
                _state = 5;
            }
        }
    }

    public class SampleTask
    {
        //internal static int a;
        //private static Object sync = new object();

        public SampleTask()
        {

        }

        public void RaceCondition(object o)
        {
            Trace.Assert(o is StateObject, "o must be of type StateObject");
            StateObject state = o as StateObject;
            Console.WriteLine("starting RaceCondition - when does the issue occur?");

            int i = 0;
            while (true)
            {
                // lock (state) // no race condition with this lock
                {
                    state.ChangeState(i++);
                }
            }
        }

        public SampleTask(StateObject s1, StateObject s2)
        {
            _s1 = s1;
            _s2 = s2;
        }

        private StateObject _s1;
        private StateObject _s2;
        
        public void Deadlock1()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine("1 - waiting for s1");
                lock (_s1)
                {
                    Console.WriteLine("1 - s1 locked, waiting for s2");
                    lock (_s2)
                    {
                        Console.WriteLine("1 - s1 and s2 locked, now go on...");
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"1 still running, i: {i}");
                    }
                }
            }
        }

        public void Deadlock2()
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine("2 - waiting for s2");
                lock (_s2)
                {
                    Console.WriteLine("2 - s2 locked, waiting for s1");
                    lock (_s1)
                    {
                        Console.WriteLine("2 - s1 and s2 locked, now go on...");
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"2 still running, i: {i}");
                    }
                }
            }
        }
    }
}