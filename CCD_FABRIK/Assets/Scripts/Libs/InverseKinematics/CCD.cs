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
        public Transform target;
        private System.Collections.Generic.List<Transform> joints;
        private System.Collections.Generic.List<double> dists;
        private double totalDistance;
        private Transform end;

        private readonly double rotationDamping = 0.3d;

        public double targetDistance
        {
            get => (target.position - end.position).Magnitude;
        }

        public int lastFrameIterations { get; private set; }

        public void Init()
        {
            root = GetComponent<Transform>();
            joints = Utils.GetJoints(root);
            end = joints[joints.Count - 1].GetChildByTag("End");
            dists = Utils.GetDists(joints, end, out totalDistance);
        }


        private void Update()
        {
            if (totalDistance < (target.position - root.position).Magnitude)
            {
                for(int i = 0; i < joints.Count; ++i)
                {
                    joints[i].LookAt(target.position);
                }
                lastFrameIterations = 0;
                return;
            }

            for (int i = 0; i < Controller.iterations; i++)
            {
                if ((target.position - end.position).Magnitude < Controller.tolerance)
                {
                    lastFrameIterations = i;
                    return;
                }

                RotateJoints();
            }

            lastFrameIterations = (int)Controller.iterations - 1;
        }

        /// <summary>
        /// Rotates all joints towards target.
        /// </summary>
        /// <remarks>Uses <b>damping</b> for better results at the cost of iterations.</remarks>
        private void RotateJoints()
        {
            for (int j = 0; j < joints.Count; j++)
            {
                Vector3 jointPos = joints[j].position;
                Vector3 targetVec = (target.position - jointPos).Normalized;
                Vector3 endVec = (end.position - jointPos).Normalized;
                Vector3 axis = Vector3.Cross(endVec, targetVec);
                double angle = Vector3.Angle(endVec, targetVec);
                angle *= rotationDamping;
                if (Functions.Abs(angle) > Constants.PI / 4) return;
                Quaternion rot = new Quaternion(axis, angle);
                joints[j].rotation *= rot;
            }
        }
    }
}