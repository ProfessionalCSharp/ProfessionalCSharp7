using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    internal class Command
    {
        public Command(string option, string text, Action action)
        {
            Option = option;
            Text = text;
            Action = action;
        }

        public Command(string option, string text, Func<Task> asyncAction)
        {
            Option = option;
            Text = text;
            ActionAsync = asyncAction;
        }

        public string Option { get; }
        public string Text { get; }
        public Action Action { get; }
        public Func<Task> ActionAsync { get; }
    }
}
