using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.UI.Popups;

namespace ShareTarget.ViewModels
{
    public class ShareTargetPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Set<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }

        private ShareOperation _shareOperation;
        private readonly ObservableCollection<string> _shareFormats = new ObservableCollection<string>();
        public string SelectedFormat { get; set; }
        public IEnumerable<string> ShareFormats => _shareFormats;

        public void Activate(ShareOperation shareOperation)
        {
            if (shareOperation == null) throw new ArgumentNullException(nameof(shareOperation));

            string title = null;
            string description = null;
            try
            {
                _shareOperation = shareOperation;

                title = _shareOperation.Data.Properties.Title;
                description = _shareOperation.Data.Properties.Description;
                foreach (var format in _shareOperation.Data.AvailableFormats)
                {
                    _shareFormats.Add(format);
                }

                Title = title;
                Description = description;

            }
            catch (Exception ex)
            {
                _shareOperation.ReportError(ex.Message);
            }
        }

        public void ReportCompleted() =>
            _shareOperation.ReportCompleted();

        private bool _dataRetrieved = false;
        public async void RetrieveData()
        {
            try
            {
                if (_dataRetrieved)
                {
                    await new MessageDialog("data already retrieved").ShowAsync();
                }
                _shareOperation.ReportStarted();
                switch (SelectedFormat)
                {
                    case "Text":
                        Text = await _shareOperation.Data.GetTextAsync();
                        break;
                    case "HTML Format":
                        Html = await _shareOperation.Data.GetHtmlFormatAsync();
                        break;
                    default:
                        break;
                }
                _shareOperation.ReportDataRetrieved();
                _dataRetrieved = true;
            }
            catch (Exception ex)
            {

                _shareOperation.ReportError(ex.Message);
            }
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        private string _html;
        public string Html
        {
            get => _html;
            set => Set(ref _html, value);
        }

        private string _title;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _description;

        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }
    }
}
