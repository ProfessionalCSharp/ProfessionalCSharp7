using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class ReserveTableDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to reserving a table!");

            var reserveTableQuery = FormDialog.FromForm(BuildReserveTableForm, FormOptions.PromptFieldsWithValues);

            context.Call(reserveTableQuery, ResumeAfterReserveTableFormDialog);
        }

        public IForm<ReserveTableQuery> BuildReserveTableForm()
        {
            OnCompletionAsyncDelegate<ReserveTableQuery> reservationAnswer = async (context, reservation) =>
                await context.PostAsync($"Thanks. Reserving {reservation.People} seats on {reservation.Date:D} at {reservation.Time:t}");

            return new FormBuilder<ReserveTableQuery>()
                .Field(nameof(ReserveTableQuery.Date))
                .Confirm("Looking to reserve a seat at {Date:d}?")
                .AddRemainingFields()
                .OnCompletion(reservationAnswer)
                .Build();
        }

        public async Task ResumeAfterReserveTableFormDialog(IDialogContext context, IAwaitable<ReserveTableQuery> result)
        {
            try
            {
                var reservation = await result;

                // TODO: call the reservation API of the restaurant
                var resultMessage = context.MakeMessage();

                var heroCard = new HeroCard
                {
                    Title = "Reservation",
                    Subtitle = $"for {reservation.People} at the date {reservation.Date:D} and time {reservation.Time:t}",
                    Images = new List<CardImage>()
                    {
                        new CardImage { Url = "https://kantinem101.blob.core.windows.net/menuimages/Hirschragout_250"}
                    }
                };
                resultMessage.Attachments.Add(heroCard.ToAttachment());

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply = "You canceled the operation. Quitting from the reservation.";
                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }
    }
}