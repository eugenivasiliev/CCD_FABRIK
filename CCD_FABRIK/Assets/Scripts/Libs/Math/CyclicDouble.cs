using System;

namespace Math
{
    /// <summary>
    /// <c>Cyclic Double</c> struct. Automatically handles a range to have wraparound.
    /// </summary>
    [Serializable]
    public struct CyclicDouble
    {
        public double Min { get; private set; }
        public double Max { get; private set; }

        public CyclicDouble(double value)
        {
            Min = 0;
            Max = 1;
            this.value = 0;
            this.Value = value;
        }

        public CyclicDouble(double value, double min, double max)
        {
            this.Min = min;
            this.Max = max;
            this.value = 0;
            this.Value = value;
        }

        private double value;
        private double Value
        {
            get => value;
            set => this.value = Min + (value % Max);
        }

        public static implicit operator double(CyclicDouble cD) => cD.Value;

        public static CyclicDouble operator+ (CyclicDouble cD, double d) => new CyclicDouble(cD.Value + d, cD.Min, cD.Max);
        public static CyclicDouble operator- (CyclicDouble cD, double d) => new CyclicDouble(cD.Value - d, cD.Min, cD.Max);
        public static CyclicDouble operator+ (CyclicDouble a, CyclicDouble b) => new CyclicDouble(a.Value + b.Value, a.Min, a.Max);
        public static CyclicDouble operator- (CyclicDouble a, CyclicDouble b) => new CyclicDouble(a.Value - b.Value, a.Min, a.Max);
        public static CyclicDouble operator- (CyclicDouble cD) => new CyclicDouble(-cD.Value, cD.Min, cD.Max);

        public static readonly CyclicDouble cyclicAngleDouble = new CyclicDouble(0d, 0d, 2d * Constants.PI);
    }
}