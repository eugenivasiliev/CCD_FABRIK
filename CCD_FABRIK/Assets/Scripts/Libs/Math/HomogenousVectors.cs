namespace Math
{
    /// <summary>
    /// Custom homogenous <c>Vector2</c> struct.
    /// </summary>
    public struct HomogenousVector2
    {
        /// <summary><b>X</b> component of the vector.</summary>
        public double x;

        /// <summary><b>Y</b> component of the vector.</summary>
        public double y;

        /// <summary><b>Z</b> component of the vector.</summary>
        public double z;

        public HomogenousVector2(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z})";

        public static bool operator== (HomogenousVector2 lhs, HomogenousVector2 rhs) => 
            (lhs.x / lhs.z) == (rhs.x / rhs.z) && (lhs.y / lhs.z) == (rhs.y / rhs.z);
        public static bool operator !=(HomogenousVector2 lhs, HomogenousVector2 rhs) =>
            (lhs.x / lhs.z) != (rhs.x / rhs.z) || (lhs.y / lhs.z) != (rhs.y / rhs.z);

        public static implicit operator Vector2 (HomogenousVector2 v) => new Vector2(v.x / v.z, v.y / v.z);
        public static implicit operator HomogenousVector2(Vector2 v) => new HomogenousVector2(v.x, v.y, 1);
    }

    /// <summary>
    /// Custom homogenous <c>Vector3</c> struct. Allows for direct <see cref="Matrix4x4"/> transformations.
    /// </summary>
    public struct HomogenousVector3
    {
        /// <summary><b>X</b> component of the vector.</summary>
        public double x;

        /// <summary><b>Y</b> component of the vector.</summary>
        public double y;

        /// <summary><b>Z</b> component of the vector.</summary>
        public double z;

        /// <summary><b>W</b> component of the vector.</summary>
        public double w;

        public HomogenousVector3(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z}, {w})";

        public static bool operator ==(HomogenousVector3 lhs, HomogenousVector3 rhs) =>
            (lhs.x / lhs.w) == (rhs.x / rhs.w) && (lhs.y / lhs.w) == (rhs.y / rhs.w) && (lhs.z / lhs.w) == (rhs.z / rhs.w);
        public static bool operator !=(HomogenousVector3 lhs, HomogenousVector3 rhs) =>
            (lhs.x / lhs.w) != (rhs.x / rhs.w) || (lhs.y / lhs.w) != (rhs.y / rhs.w) || (lhs.z / lhs.w) != (rhs.z / rhs.w);

        public static implicit operator Vector3(HomogenousVector3 v) => new Vector3(v.x / v.w, v.y / v.w, v.z / v.w);
        public static implicit operator HomogenousVector3(Vector3 v) => new HomogenousVector3(v.x, v.y, v.z, 1);
    }
}