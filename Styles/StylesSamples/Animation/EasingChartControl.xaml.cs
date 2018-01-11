using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Animation
{
    public sealed partial class EasingChartControl : UserControl
    {
        private const double SamplingInterval = 0.01;
        public EasingChartControl()
        {
            this.InitializeComponent();
        }

        public void Draw(EasingFunctionBase easingFunction)
        {
            canvas1.Children.Clear();

            var pathSegments = new PathSegmentCollection();

            for (double i = 0; i < 1; i += SamplingInterval)
            {
                double x = i * canvas1.Width;
                double y = easingFunction.Ease(i) * canvas1.Height;

                var segment = new LineSegment();
                segment.Point = new Point(x, y);

                pathSegments.Add(segment);
            }

            var p = new Path();
            p.Stroke = new SolidColorBrush(Colors.Black);
            p.StrokeThickness = 3;
            var figures = new PathFigureCollection();
            figures.Add(new PathFigure() { Segments = pathSegments });
            p.Data = new PathGeometry() { Figures = figures };
            canvas1.Children.Add(p);
        }
    }
}
