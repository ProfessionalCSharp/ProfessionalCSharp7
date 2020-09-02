using System;
using static UWPSwitchExpressionSample.LightState;

namespace UWPSwitchExpressionSample
{
    public class TrafficLightStateSwitcher
    {
        public (LightState Current, LightState Previous) GetNextLight(LightState currentLight, LightState previousLight)
            => (currentLight, previousLight) switch
            {
                (Red, _)        => (Yellow, currentLight),
                (Yellow, Red)   => (Green, currentLight),
                (Green, _)      => (Yellow, currentLight),
                (Yellow, Green) => (Red, currentLight),
                _               => throw new InvalidOperationException()
            };

        #region too simple version
        public LightState GetNextLight(LightState currentLight)
            => currentLight switch
            {
                Red => Yellow,
                Yellow => Green,
                Green => Red,
                _ => throw new InvalidOperationException()
            };
        #endregion
    }
}
