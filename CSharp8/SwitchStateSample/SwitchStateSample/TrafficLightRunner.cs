using System;
using System.Threading.Tasks;

namespace SwitchStateSample
{
    public class TrafficLightRunner
    {
        private readonly TrafficLightSwitcher _switcher = new TrafficLightSwitcher();

        public async Task SimpleLigthAsync()
        {
            LightState current = LightState.Red;
            while (true)
            {
                current = _switcher.GetNextLight1(current);
                Console.WriteLine($"new light: {current}");
                await Task.Delay(2000);
            }
        }

        public async Task UseTuplesAsync()
        {
            LightState current = LightState.FlashingYellow;
            LightState previous = LightState.Undefined;
            while (true)
            {
                (current, previous) = _switcher.GetNextLight2(current, previous);
                Console.WriteLine($"new light: {current}, previous: {previous}");
                await Task.Delay(2000);
            }
        }

        public async Task UseTuplesWithCountAsync()
        {
            LightState current = LightState.FlashingYellow;
            LightState previous = LightState.Undefined;
            int count = 0;
            while (true)
            {
                (current, previous, count) = _switcher.GetNextLight3(current, previous, count);
                Console.WriteLine($"new light: {current}, previous: {previous}, count: {count}");
                await Task.Delay(2000);
            }
        }

        public async Task UseCustomTypeAsync()
        {
            var lightStatus = new LightStatus();
            while (true)
            {
                lightStatus = _switcher.GetNextLight4(lightStatus);
                Console.WriteLine($"new light: {lightStatus.Current}, previous: {lightStatus.Previous}, count: {lightStatus.FlashCount}, time: {lightStatus.Milliseconds}");
                await Task.Delay(lightStatus.Milliseconds);
            }
        }
    }
}
