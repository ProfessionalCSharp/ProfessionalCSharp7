using System;

namespace Framework.Services
{
    public interface IEditModeService
    {
        event EventHandler<bool> EditModeChanged;
        bool IsEditMode { get; set; }
        bool IsReadMode { get; }
    }
}