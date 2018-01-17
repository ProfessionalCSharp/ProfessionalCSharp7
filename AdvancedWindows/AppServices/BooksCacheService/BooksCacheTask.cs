using BooksCacheModel;
using System;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace BooksCacheService
{
    public sealed class BooksCacheTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _taskDeferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _taskDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;

            var trigger = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            AppServiceConnection connection = trigger.AppServiceConnection;
            connection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            AppServiceDeferral deferral = args.GetDeferral();
            try
            {
                ValueSet message = args.Request.Message;
                ValueSet result = null;

                switch (message["command"].ToString())
                {
                    case "GET":
                        result = GetBooks();
                        break;
                    case "POST":
                        result = AddBook(message["book"].ToString());
                        break;
                    default:
                        break;
                }

                await args.Request.SendResponseAsync(result);
            }
            finally
            {
                deferral.Complete();
            }
        }

        private ValueSet GetBooks()
        {
            var result = new ValueSet();
            result.Add("result", BooksRepository.Instance.Books.ToJson());
            return result;
        }

        private ValueSet AddBook(string book)
        {
            BooksRepository.Instance.AddBook(book.ToBook());
            var result = new ValueSet();
            result.Add("result", "ok");
            return result;
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason) =>
            _taskDeferral?.Complete();
    }
}
