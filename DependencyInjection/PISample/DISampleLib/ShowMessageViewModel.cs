using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DISampleLib
{
    public class ShowMessageViewModel
    {
        private readonly IMessageService _messageService;
        public ShowMessageViewModel(IMessageService messageService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));

            ShowMessageCommand = new RelayCommand(ShowMessage);
        }

        public ICommand ShowMessageCommand { get; }

        public void ShowMessage()
        {
            _messageService.ShowMessageAsync("A message from the view-model");
        }
    }
}
