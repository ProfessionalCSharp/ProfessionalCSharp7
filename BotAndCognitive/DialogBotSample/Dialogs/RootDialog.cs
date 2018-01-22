using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string FindNumberOption = "find a number";
        private const string OrderLunchOption = "order lunch";
        private const string ReserveTableOption = "reserve a table";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Contains("help") || message.Text.ToLower().Contains("support"))
            {
                await context.Forward(new SupportDialog(), ResumeAfterSupportDialog, message, CancellationToken.None);
            }
            else
            {
                ShowOptions(context);
            }
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, OnOptionSelected, new List<string>() { FindNumberOption, OrderLunchOption, ReserveTableOption }, "Are you looking to order a lunch or reserve a seat?", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case OrderLunchOption:
                        context.Call(new OrderLunchDialog(), ResumeAfterOptionDialog);
                        break;
                    case ReserveTableOption:
                        context.Call(new ReserveTableDialog(), ResumeAfterOptionDialog);
                        break;
                    case FindNumberOption:
                        context.Call(new FindNumberDialog(), ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync("Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {

                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<int> result)
        {
            var message = await result;
            context.Wait(MessageReceivedAsync);
        }

            private async Task ResumeAfterSupportDialog(IDialogContext context, IAwaitable<int> result)
        {
            var ticket = await result;
            await context.PostAsync($"Thanks for contacting support. Your ticket number is {ticket}");
            context.Wait(MessageReceivedAsync);
        }
    }
}