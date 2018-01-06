using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;

namespace ApplicationLifetime.Services
{
    public class DataManager : INotifyPropertyChanged
    {
        private const string SessionStateFile = "TempSessionState.json";
        private Dictionary<string, string> _state = new Dictionary<string, string>()
        {
            [nameof(Session1)] = string.Empty,
            [nameof(Session2)] = string.Empty
        };

        private DataManager()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static DataManager Instance { get; } = new DataManager();

        public string Session1
        {
            get => _state[nameof(Session1)];
            set
            {
                _state[nameof(Session1)] = value;
                OnPropertyChanged();
            }
        }

        public string Session2
        {
            get => _state[nameof(Session2)];
            set
            {
                _state[nameof(Session2)] = value;
                OnPropertyChanged();
            }
        }

        public async Task SaveTempSessionAsync()
        {
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(SessionStateFile, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, _state);
            }
        }

        public async Task LoadTempSessionAsync()
        {
            Stream stream = await ApplicationData.Current.LocalCacheFolder.OpenStreamForReadAsync(SessionStateFile);
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(stream))
            {
                string json = await reader.ReadLineAsync();
                Dictionary<string, string> state = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                _state = state;

                foreach (var item in state)
                {
                    OnPropertyChanged(item.Key);
                }
            }
        }
    }
}
