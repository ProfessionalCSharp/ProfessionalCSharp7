using LuisBotSample.Extensions;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace LuisBotSample.Dialogs
{
    [Serializable]
    public sealed class RestaurantReservation
    {
        public DayOfWeek Weekday
        {
            get => Day?.DayOfWeek ?? DayOfWeek.Sunday;
            set => Day = DateTime.Today.AddDays(value.DaysUntilNext());
        }

        [Prompt("Please enter the {&} for the reservation")]
        public DateTime? Day { get; set; }

        [Prompt("Please enter the {&}")]
        public DateTime? Time { get; set; }

        [Numeric(1, 50)]
        [Prompt("For how many people should be reserved?")]
        public int? Number { get; set; }

        public override string ToString() => $"Reservation for {Number} people on {Day:d} at {Time:t}";

        public const string Reservation_Day = "Reservation.Day";
        public const string Reservation_Number = "Reservation.Number";
        public const string Reservation_Time = "Reservation.Time";
        public const string Reservation_Weekday = "Reservation.Weekday";
    }
}