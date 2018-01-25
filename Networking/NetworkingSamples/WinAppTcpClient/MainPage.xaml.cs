using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinAppTcpClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged, IDisposable
    {
        private TcpClient _client = new TcpClient();
        private readonly CustomProtocolCommands _commands = new CustomProtocolCommands();

        public MainPage() => InitializeComponent();

        private async void OnConnect(object sender, RoutedEventArgs e)
        {
            try
            {
                await _client.ConnectAsync(RemoteHost, ServerPort);
            }
            catch (SocketException ex) when (ex.HResult == 0x2748)
            {
                _client.Dispose();
                _client = new TcpClient();
                await new MessageDialog("Please retry connect").ShowAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        public IEnumerable<CustomProtocolCommand> Commands => _commands;

        private async Task<bool> VerifyIsConnectedAsync()        {
            if (!_client.Connected)
            {
                await new MessageDialog("connect first").ShowAsync();
                return false;
            }
            return true;
        }

        #region Properties
        private string _remoteHost = "localhost";
        public string RemoteHost
        {
            get => _remoteHost;
            set => SetProperty(ref _remoteHost, value);
        }

        private int _serverPort = 8800;

        public int ServerPort
        {
            get => _serverPort;
            set => SetProperty(ref _serverPort, value);
        }

        private string _sessionId;
        public string SessionId
        {
            get => _sessionId; 
            set => SetProperty(ref _sessionId, value);
        }

        private CustomProtocolCommand _activeCommand;
        public CustomProtocolCommand ActiveCommand
        {
            get => _activeCommand;
            set => SetProperty(ref _activeCommand, value);
        }

        private string _log;
        public string Log
        {
            get => _log;
            set => SetProperty(ref _log, value);
        }

        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value); 
        }
        #endregion

        #region INotifyPropertyChanged

        private bool SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(item, value)) return false;

            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public void Dispose() => _client?.Dispose();

        private string GetSessionHeader()
        {
            if (string.IsNullOrEmpty(SessionId)) return string.Empty;
            return $"ID::{SessionId}::";
        }

        private string GetCommand() =>
            $"{GetSessionHeader()}{ActiveCommand?.Name}::{ActiveCommand?.Action}";

        private async void OnSendCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!await VerifyIsConnectedAsync()) return;
                NetworkStream stream = _client.GetStream();
                byte[] writeBuffer = Encoding.ASCII.GetBytes(GetCommand());
                await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                await stream.FlushAsync();
                byte[] readBuffer = new byte[1024];
                int read = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
                string messageRead = Encoding.ASCII.GetString(readBuffer, 0, read);
                Log += messageRead + Environment.NewLine;
                ParseMessage(messageRead);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        private void ParseMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            string[] messageColl = message.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            Status = messageColl[0];
            SessionId = GetSessionId(messageColl);
        }
        private string GetSessionId(string[] messageColl) =>
            messageColl.Length >= 2 && messageColl[1] == "ID" ? messageColl[2] : string.Empty;

        private void OnClearLog(object sender, RoutedEventArgs e) =>
            Log = string.Empty;
    }
}
