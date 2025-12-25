namespace Math
{
    /// <summary>Custom <c>Quaternion</c> class. Used to represent rotations.</summary>
    /// <remarks>Inherits from <c>Transformation</c></remarks>
    public class Quaternion : Transformation, IDerivable<Quaternion>
    {
        /// <summary>
        /// Real component of the <c>Quaternion</c>
        /// </summary>
        public double a;

        /// <summary>
        /// <b>x</b>-axis component of the <c>Quaternion</c>
        /// </summary>
        public double b;

        /// <summary>
        /// <b>y</b>-axis component of the <c>Quaternion</c>
        /// </summary>
        public double c;

        /// <summary>
        /// <b>z</b>-axis component of the <c>Quaternion</c>
        /// </summary>
        public double d;

        public Quaternion(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public Quaternion(double roll, double pitch, double yaw, bool isInDegrees = false)
        {
            if (isInDegrees)
            {
                roll = roll * Constants.Deg2Rad;
                pitch = pitch * Constants.Deg2Rad;
                yaw = yaw * Constants.Deg2Rad;
            }

            double cr = System.Math.Cos(roll * 0.5);
            double sr = System.Math.Sin(roll * 0.5);
            double cp = System.Math.Cos(pitch * 0.5);
            double sp = System.Math.Sin(pitch * 0.5);
            double cy = System.Math.Cos(yaw * 0.5);
            double sy = System.Math.Sin(yaw * 0.5);

            this.a = cr * cp * cy + sr * sp * sy;
            this.b = sr * cp * cy - cr * sp * sy;
            this.c = cr * sp * cy + sr * cp * sy;
            this.d = cr * cp * sy - sr * sp * cy;
        }

        public Quaternion(Vector3 axis, double angle, bool isInDegrees = false)
        {
            if (angle == 0)
            {
                this.a = 1; this.b = 0; this.c = 0; this.d = 0;
                return;
            }
            if (isInDegrees) angle = angle * Constants.Deg2Rad;
            double angle_2 = angle * 0.5f;
            Vector3 n_axis = axis.Normalized;
            double s = Functions.Sin(angle_2);

            this.a = Functions.Cos(angle_2);
            this.b = s * n_axis.x;
            this.c = s * n_axis.y;
            this.d = s * n_axis.z;
        }

        static public Quaternion operator +(Quaternion q1, Quaternion q2) => new Quaternion(q1.a + q2.a, q1.b + q2.b, q1.c + q2.c, q1.d + q2.d);

        static public Quaternion operator -(Quaternion q) => new Quaternion(-q.a, -q.b, -q.c, -q.d);

        static public Quaternion operator -(Quaternion q1, Quaternion q2) => q1 + -q2;

        static public Quaternion operator *(Quaternion q1, Quaternion q2) =>
            new Quaternion(
                q1.a * q2.a - q1.b * q2.b - q1.c * q2.c - q1.d * q2.d, //real
                q1.a * q2.b + q1.b * q2.a + q1.c * q2.d - q1.d * q2.c, //i
                q1.a * q2.c + q1.c * q2.a + q1.d * q2.b - q1.b * q2.d, //j
                q1.a * q2.d + q1.d * q2.a + q1.b * q2.c - q1.c * q2.b  //k
            );

        public static Quaternion operator *(double d, Quaternion q) => new Quaternion(d * q.a, d * q.b, d * q.c, d * q.d);

        /// <remarks>
        /// Using Euler-Rodrigues formula for quaternion rotation.
        /// </remarks>
        public static Vector3 operator *(Quaternion q, Vector3 v) =>
            v + 2d * q.Real * q.Imaginary.Cross(v) 
                + 2d * q.Imaginary.Cross(q.Imaginary.Cross(v));
        public static Quaternion operator /(Quaternion q, double d) => (1.0f / d) * q;

        public static implicit operator UnityEngine.Quaternion(Quaternion q) =>
            new UnityEngine.Quaternion((float)q.b, (float)q.c, (float)q.d, (float)q.a);

        public static implicit operator Quaternion(UnityEngine.Quaternion q) =>
            new Quaternion(q.w, q.x, q.y, q.z);

        /// <summary>
        /// Real component of the <c>Quaternion</c>.
        /// </summary>
        public double Real => this.a;

        /// <summary>
        /// Imaginary vector component of the <c>Quaternion</c>.
        /// </summary>
        public Vector3 Imaginary => new Vector3(this.b, this.c, this.d);

        public Quaternion conjugated { get { return new Quaternion(this.a, -this.b, -this.c, -this.d); } }
        public void Conjugate() { b = -b; c = -c; d = -d; }

        public double sqrMagnitude { get { return this.a * this.a + this.b * this.b + this.c * this.c + this.d * this.d; } }
        public double SqrMagnitude() => this.sqrMagnitude;

        public double magnitude { get { return System.Math.Sqrt(this.sqrMagnitude); } }
        public double Magnitude() => this.magnitude;

        public Quaternion normalized { get { return this / this.magnitude; } }
        public void Normalize() { a /= this.magnitude; b /= this.magnitude; c /= this.magnitude; d /= this.magnitude; }

        public Quaternion inverse { get { return this.conjugated / this.sqrMagnitude; } }
        public void Invert() { a /= sqrMagnitude; b /= -sqrMagnitude; c /= -sqrMagnitude; d /= -sqrMagnitude; }

        public static double Dot(Quaternion q1, Quaternion q2) => q1.a * q2.a + q1.b * q2.b + q1.c * q2.c + q1.d * q2.d;
        public double Dot(Quaternion q) => this.a * q.a + this.b * q.b + this.c * q.c + this.d * q.d;

        public Vector3 eulerAngles
        {
            get
            {
                Vector3 angles;

                // roll
                double sinr_cosp = 2 * (this.a * this.b + this.c * this.d);
                double cosr_cosp = 1 - 2 * (this.b * this.b + this.c * this.c);
                angles.x = (float)System.Math.Atan2(sinr_cosp, cosr_cosp);

                // pitch
                double sinp = System.Math.Sqrt(1 + 2 * (this.a * this.c - this.b * this.d));
                double cosp = System.Math.Sqrt(1 - 2 * (this.a * this.c - this.b * this.d));
                angles.y = 2 * (float)System.Math.Atan2(sinp, cosp) - System.MathF.PI / 2;

                // yaw
                double siny_cosp = 2 * (this.a * this.d + this.b * this.c);
                double cosy_cosp = 1 - 2 * (this.c * this.c + this.d * this.d);
                angles.z = (float)System.Math.Atan2(siny_cosp, cosy_cosp);

                return angles;
            }
        }

        public static double Angle(Quaternion a, Quaternion b)
        {
            double dot = Quaternion.Dot(a, b);
            return System.Math.Atan2(dot, a.magnitude * b.magnitude);
        }

        public double angle => 2 * Functions.Arccos(this.a);

        public Quaternion Derivative()
        {
            double angle = 2 * Functions.Arccos(this.a);
            double c = this.a;
            double s = Functions.Cos(angle);
            double frac = c / s;

            Quaternion q = new Quaternion(
                -s, this.b * frac, this.c * frac, this.d * frac
                );

            return q;
        }

        public static Quaternion identity = new Quaternion(1, 0, 0, 0);
    }
}
