namespace Math
{
    /// <summary>
    /// Custom <c>Vector2</c> struct.
    /// </summary>
    [System.Serializable]
    public struct Vector2
    {
        /// <summary>
        /// <b>X</b> component of the vector.
        /// </summary>
        public double x;

        /// <summary>
        /// <b>Y</b> component of the vector.
        /// </summary>
        public double y;

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator+ (Vector2 l, Vector2 r) => new Vector2(l.x + r.x, l.y + r.y);
        public static Vector2 operator- (Vector2 v) => new Vector2(-v.x, -v.y);
        public static Vector2 operator- (Vector2 l, Vector2 r) => l + (-r);
        public static Vector2 operator* (double d, Vector2 v) => new Vector2(d * v.x, d * v.y);
        public static Vector2 operator/ (Vector2 v, double d) => (1 / d) * v;
        public static double operator* (Vector2 l, Vector2 r) => l.x * r.x + l.y * r.y;

        public static implicit operator UnityEngine.Vector2(Vector2 v) => new UnityEngine.Vector2((float)v.x, (float)v.y);
        public static implicit operator Vector2(UnityEngine.Vector2 v) => new Vector2(v.x, v.y);

        public double SqrMagnitude => this * this;
        public double Magnitude => System.Math.Sqrt(this.SqrMagnitude);

        /// <summary>Vector with magnitude 1 in the same direction as the base vector.</summary>
        public Vector2 Normalized => this / this.Magnitude;

        /// <summary>Sets vector to be vector with magnitude 1 in the same direction as the base vector.</summary>
        public void Normalize() => this = this.Normalized;

        /// <returns>Scalar product between two vectors.</returns>
        public static double Dot(Vector2 v1, Vector2 v2) => v1 * v2;

        /// <returns>Scalar product with another vector.</returns>
        public double Dot(Vector2 v) => this * v;

        /// <returns>Angle between two vectors.</returns>
        public static double Angle(Vector2 v1, Vector2 v2) => (v1 * v2) / (v1.Magnitude * v2.Magnitude);

        /// <returns>Angle with another vector.</returns>
        public double Angle(Vector2 v) => Vector2.Angle(this, v);

        /// <param name="v1">Vector projected.</param>
        /// <param name="v2">Vector projected onto.</param>
        /// <returns>Projection from one vector onto another</returns>
        public static Vector2 Project(Vector2 v1, Vector2 v2) => ((v1 * v2) / v2.SqrMagnitude) * v2;

        /// <param name="v">Vector projected onto.</param>
        /// <returns>Projection from this vector onto another</returns>
        public Vector2 Project(Vector2 v) => ((this * v) / v.SqrMagnitude) * v;

        #region CONSTS
        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 up = new Vector2(0, 1);
        public static readonly Vector2 down = new Vector2(0, -1);
        public static readonly Vector2 right = new Vector2(1, 0);
        public static readonly Vector2 left = new Vector2(-1, 0);
        #endregion
    }

    /// <summary>
    /// Custom <c>Vector3</c> struct.
    /// </summary>
    [System.Serializable]
    public struct Vector3 
    {

        /// <summary>
        /// <b>X</b> component of the vector.
        /// </summary>
        public double x;

        /// <summary>
        /// <b>Y</b> component of the vector.
        /// </summary>
        public double y;

        /// <summary>
        /// <b>Z</b> component of the vector.
        /// </summary>
        public double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator+ (Vector3 l, Vector3 r) => new Vector3(l.x + r.x, l.y + r.y, l.z + r.z);
        public static Vector3 operator- (Vector3 v) => new Vector3(-v.x, -v.y, -v.z);
        public static Vector3 operator- (Vector3 l, Vector3 r) => l + (-r);
        public static Vector3 operator* (double d, Vector3 v) => new Vector3(d * v.x, d * v.y, d * v.z);
        public static Vector3 operator/ (Vector3 v, double d) => (1 / d) * v;

        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns>Scalar product between two vectors.</returns>
        public static double operator* (Vector3 l, Vector3 r) => l.x * r.x + l.y * r.y + l.z * r.z;

