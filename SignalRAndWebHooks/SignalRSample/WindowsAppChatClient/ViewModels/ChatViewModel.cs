using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using WindowsAppChatClient.Framework;
using WindowsAppChatClient.Services;

namespace WindowsAppChatClient.ViewModels
{
    public class ChatViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly UrlService _urlService;
        public ChatViewModel(IDialogService dialogService, UrlService urlService)
        {
            _dialogService = dialogService;
            _urlService = urlService;

            ConnectCommand = new RelayCommand(OnConnect);
            SendCommand = new RelayCommand(OnSendMessage);
        }

        public string Name { get; set; }
        public string Message { get; set; }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

        public RelayCommand SendCommand { get; }

        public RelayCommand ConnectCommand { get; }

        private HubConnection _hubConnection;

        public async void OnConnect()
        {
            await CloseConnectionAsync();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_urlService.ChatAddress)
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddDebug())
                .Build();

            _hubConnection.Closed += HubConnectionClosed;

            _hubConnection.On<string, string>("BroadcastMessage", OnMessageReceived);

            try
            {
                await _hubConnection.StartAsync();
                await _dialogService.ShowMessageAsync("client connected");
            }
            catch (HttpRequestException ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        private Task HubConnectionClosed(Exception arg) 
            => _dialogService.ShowMessageAsync("Hub connection closed");

        public async void OnSendMessage()
        {
            try
            {
                await _hubConnection.SendAsync("Send", Name, Message);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        public async void OnMessageReceived(string name, string message)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Messages.Add($"{name}: {message}");
                });
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        private Task CloseConnectionAsync() 
            => _hubConnection?.DisposeAsync() ?? Task.CompletedTask;
    }
}
