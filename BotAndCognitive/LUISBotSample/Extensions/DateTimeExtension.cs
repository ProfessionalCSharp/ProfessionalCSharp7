using System;

namespace LuisBotSample.Extensions
{
    public static class DateTimeExtension
    {
        public static int DaysUntilNext(this DayOfWeek dayOfWeek)
        {
            int todayDayOfWeek = (int)DateTime.Today.DayOfWeek;
            if (todayDayOfWeek < (int)dayOfWeek)
            {
                return ((int)dayOfWeek - todayDayOfWeek);
            }
            else
            {
                return ((int)dayOfWeek - todayDayOfWeek) + 7;
            }
        }
    }
}