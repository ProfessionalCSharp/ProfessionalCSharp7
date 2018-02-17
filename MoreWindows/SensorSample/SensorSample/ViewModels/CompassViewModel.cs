using SensorSample.Framework;
using System;
using Windows.ApplicationModel.Core;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace SensorSample.ViewModels
{
    public class CompassViewModel : BindableBase
    {
        public void OnGetCompass()
        {
            Compass sensor = Compass.GetDefault();
            if (sensor != null)
            {
                CompassReading reading = sensor.GetCurrentReading();
                CompassInfo = $"magnetic north: {reading.HeadingMagneticNorth} real north: {reading.HeadingTrueNorth} accuracy: {reading.HeadingAccuracy}";
            }
            else
            {
                CompassInfo = "Compass not found";
            }
        }

        private string _compassInfo;

        public string CompassInfo
        {
            get => _compassInfo;
            set => Set(ref _compassInfo, value);
        }

        public void OnGetCompassReport()
        {
            Compass sensor = Compass.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);

                sensor.ReadingChanged += async (s, e) =>
                {
                    CompassReading reading = e.Reading;
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        CompassInfoReport = $"magnetic north: {reading.HeadingMagneticNorth} real north: {reading.HeadingTrueNorth} accuracy: {reading.HeadingAccuracy} {reading.Timestamp:T}";
                    });
                  
                };
            }
        }

        private string _compassInfoReport;

        public string CompassInfoReport
        {
            get => _compassInfoReport;
            set => Set(ref _compassInfoReport, value);
        }
    }
}
