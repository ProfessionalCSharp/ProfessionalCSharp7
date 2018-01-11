using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Media.Animation;

namespace Animation
{
    public class EasingFunctionsManager
    {
        private static IEnumerable<EasingFunctionBase> s_easingFunctions = new List<EasingFunctionBase>()
        {
            new BackEase(),
            new SineEase(),
            new BounceEase(),
            new CircleEase(),
            new CubicEase(),
            new ElasticEase(),
            new ExponentialEase(),
            new PowerEase(),
            new QuadraticEase(),
            new QuinticEase()
        };

        public IEnumerable<EasingFunctionModel> EasingFunctionModels =>
            s_easingFunctions.Select(f => new EasingFunctionModel(f));

    }
}
