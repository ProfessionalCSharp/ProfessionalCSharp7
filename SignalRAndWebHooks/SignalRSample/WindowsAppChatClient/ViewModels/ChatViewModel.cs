using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using WindowsAppChatClient.Framework;
using WindowsAppChatClient.Services;

namespace WindowsAppChatClient.ViewModels
{
    public class ChatViewModel
    {
        private const string ServerURI = "http://localhost:13773/chat";
        private readonly IMessagingService _messagingService;
        private readonly ILoggerFactory _loggerFactory;
        public ChatViewModel(IMessagingService messagingService, ILoggerFactory loggerFactory)
        {
            _messagingService = messagingService;
            _loggerFactory = loggerFactory;

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
                .WithUrl(ServerURI)
                .WithLogger(loggerFactory =>
                {
                   
                })
                .Build();

            _hubConnection.Closed += HubConnection_Closed;

            _hubConnection.On<string, string>("BroadcastMessage", (name, message) =>
            {
                OnMessageReceived(name, message);
            });

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (HttpRequestException ex)
            {
                await _messagingService.ShowMessageAsync(ex.Message);
            }
            await _messagingService.ShowMessageAsync("client connected");
        }

        private async Task HubConnection_Closed(Exception arg)
        {
            await _messagingService.ShowMessageAsync("Hub connection closed");
        }

        public void OnSendMessage()
        {
            _hubConnection.SendAsync("Send", Name, Message);
        }

        public async void OnMessageReceived(string name, string message)
        {
            try
            {
                // this
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Messages.Add($"{name}: {message}");
                });
                //// or that
                //await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                //{
                //    Messages.Add($"{name}: {message}");
                //});
            }
            catch (Exception ex)
            {
                await _messagingService.ShowMessageAsync(ex.Message);
            }

        }

        private async void HubConnectionClosed()
        {
            await _messagingService.ShowMessageAsync("Hub connection closed");
        }

        private Task CloseConnectionAsync() =>
            _hubConnection?.DisposeAsync() ?? Task.CompletedTask;
    }
}
