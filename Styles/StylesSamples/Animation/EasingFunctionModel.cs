using Windows.UI.Xaml.Media.Animation;

namespace Animation
{
    public class EasingFunctionModel
    {
        public EasingFunctionModel(EasingFunctionBase easingFunction) =>
            EasingFunction = easingFunction;

        public EasingFunctionBase EasingFunction { get; }

        public override string ToString() => EasingFunction.GetType().Name;
    }
}
