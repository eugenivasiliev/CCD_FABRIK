using Geometry;
using Math;
using Joints;

namespace InverseKinematics
{
    /// <summary>
    /// <a cref="UnityEngine.MonoBehaviour"/> class implementing <b>Cyclic Coordinate Descent</b>.
    /// </summary>
    public class CCD : UnityEngine.MonoBehaviour
    {
        private Transform root;
        [UnityEngine.SerializeField] private Transform target;
        private System.Collections.Generic.List<Transform> joints;
        private System.Collections.Generic.List<double> dists;
        private double totalDistance;
        private Transform end;

        private readonly double smartAngleTolerance = 0.1d;


        void Start()
        {
            root = GetComponent<Transform>();
            joints = Utils.GetJoints(root);
            end = joints[joints.Count - 1].children[0];
            dists = Utils.GetDists(joints, end, out totalDistance);
        }

        void Update()
        {
            if ((target.position - end.position).Magnitude < Controller.tolerance) return;

            int k = 0;
            while (true)
            {
                for (int i = joints.Count - 1; i >= 0; i--)
                {
                    (Vector3 endVector, Vector3 targetVector) rotationVectors = GetRotationVectors(joints[i]);
                    double ang = Functions.Arccos(Functions.Clamp(rotationVectors.targetVector.Dot(rotationVectors.endVector), -1.0d, 1.0d));
                    Vector3 axis = -Vector3.Cross(rotationVectors.targetVector, rotationVectors.endVector);
                    joints[i].rotation *= new Quaternion(axis, ang);

                    //Early exit
                    if ((target.position - end.position).Magnitude < Controller.tolerance || k / joints.Count > Controller.iterations) return;

                    if (ang > smartAngleTolerance) i = joints.Count;

                    k++;
                }
            }
        }

        private (Vector3 endVector, Vector3 targetVector) GetRotationVectors(Transform joint)
        {
            return ((end.position - joint.position).Normalized, (target.position - joint.position).Normalized);
        }
    }
}