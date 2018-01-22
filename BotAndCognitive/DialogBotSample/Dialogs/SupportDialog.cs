using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class SupportDialog : IDialog<int>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            var ticketNumber = new Random().Next(0, 10000);

            await context.PostAsync($"Your message '{message.Text}' was registered. Once we resolve it; we will get back to you.");

            context.Done(ticketNumber);
        }
    }
}