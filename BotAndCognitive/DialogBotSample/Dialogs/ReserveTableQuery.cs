using Microsoft.Bot.Builder.FormFlow;
using System;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class ReserveTableQuery
    {
        [Prompt("Please enter the {&} for the reservation")]
        public DateTime? Date { get; set; }

        [Prompt("Please enter the {&}")]
        public DateTime? Time { get; set; }

        [Numeric(1, 50)]
        [Prompt("For how many {&} should be reserved?")]
        public int? People { get; set; }
    }
}