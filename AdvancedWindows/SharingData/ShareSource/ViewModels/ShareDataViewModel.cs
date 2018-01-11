using ShareSource.BooksExtensions;
using ShareSource.Models;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;

namespace ShareSource.ViewModels
{
    public class ShareDataViewModel
    {
        public ShareDataViewModel()
        {
            DataTransferManager.GetForCurrentView().DataRequested += ShareDataRequested;
        }

        public string SimpleText { get; set; } = string.Empty;

        private void ShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var books = new BooksRepository().GetSampleBooks();

            Uri baseUri = new Uri("ms-appx:///");
            DataPackage package = args.Request.Data;
            package.Properties.Title = "Sharing Sample";
            package.Properties.Description = "Sample for sharing data";
            package.Properties.Thumbnail = RandomAccessStreamReference.CreateFromUri(
                new Uri(baseUri, "Assets/Square44x44Logo.png"));
            package.SetText(SimpleText);
            package.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(books.ToHtml()));
        }

        public void ShowShareUI() =>
            DataTransferManager.ShowShareUI();
    }
}
