using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Shapes
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void OnChangeShape(object sender, RoutedEventArgs e)
        {
            SetMouth();
        }

        private readonly Point[,] _mouthPoints = new Point[2, 3]
        {
            {
                new Point(40, 74), new Point(57, 95), new Point(80, 74),
            },
            {
                new Point(40, 82), new Point(57, 65), new Point(80, 82),
            }
        };

        private bool _laugh = false;
        public void SetMouth()
        {
            int index = _laugh ? 0 : 1;

            var figure = new PathFigure() { StartPoint = _mouthPoints[index, 0] };
            figure.Segments = new PathSegmentCollection();
            var segment1 = new QuadraticBezierSegment
            {
                Point1 = _mouthPoints[index, 1],
                Point2 = _mouthPoints[index, 2]
            };

            figure.Segments.Add(segment1);
            var geometry = new PathGeometry();
            geometry.Figures = new PathFigureCollection();
            geometry.Figures.Add(figure);

            mouth.Data = geometry;
            _laugh = !_laugh;
        }
    }
}
