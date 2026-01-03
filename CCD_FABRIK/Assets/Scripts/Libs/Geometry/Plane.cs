using Math;

namespace Geometry
{
    public struct Plane
    {
        double d;
        Vector3 n;

        public Plane(double d, Vector3 n)
        {
            this.d = d;
            this.n = n;
        }

        public Plane(Vector3 n, Vector3 p)
        {
            this.n = n;
            this.d = -(n * p);
        }

        public bool isInPlane(Vector3 p) => n * p == -d;

        public Vector3 Projection(Vector3 v)
        {
            Vector3 x = Vector3.Cross(n, v);
            Vector3 y = Vector3.Cross(n, x);

            UnityEngine.Debug.Log("X: " + x.x + " " + x.y + " " + x.z);

            Vector3 vx = Vector3.Project(v, x);
            Vector3 vy = Vector3.Project(v, y);

            return vx + vy;
        }
    }
}