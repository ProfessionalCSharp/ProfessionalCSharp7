using SensorSample.Framework;
using System;
using Windows.ApplicationModel.Core;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace SensorSample.ViewModels
{
    public class AccelerometerViewModel : BindableBase
    {
        public void OnGetAccelerometer()
        {
            Accelerometer sensor = Accelerometer.GetDefault();
            if (sensor != null)
            {
                AccelerometerReading reading = sensor.GetCurrentReading();
                AccelerometerInfo = $"X: {reading.AccelerationX} Y: {reading.AccelerationY} Z: {reading.AccelerationZ}";
            }
            else
            {
                AccelerometerInfo = "Compass not found";
            }
        }

        private string _accelerometerInfo;

        public string AccelerometerInfo
        {
            get { return _accelerometerInfo; }
            set { Set(ref _accelerometerInfo, value); }
        }

        public void OnGetAccelerometerReport()
        {
            Accelerometer sensor = Accelerometer.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);

                sensor.ReadingChanged += async (s, e) =>
                {
                    AccelerometerReading reading = e.Reading;
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        AccelerometerInfoReport = $"X: {reading.AccelerationX} Y: {reading.AccelerationY} Z: {reading.AccelerationZ} { reading.Timestamp:T}";
                    });

                };
            }
        }

        private string _acceleroMeterInfoReport;

        public string AccelerometerInfoReport
        {
            get => _acceleroMeterInfoReport;
            set => Set(ref _acceleroMeterInfoReport, value);
        }
    }
}
