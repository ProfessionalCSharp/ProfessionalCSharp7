using DISampleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
    public class WPFMessageService : IMessageService
    {
        public Task ShowMessageAsync(string message)           
        {
            MessageBox.Show(message);
            return Task.CompletedTask;
        }
    }
}
