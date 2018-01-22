using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace DialogBotSample.Dialogs
{
    [Serializable]
    public class FindNumberDialog : IDialog<int>
    {
        private int _theNumber;
        private bool _success = false;
        private int _loop = 0;

        public async Task StartAsync(IDialogContext context)
        {
            _theNumber = new Random().Next(1, 50);
            await context.PostAsync("Find a number between 1 and 50 with a max of 6 attempts");
            PromptDialog.Number(context, new ResumeAfter<long>(ProcessNumber), "enter a number between 1 and 50");
        }

        private async Task ProcessNumber(IDialogContext context, IAwaitable<long> result)
        {
            var number = await result;
            _loop++;
            if (number == _theNumber)
            {
                _success = true;
            }
            else if (number < _theNumber)
            {
                await context.PostAsync("too small, try again");
            }
            else
            {
                await context.PostAsync("too big, try again");
            }
            if (!_success && _loop < 6)
            {
                PromptDialog.Number(
                    context, 
                    ProcessNumber, 
                    "enter a number between 1 and 50");
            }
            else
            {
                string message;
                if (_success)
                {
                    message = $"Success - you made it! The correct number is {_theNumber}";
                }
                else
                {
                    message = $"Better luck next time. The correct number is {_theNumber}";
                }
                await context.PostAsync(message);
                context.Done(_theNumber);
            }
        }
    }
}