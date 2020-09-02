using static SwitchStateSample.LightState;

namespace SwitchStateSample
{
    public class TrafficLightSwitcher
    {
        public LightState GetNextLight1(LightState currentLight) =>
            currentLight switch
            {
                Red => Yellow,
                Yellow => Green,
                Green => Red,
                _ => Undefined
            };

        //public (LightState Current, LightState Previous) GetNextLight2(LightState currentLight, LightState previousLight)
        //{
        //    return (currentLight, previousLight) switch
        //    {
        //        (LightState.FlashingYellow, LightState.Undefined) => (LightState.Red, currentLight),
        //        (LightState.FlashingYellow, LightState.Red) => (LightState.Red, currentLight),
        //        (LightState.FlashingYellow, LightState.Green) => (LightState.Red, currentLight),
        //        (LightState.FlashingYellow, LightState.Yellow) => (LightState.Red, currentLight),
        //        (LightState.Red, LightState.FlashingYellow) => (LightState.Yellow, currentLight),
        //        (LightState.Red, LightState.Yellow) => (LightState.Yellow, currentLight),
        //        (LightState.Yellow, LightState.Red) => (LightState.Green, currentLight),
        //        (LightState.Green, LightState.Yellow) => (LightState.FlashingGreen, currentLight),
        //        (LightState.FlashingGreen, LightState.Green) => (LightState.Yellow, currentLight),
        //        (LightState.Yellow, LightState.FlashingGreen) => (LightState.Red, currentLight),
        //        _ => (LightState.FlashingYellow, currentLight)
        //    };
        //}

        public (LightState Current, LightState Previous) GetNextLight2(LightState currentLight, LightState previousLight) =>
            (currentLight, previousLight) switch
            {
                (FlashingYellow, _)     => (Red, currentLight),
                (Red, _)                => (Yellow, currentLight),
                (Yellow, Red)           => (Green, currentLight),
                (Green, _)              => (FlashingGreen, currentLight),
                (FlashingGreen, _)      => (Yellow, currentLight),
                (Yellow, FlashingGreen) => (Red, currentLight),
                _                       => (FlashingYellow, currentLight)
            };

        public (LightState Current, LightState Previous, int count) GetNextLight3(LightState currentLight, LightState previousLight, int currentCount = 0) =>
            (currentLight, previousLight, currentCount) switch
            {
                (FlashingYellow, _, _)     => (Red, currentLight, 0),
                (Red, _, _)                => (Yellow, currentLight, 0),
                (Yellow, Red, _)           => (Green, currentLight, 0),
                (Green, _, _)              => (FlashingGreen, currentLight, 0),
                (FlashingGreen, _, 2)      => (Yellow, currentLight, 0),
                (FlashingGreen, _, _)      => (FlashingGreen, currentLight, ++currentCount),
                (Yellow, FlashingGreen, _) => (Red, currentLight, 0),
                _                          => (FlashingYellow, currentLight, 0)
            };

        public LightStatus GetNextLight4(LightStatus lightStatus) =>
            lightStatus switch
            {
                { Current: FlashingYellow } => new LightStatus(Red, FlashingYellow, 5000),
                { Current: Red } => new LightStatus(Yellow, lightStatus.Current, 3000),
                { Current: Yellow, Previous: Red } => new LightStatus(Green, lightStatus.Current, 5000),
                { Current: Green } => new LightStatus(FlashingGreen, lightStatus.Current, 1000),
                { Current: FlashingGreen, FlashCount: 2 } => new LightStatus(Yellow, lightStatus.Current, 2000),
                { Current: FlashingGreen } => new LightStatus(FlashingGreen, lightStatus.Current, 1000, lightStatus.FlashCount + 1),
                { Current: Yellow, Previous: FlashingGreen } => new LightStatus(Red, lightStatus.Current, 5000),
                _ => new LightStatus(FlashingYellow, lightStatus.Current, 1000)
            };
    }
}
