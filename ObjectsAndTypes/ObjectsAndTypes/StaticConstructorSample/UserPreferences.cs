using System;

namespace StaticConstructorSample
{
    public static class UserPreferences
    {
        public static Color BackColor { get;  }

        static UserPreferences()
        {
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
            {
                BackColor = Color.Green;
            }
            else
            {
                BackColor = Color.Red;
            }
        }
    }
}