namespace SwitchStateSample
{
    public struct LightStatus
    {
        public LightStatus(LightState current, LightState previous, int seconds, int blinkCount)
            => (Current, Previous, Milliseconds, FlashCount) = (current, previous, seconds, blinkCount);

        public LightStatus(LightState current, LightState previous, int seconds) : this(current, previous, seconds, 0) { }
        public LightStatus(LightState current, LightState previous) : this(current, previous, 3) { }

        public LightState Current { get; }
        public LightState Previous { get; }
        public int FlashCount { get; }
        public int Milliseconds { get; }
    }
}
