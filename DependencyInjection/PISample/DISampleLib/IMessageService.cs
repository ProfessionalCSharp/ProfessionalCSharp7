using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DISampleLib
{
    public interface IMessageService
    {
        Task ShowMessageAsync(string message);
    }
}