        public static implicit operator UnityEngine.Vector3(Vector3 v) => new UnityEngine.Vector3((float)v.x, (float)v.y, (float)v.z);
        public static implicit operator Vector3(UnityEngine.Vector3 v) => new Vector3(v.x, v.y, v.z);

        public double SqrMagnitude => this * this;
        public double Magnitude => System.Math.Sqrt(this.SqrMagnitude);

        /// <summary>Vector with magnitude 1 in the same direction as the base vector.</summary>
        public Vector3 Normalized => (this.Magnitude == 0) ? Vector3.zero : this / this.Magnitude;

        /// <summary>Sets vector to be vector with magnitude 1 in the same direction as the base vector.</summary>
        public void Normalize() => this = this.Normalized;

        /// <returns>Scalar product between two vectors.</returns>
        public static double Dot(Vector3 v1, Vector3 v2) => v1 * v2;

        /// <returns>Scalar product with another vector.</returns>
        public readonly double Dot(Vector3 v) => this * v;

        /// <remarks>Follows the <b>right hand rule</b>.</remarks>
        /// <returns>Vector product between two vectors.</returns>
        public static Vector3 Cross(Vector3 v1, Vector3 v2) => 
            new Vector3(
                v1.y * v2.z - v1.z * v2.y, 
                v1.z * v2.x - v1.x * v2.z, 
                v1.x * v2.y - v1.y * v2.x
                );

        /// <remarks>Follows the <b>right hand rule</b>.</remarks>
        /// <returns>Vector product with another vector.</returns>
        public readonly Vector3 Cross(Vector3 v) => Vector3.Cross(this, v);

        /// <returns>Angle between two vectors.</returns>
        public static double Angle(Vector3 v1, Vector3 v2) => (v1 * v2) /(v1.Magnitude *  v2.Magnitude);

        /// <returns>Angle with another vector.</returns>
        public readonly double Angle(Vector3 v) => Vector3.Angle(this, v);

        /// <param name="v1">Vector projected.</param>
        /// <param name="v2">Vector projected onto.</param>
        /// <returns>Projection from one vector onto another</returns>
        public static Vector3 Project(Vector3 v1, Vector3 v2) => ((v1 * v2) / v2.SqrMagnitude) * v2;

        /// <param name="v">Vector projected onto.</param>
        /// <returns>Projection from this vector onto another</returns>
        public readonly Vector3 Project(Vector3 v) => ((this * v) / v.SqrMagnitude) * v;

        #region CONSTS
        public static readonly Vector3 zero = new Vector3(0, 0, 0);
        public static readonly Vector3 up = new Vector3(0, 1, 0);
        public static readonly Vector3 down = new Vector3(0, -1, 0);
        public static readonly Vector3 right = new Vector3(1, 0, 0);
        public static readonly Vector3 left = new Vector3(-1, 0, 0);
        public static readonly Vector3 forward = new Vector3(0, 0, 1);
        public static readonly Vector3 back = new Vector3(0, 0, -1);
        #endregion
    }

    [System.Serializable]
    public struct Vector
    {
        public System.Collections.Generic.List<double> values;

        public Vector(int n)
        {
            this.values = new System.Collections.Generic.List<double>();
            for (int i = 0; i < n; i++) this.values.Add(0d);
        }

        public double this[int i]
        {
            get { return values[i]; }
            set { values[i] = value; }
        }

        public static Vector operator +(Vector l, Vector r)
        {
            if (l.values.Count != r.values.Count) throw new System.ArgumentException();

            Vector result = new Vector(l.values.Count);

            for (int i = 0; i < l.values.Count; i++)
                result[i] = l.values[i] + r.values[i];

            return result;
        }

        public static Vector operator -(Vector v)
        {
            Vector result = v;
            for (int i = 0; i < v.values.Count; i++) result[i] *= -1;
            return result;
        }

        public static Vector operator- (Vector l, Vector r)
        {
            return l + -r;
        }

        public static Vector operator*(double d, Vector v)
        {
            Vector result = v;
            for (int i = 0; i < v.values.Count; i++) result[i] *= d;
            return result;
        }

        public double SqrMagnitude
        {
            get
            {
                double result = 0;
                foreach(double v in values) result += v * v;
                return result;
            }
        }
    }
}