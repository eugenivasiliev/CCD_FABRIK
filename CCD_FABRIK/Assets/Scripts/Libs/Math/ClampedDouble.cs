using System;

namespace Math
{
    /// <summary>
    /// <c>Clamped Double</c> struct. Allows for <see cref="IJoint{T}"/> to be automatically bounded.
    /// </summary>
    [Serializable]
    public struct ClampedDouble
    {
        public double Min { get; private set; }
        public double Max { get; private set; }

        public ClampedDouble(double value)
        {
            Min = 0;
            Max = 1;
            this.value = 0;
            this.Value = value;
        }

        public ClampedDouble(double value, double min, double max)
        {
            this.Min = min;
            this.Max = max;
            this.value = 0;
            this.Value = value;
        }

        private double value;
        public double Value
        {
            get => value;
            set => this.value = Functions.Clamp(value, Min, Max);
        }

        public static implicit operator double(ClampedDouble cD) => cD.Value;

        public static ClampedDouble operator+ (ClampedDouble cD, double d) => new ClampedDouble(cD.Value + d, cD.Min, cD.Max);
        public static ClampedDouble operator- (ClampedDouble cD, double d) => new ClampedDouble(cD.Value - d, cD.Min, cD.Max);
        public static ClampedDouble operator+ (ClampedDouble a, ClampedDouble b) => new ClampedDouble(a.Value + b.Value, a.Min, a.Max);
        public static ClampedDouble operator- (ClampedDouble a, ClampedDouble b) => new ClampedDouble(a.Value - b.Value, a.Min, a.Max);
        public static ClampedDouble operator- (ClampedDouble cD) => new ClampedDouble(-cD.Value, cD.Min, cD.Max);
    }
}