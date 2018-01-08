using SensorSample.Framework;
using System;
using Windows.ApplicationModel.Core;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace SensorSample.ViewModels
{
    public class InclinometerViewModel : BindableBase
    {
        public void OnGetInclinometer()
        {
            Inclinometer sensor = Inclinometer.GetDefault();
            if (sensor != null)
            {
                InclinometerReading reading = sensor.GetCurrentReading();
                InclinometerInfo = $"pitch degrees: {reading.PitchDegrees} roll degrees: {reading.RollDegrees} yaw accuracy: {reading.YawAccuracy} yaw degrees: {reading.YawDegrees}";
            }
            else
            {
                InclinometerInfo = "Inclinometer not found";
            }
        }

        private string _inclinometerInfo;

        public string InclinometerInfo
        {
            get => _inclinometerInfo;
            set => Set(ref _inclinometerInfo, value);
        }

        public void OnGetInclinometerReport()
        {
            Inclinometer sensor = Inclinometer.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);

                sensor.ReadingChanged += async (s, e) =>
                {
                    InclinometerReading reading = e.Reading;
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        InclinometerInfoReport = $"pitch degrees: {reading.PitchDegrees} roll degrees: {reading.RollDegrees} yaw accuracy: {reading.YawAccuracy} yaw degrees: {reading.YawDegrees} {reading.Timestamp:T}";
                    });

                };
            }
        }

        private string _inclinometerInfoReport;

        public string InclinometerInfoReport
        {
            get => _inclinometerInfoReport;
            set => Set(ref _inclinometerInfoReport, value);
        }
    }
}
