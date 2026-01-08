namespace Math
{
    /// <summary>
    /// General container class for all math functions. Uses some logic from <see cref="System.Math"/>
    /// </summary>
    public static class Functions
    {

        public static double Abs(double x) => (x < 0) ? -x : x;
        public static double Sqrt(double x) => System.Math.Sqrt(x);

        public static double Sin(double a) => System.Math.Sin(a);
        public static double Cos(double a) => System.Math.Cos(a);

        public static double Arcsin(double s) => System.Math.Asin(s);
        public static double Arccos(double c) => System.Math.Acos(c);

        public static double Min(double a, double b) => (a < b) ? a : b;
        public static int Min(int a, int b) => (a < b) ? a : b;
        public static uint Min(uint a, uint b) => (a < b) ? a : b;
        public static double Max(double a, double b) => (a > b) ? a : b;
        public static int Max(int a, int b) => (a > b) ? a : b;
        public static uint Max(uint a, uint b) => (a > b) ? a : b;
        public static double Clamp(double val, double min, double max) => Min(Max(val, min), max);

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Vector2 lerp(Vector2 a, Vector2 b, double t) => t * a + (1 - t) * b;

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Vector3 lerp(Vector3 a, Vector3 b, double t) => t * a + (1 - t) * b;

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Quaternion lerp(Quaternion a, Quaternion b, double t) => t * a + (1 - t) * b;

        /// <summary>
        /// Spherical interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Vector2 slerp(Vector2 a, Vector2 b, double t) 
        {
            double omega = Vector2.Angle(a, b);
            return (Sin(omega * t) / Sin(omega)) * a + (Cos(omega * (1-t)) / Cos(omega)) * b;
        }

        /// <summary>
        /// Spherical interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Vector3 slerp(Vector3 a, Vector3 b, double t) 
        {
            double omega = Vector3.Angle(a, b);
            return (Sin(omega * t) / Sin(omega)) * a + (Cos(omega * (1 - t)) / Cos(omega)) * b;
        }

        /// <summary>
        /// Spherical interpolation.
        /// </summary>
        /// <param name="a">Start value.</param>
        /// <param name="b">End value.</param>
        /// <param name="t">Interpolation value.</param>
        public static Quaternion slerp(Quaternion a, Quaternion b, double t)
        {
            double omega = Quaternion.Angle(a, b);
            return (Sin(omega * t) / Sin(omega)) * a + (Cos(omega * (1 - t)) / Cos(omega)) * b;
        }
    }
}
