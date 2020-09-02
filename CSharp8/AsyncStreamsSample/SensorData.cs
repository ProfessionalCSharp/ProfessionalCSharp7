using System;

namespace AsyncStreamsSample
{
    public struct SensorData : IEquatable<SensorData>
    {
        public SensorData(int value1, int value2) => (Value1, Value2) = (value1, value2);

        public int Value1 { get; }
        public int Value2 { get; }

        public override bool Equals(object? obj) => obj is SensorData data && Equals(data);
        public bool Equals(SensorData other) => Value1 == other.Value1 && Value2 == other.Value2;
        public override int GetHashCode() => HashCode.Combine(Value1, Value2);

        public static bool operator ==(SensorData left, SensorData right) => left.Equals(right);
        public static bool operator !=(SensorData left, SensorData right) => !(left == right);
    }
}
