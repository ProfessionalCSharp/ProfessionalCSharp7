using System;

namespace EnumSample
{
    class Program
    {
        static void Main()
        {
            DaysOfWeekSamples();
            ColorSamples();
            UsingEnumClass();

            Console.ReadLine();
        }

        private static void UsingEnumClass()
        {
            Color red;
            if (Enum.TryParse<Color>("Red", out red))
            {
                Console.WriteLine($"successfully parsed {red}");
            }

            string redtext = Enum.GetName(typeof(Color), red);
            Console.WriteLine(redtext);

            foreach (var day in Enum.GetNames(typeof(Color)))
            {
                Console.WriteLine(day);
            }


            foreach (short val in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine(val);
            }

            foreach (var item in Enum.GetValues(typeof(Color)))
            {
                Console.WriteLine(item);
            }
        }

        private static void DaysOfWeekSamples()
        {
            DaysOfWeek mondayAndWednesday = DaysOfWeek.Monday | DaysOfWeek.Wednesday;
            Console.WriteLine(mondayAndWednesday);
            DaysOfWeek weekend = DaysOfWeek.Saturday | DaysOfWeek.Sunday;
            Console.WriteLine(weekend);
            DaysOfWeek workday = DaysOfWeek.Monday | DaysOfWeek.Tuesday | DaysOfWeek.Wednesday | DaysOfWeek.Thursday  | DaysOfWeek.Friday;
            Console.WriteLine(workday);
        }

        private static void ColorSamples()
        {
            Color c1 = Color.Red;
            Console.WriteLine(c1);

            Color c2 = (Color)2;
            Console.WriteLine(c2);
            Console.WriteLine((short)c2);
        }
    }
}
