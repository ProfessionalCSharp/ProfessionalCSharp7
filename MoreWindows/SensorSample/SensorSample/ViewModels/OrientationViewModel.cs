using SensorSample.Framework;
using System;
using Windows.ApplicationModel.Core;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace SensorSample.ViewModels
{
    public static class OrientationSensorExtensions
    {
        public static string Output(this SensorQuaternion q) =>
            $"x {q.X} y {q.Y} z {q.Z} w {q.W}";

        public static string Ouput(this SensorRotationMatrix m) =>
            $"m11 {m.M11} m12 {m.M12} m13 {m.M13} m21 {m.M21} m22 {m.M22} m23 {m.M23} m31 {m.M31} m32 {m.M32} m33 {m.M33}";

    }

    public class OrientationViewModel : BindableBase
    {
        public void OnGetOrientation()
        {
            OrientationSensor sensor = OrientationSensor.GetDefault();
            if (sensor != null)
            {
                OrientationSensorReading reading = sensor.GetCurrentReading();
                OrientationInfo = $"Quaternion: {reading.Quaternion.Output()} Rotation: {reading.RotationMatrix.Ouput()} Yaw accuracy: {reading.YawAccuracy}";
            }
            else
            {
                OrientationInfo = "Compass not found";
            }
        }

        private string _orientationInfo;

        public string OrientationInfo
        {
            get => _orientationInfo;
            set => Set(ref _orientationInfo, value);
        }

        public void OnGetOrientationReport()
        {
            OrientationSensor sensor = OrientationSensor.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);

                sensor.ReadingChanged += async (s, e) =>
                {
                    OrientationSensorReading reading = e.Reading;
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        OrientationInfoReport = $"Quaternion: {reading.Quaternion.Output()} Rotation: {reading.RotationMatrix.Ouput()} Yaw accuracy: {reading.YawAccuracy} { reading.Timestamp:T}";
                    });

                };
            }
        }

        private string _orientationInfoReport;

        public string OrientationInfoReport
        {
            get => _orientationInfoReport;
            set => Set(ref _orientationInfoReport, value);
        }
    }
}
