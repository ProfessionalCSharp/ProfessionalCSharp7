using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Animation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
#if WPF
            NameScope.SetNameScope(translate1, new NameScope());
#endif

            var storyboard = new Storyboard();
            var ellipseMove = new DoubleAnimation();
            ellipseMove.EasingFunction = easingFunction;
            ellipseMove.Duration = new Duration(TimeSpan.FromSeconds(AnimationTimeSeconds));
            ellipseMove.From = 0;
            ellipseMove.To = 460;
#if WPF
            Storyboard.SetTargetName(ellipseMove, nameof(translate1));
            Storyboard.SetTargetProperty(ellipseMove, new PropertyPath(TranslateTransform.XProperty));
#else
            Storyboard.SetTarget(ellipseMove, translate1);
            Storyboard.SetTargetProperty(ellipseMove, "X");
#endif

            ellipseMove.BeginTime = TimeSpan.FromSeconds(0.5); // start animation in 0.5 seconds
            ellipseMove.FillBehavior = FillBehavior.HoldEnd; // keep position after animation

            storyboard.Children.Add(ellipseMove);
#if WPF
            storyboard.Begin(this);
#else
            storyboard.Begin();
#endif
        }
    }
}
