using System;

namespace EnumerationSample
{
    public enum TimeOfDay
    {
        Morning = 0,
        Afternoon = 1,
        Evening = 2
    }

    class Program
    {
        static void Main()
        {
            WriteGreeting(TimeOfDay.Morning);
        }

        static void WriteGreeting(TimeOfDay timeOfDay)
        {
            switch (timeOfDay)
            {
                case TimeOfDay.Morning:
                    Console.WriteLine("Good morning!");
                    break;
                case TimeOfDay.Afternoon:
                    Console.WriteLine("Good afternoon!");
                    break;
                case TimeOfDay.Evening:
                    Console.WriteLine("Good evening!");
                    break;
                default:
                    Console.WriteLine("Hello!");
                    break;
            }
        }
    }
}
