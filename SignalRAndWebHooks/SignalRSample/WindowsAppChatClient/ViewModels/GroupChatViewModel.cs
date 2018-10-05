using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using WindowsAppChatClient.Framework;
using WindowsAppChatClient.Services;

namespace WindowsAppChatClient.ViewModels
{
    public sealed class GroupChatViewModel : INotifyPropertyChanged
    {
        private readonly IDialogService _dialogService;
        private readonly UrlService _urlService;
        public GroupChatViewModel(IDialogService dialogService, UrlService urlService)
        {
            _dialogService = dialogService;
            _urlService = urlService;

            ConnectCommand = new RelayCommand(OnConnect);
            SendCommand = new RelayCommand(OnSendMessage);
            EnterGroupCommand = new RelayCommand(OnEnterGroup);
            LeaveGroupCommand = new RelayCommand(OnLeaveGroup);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Set<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Name { get; set; }
        public string Message { get; set; }
        public string NewGroup { get; set; }

        private string _selectedGroup;
        public string SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }

        public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();

        public ICommand SendCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand EnterGroupCommand { get; }
        public ICommand LeaveGroupCommand { get; }

        private HubConnection _hubConnection;

        public async void OnConnect()
        {
            await CloseConnectionAsync();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_urlService.GroupAddress)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddDebug();
                })
                .Build();

            _hubConnection.Closed += HubConnectionClosed;

            _hubConnection.On<string, string, string>("MessageToGroup", OnMessageReceived);

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

        public async void OnSendMessage()
        {
            try
            {
                await _hubConnection.SendAsync("Send", SelectedGroup, Name, Message);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        public async void OnMessageReceived(string group, string name, string message)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Messages.Add($"{group}-{name}: {message}");
                });
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        public async void OnEnterGroup()
        {
            try
            {
                await _hubConnection.InvokeAsync("AddGroup", NewGroup);
                Groups.Add(NewGroup);
                SelectedGroup = NewGroup;
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        public async void OnLeaveGroup()
        {
            try
            {
                await _hubConnection.InvokeAsync("LeaveGroup", SelectedGroup);
                Groups.Remove(SelectedGroup);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageAsync(ex.Message);
            }
        }

        private Task HubConnectionClosed(Exception arg) 
            => _dialogService.ShowMessageAsync("Hub connection closed");

        private Task CloseConnectionAsync() =>
            _hubConnection?.DisposeAsync() ?? Task.CompletedTask;
    }
}
