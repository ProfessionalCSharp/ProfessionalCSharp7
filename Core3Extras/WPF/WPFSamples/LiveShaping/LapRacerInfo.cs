namespace LiveShaping
{
    public enum PositionChange
    {
        None,
        Up,
        Down,
        Out
    }

    public class LapRacerInfo : BindableObject
    {
        public Racer Racer { get; set; }
        private int _lap;
        public int Lap
        {
            get { return _lap; }
            set { SetProperty(ref _lap, value); }
        }
        private int _position;
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }
        private PositionChange _positionChange;
        public PositionChange PositionChange
        {
            get { return _positionChange; }
            set { SetProperty(ref _positionChange, value); }
        }
    }
}
