using SensorSample.ViewModels;
using Windows.UI.Xaml.Controls;


namespace SensorSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public LightViewModel LightViewModel { get; } = new LightViewModel();

        public CompassViewModel CompassViewModel { get; } = new CompassViewModel();

        public AccelerometerViewModel AccelerometerViewModel { get; } = new AccelerometerViewModel();

        public InclinometerViewModel InclinometerViewModel { get; } = new InclinometerViewModel();

        public GyrometerViewModel GyrometerViewModel { get; } = new GyrometerViewModel();

        public OrientationViewModel OrientationViewModel { get; } = new OrientationViewModel();
    }
}
