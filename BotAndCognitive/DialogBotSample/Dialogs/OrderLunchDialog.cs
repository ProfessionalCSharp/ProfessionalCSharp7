using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class OrderLunchDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Fail(new NotImplementedException("Order Lunch Dialog not implemented yet"));
            return Task.CompletedTask;
        }
    }
}