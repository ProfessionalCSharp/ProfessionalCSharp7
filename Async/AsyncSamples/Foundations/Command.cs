using System;
using System.Collections.Generic;
using System.Text;

namespace Foundations
{
    class Command
    {
        public Command(string option, string text, Action action)
        {
            Option = option;
            Text = text;
            Action = action;
        }

        public string Option { get; }
        public string Text { get; }
        public Action Action { get; }
    }
}
