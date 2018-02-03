using MapSample.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Maps;

namespace MapSample.ViewModels
{
    public class MapsViewModel : BindableBase
    {
        private readonly CoreDispatcher _dispatcher;
        private readonly Geolocator _locator = new Geolocator();
        private readonly MapControl _mapControl;
        public MapsViewModel(MapControl mapControl)
        {
            _mapControl = mapControl;
            StopStreetViewCommand = new DelegateCommand(StopStreetView, () => IsStreetView);
            StartStreetViewCommand = new DelegateCommand(StartStreetViewAsync, () => !IsStreetView);

            if (!DesignMode.DesignModeEnabled)
            {
                _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            }

            _locator.StatusChanged += async (s, e) =>
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                                PositionStatus = e.Status
                );
            };

            // intialize defaults at startup
            CurrentPosition = new Geopoint(new BasicGeoposition { Latitude = 48.2, Longitude = 16.3 });
             // { Latitude = 47.604, Longitude = -122.329 });
            CurrentMapStyle = MapStyle.Road;
            DesiredPitch = 0;
            ZoomLevel = 12;
        }

        public IEnumerable<MapStyle> MapStyles => Enum.GetNames(typeof(MapStyle)).Select(s => (MapStyle)Enum.Parse(typeof(MapStyle), s));

        private MapStyle _currentMapStyle;
        public MapStyle CurrentMapStyle
        {
            get => _currentMapStyle;
            set => SetProperty(ref _currentMapStyle, value);
        }

        private double _zoomLevel;
        public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                if (value < 1) value = 1;
                if (value > 20) value = 20;
                SetProperty(ref _zoomLevel, value);
            }
        }

        private double _desiredPitch;
        public double DesiredPitch
        {
            get => _desiredPitch;
            set
            {
                if (value < 0) value = 0;
                if (value > 65) value = 65;
                SetProperty(ref _desiredPitch, value);
            }
        }

        private Geopoint _currentPosition;
        public Geopoint CurrentPosition
        {
            get => _currentPosition;
            set => SetProperty(ref _currentPosition, value);
        }

        private bool _isStreetView = false;
        private bool IsStreetView
        {
            get => _isStreetView;
            set
            {
                SetProperty(ref _isStreetView, value);
                StopStreetViewCommand.RaiseCanExecuteChanged();
            }
        }
        public async void StartStreetViewAsync()
        {
            if (_mapControl.IsStreetsideSupported)
            {
                var panorama = await StreetsidePanorama.FindNearbyAsync(CurrentPosition);
                if (panorama == null)
                {
                    var dlg = new MessageDialog("No streetside available here");
                    await dlg.ShowAsync();
                    return;
                }
                IsStreetView = true;
                _mapControl.CustomExperience = new StreetsideExperience(panorama);
            }
        }

        public DelegateCommand StopStreetViewCommand { get; }
        public DelegateCommand StartStreetViewCommand { get; }

        public void StopStreetView()
        {
            IsStreetView = false;
            _mapControl.CustomExperience = null;
        }

        public void OnMapTapped(MapControl sender, MapInputEventArgs args) => CurrentPosition = args.Location;

        public async void GetCurrentPositionAsync()
        {
            try
            {           
                Geoposition position = await _locator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));
                CurrentPosition = new Geopoint(new BasicGeoposition
                {
                    Longitude = position.Coordinate.Point.Position.Longitude,
                    Latitude = position.Coordinate.Point.Position.Latitude
                });
                
            }
            catch (UnauthorizedAccessException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        private PositionStatus _positionStatus;
        public PositionStatus PositionStatus
        {
            get => _positionStatus;
            set => SetProperty(ref _positionStatus, value);
        }
    }
}