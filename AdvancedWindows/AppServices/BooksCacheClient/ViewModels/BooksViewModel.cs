using BooksCacheModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

namespace BooksCacheClient.ViewModels
{
    public class BooksViewModel
    {
        private const string BookServiceName = "com.CNinnovation.BooksCache";
        private const string BooksPackageFamilyName = "085f62ed-e72b-4c07-9970-b4d01c066dd6_p2wxv0ry6mv8g";

        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
        public async void GetBooksAsync()
        {
            var message = new ValueSet();
            message.Add("command", "GET");
            string json = await SendMessageAsync(message);
            IEnumerable<Book> books = json.ToBooks();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }
    
        public string NewBookTitle { get; set; }
        public string NewBookPublisher { get; set; }

        public async void PostBookAsync()
        {
            var message = new ValueSet();
            message.Add("command", "POST");
            string json = new Book { Title = NewBookTitle, Publisher = NewBookPublisher }.ToJson();
            message.Add("book", json);
            string result = await SendMessageAsync(message);
        }
        private async Task<string> SendMessageAsync(ValueSet message)
        {
            using (var connection = new AppServiceConnection())
            {
                connection.AppServiceName = BookServiceName;
                connection.PackageFamilyName = BooksPackageFamilyName;

                AppServiceConnectionStatus status = await connection.OpenAsync();
                if (status == AppServiceConnectionStatus.Success)
                {
                    AppServiceResponse response = await connection.SendMessageAsync(message);
                    if (response.Status == AppServiceResponseStatus.Success && response.Message.ContainsKey("result"))
                    {
                        string result = response.Message["result"].ToString();
                        return result;
                    }
                    else
                    {
                        await ShowServiceErrorAsync(response.Status);
                    }
                }
                else
                {
                    await ShowConnectionErrorAsync(status);
                }

                return string.Empty;
            }
        }

        private async Task ShowServiceErrorAsync(AppServiceResponseStatus status)
        {
            string error = null;
            switch (status)
            {
                case AppServiceResponseStatus.Success:
                    error = "Service did not return the expected result";
                    break;
                case AppServiceResponseStatus.Failure:
                    error = "Service failed to answer";
                    break;
                case AppServiceResponseStatus.ResourceLimitsExceeded:
                    error = "Service exceeded the resource limits and was terminated";
                    break;
                case AppServiceResponseStatus.Unknown:
                    error = "Unknown error";
                    break;
                default:
                    break;
            }
            await new MessageDialog(error).ShowAsync();
        }

        private async Task ShowConnectionErrorAsync(AppServiceConnectionStatus status)
        {
            string error = null;
            switch (status)
            {
                case AppServiceConnectionStatus.AppNotInstalled:
                    error = "The Book Cache service is not installed.Deploy the BooksCacheProvider.";
                    break;
                case AppServiceConnectionStatus.AppUnavailable:
                    error = "The Book Cache service is not available. Maybe an update is in progress, or the app location is not available";
                    break;
                case AppServiceConnectionStatus.AppServiceUnavailable:
                    error = "The Book Cache service is not available";
                    break;
                case AppServiceConnectionStatus.Unknown:
                    error = "Unknown error";
                    break;
                default:
                    break;
            }
            await new MessageDialog(error).ShowAsync();
        }
    }
}
