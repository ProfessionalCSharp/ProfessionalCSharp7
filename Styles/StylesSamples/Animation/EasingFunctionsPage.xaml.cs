using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Animation
{
    public sealed partial class EasingFunctionsPage : Page
    {
        private EasingFunctionsManager _easingFunctions = new EasingFunctionsManager();
        private const int AnimationTimeSeconds = 6;

        public EasingFunctionsPage()
        {
            this.InitializeComponent();
            foreach (var easingFunctionModel in _easingFunctions.EasingFunctionModels)
            {
                comboEasingFunctions.Items.Add(easingFunctionModel);
            }
        }

        private EasingMode GetEasingMode()
        {

            if (easingModeIn.IsChecked == true) return EasingMode.EaseIn;
            else if (easingModeOut.IsChecked == true) return EasingMode.EaseOut;
            else return EasingMode.EaseInOut;
        }

        private void OnStartAnimation(object sender, RoutedEventArgs e)
        {
            var easingFunctionModel = comboEasingFunctions.SelectedItem as EasingFunctionModel;
            if (easingFunctionModel != null)
            {
                EasingFunctionBase easingFunction = easingFunctionModel.EasingFunction;
                easingFunction.EasingMode = GetEasingMode();
                StartAnimation(easingFunction);
            }
        }

        private void StartAnimation(EasingFunctionBase easingFunction)
        {
            // show the chart
            chartControl.Draw(easingFunction);

            // animation
            var storyboard = new Storyboard();
            var ellipseMove = new DoubleAnimation();
            ellipseMove.EasingFunction = easingFunction;
            ellipseMove.Duration = new Duration(TimeSpan.FromSeconds(AnimationTimeSeconds));
            ellipseMove.From = 0;
            ellipseMove.To = 460;
            Storyboard.SetTarget(ellipseMove, translate1);
            Storyboard.SetTargetProperty(ellipseMove, "X");
            ellipseMove.BeginTime = TimeSpan.FromSeconds(0.5); // start animation in 0.5 seconds
            ellipseMove.FillBehavior = FillBehavior.HoldEnd; // keep position after animation

            storyboard.Children.Add(ellipseMove);
            storyboard.Begin();
        }
    }
}
