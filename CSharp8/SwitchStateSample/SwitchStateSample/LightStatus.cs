namespace SwitchStateSample
{
    public readonly struct LightStatus
    {
        public LightStatus(LightState current, LightState previous, int milliSeconds, int blinkCount) =>
            (Current, Previous, Milliseconds, FlashCount) = (current, previous, milliSeconds, blinkCount);

        public LightStatus(LightState current, LightState previous, int milliSeconds) : this(current, previous, milliSeconds, 0) { }
        public LightStatus(LightState current, LightState previous) : this(current, previous, 3) { }

        public LightState Current { get; }
        public LightState Previous { get; }
        public int FlashCount { get; }
        public int Milliseconds { get; }
    }
}
