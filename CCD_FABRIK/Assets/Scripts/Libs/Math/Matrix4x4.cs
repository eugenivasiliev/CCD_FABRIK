namespace Math
{
    /// <summary>
    /// Custom <c>Matrix4x4</c> class. Allows for homogenous <see cref="Vector3"/> transformations.
    /// </summary>
    public class Matrix4x4 : Transformation
    {
        public double M11, M12, M13, M14;
        public double M21, M22, M23, M24;
        public double M31, M32, M33, M34;
        public double M41, M42, M43, M44;

        public Matrix4x4()
        {
            M11 = 0; M12 = 0; M13 = 0; M14 = 0;
            M21 = 0; M22 = 0; M23 = 0; M24 = 0;
            M31 = 0; M32 = 0; M33 = 0; M34 = 0;
            M41 = 0; M42 = 0; M43 = 0; M44 = 0;
        }

        public Matrix4x4(double M11, double M12, double M13, double M14, 
            double M21, double M22, double M23, double M24, 
            double M31, double M32, double M33, double M34, 
            double M41, double M42, double M43, double M44)
        {
            this.M11 = M11; this.M12 = M12; this.M13 = M13; this.M14 = M14;
            this.M21 = M21; this.M22 = M22; this.M23 = M23; this.M24 = M24;
            this.M31 = M31; this.M32 = M32; this.M33 = M33; this.M34 = M34;
            this.M41 = M41; this.M42 = M42; this.M43 = M43; this.M44 = M44;
        }

        public readonly static Matrix4x4 Identity = new Matrix4x4();

        public static Matrix4x4 CreateTranslation(Vector3 translation) =>
            new Matrix4x4(
                1, 0, 0, translation.x,
                0, 1, 0, translation.y,
                0, 0, 1, translation.z,
                0, 0, 0, 1
                );

        public static HomogenousVector3 operator* (Matrix4x4 m, HomogenousVector3 hv) =>
            new HomogenousVector3(
                m.M11 * hv.x + m.M12 * hv.y + m.M13 * hv.z + m.M14 * hv.w,
                m.M21 * hv.x + m.M22 * hv.y + m.M23 * hv.z + m.M24 * hv.w,
                m.M31 * hv.x + m.M32 * hv.y + m.M33 * hv.z + m.M34 * hv.w,
                m.M41 * hv.x + m.M42 * hv.y + m.M43 * hv.z + m.M44 * hv.w
                );

        /// <summary>
        /// Acts on <see cref="Vector3"/> as if it were a <see cref="HomogenousVector3"/>
        /// </summary>
        /// <param name="m"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 operator* (Matrix4x4 m, Vector3 v) => (Vector3)(m * (HomogenousVector3)v);
    }
}
