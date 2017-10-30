using System;

namespace Utilities
{
    internal class Command<T>
    {
        public Command(string option, string text, Action<T> action)
        {
            Option = option;
            Text = text;
            Action = action;
        }

        public string Option { get; }
        public string Text { get; }
        public Action<T> Action { get; }
    }
}
