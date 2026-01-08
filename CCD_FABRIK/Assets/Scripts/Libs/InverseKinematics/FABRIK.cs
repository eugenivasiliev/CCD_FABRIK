using Geometry;
using Joints;
using Math;

namespace InverseKinematics
{
    /// <summary>
    /// <a cref="UnityEngine.MonoBehaviour"/> class implementing <b>Forward And Backward Inverse Kinematics</b>.
    /// </summary>
    public class FABRIK : UnityEngine.MonoBehaviour
    {
        private Transform root;
        private Vector3 rootStartPos;
        public Transform target;
        private System.Collections.Generic.List<Transform> joints;
        private System.Collections.Generic.List<double> dists;
        private double totalDistance;
        private Transform end;

        private readonly System.Collections.Generic.List<string> tags = new System.Collections.Generic.List<string> { "Joint", "End" };

        public double targetDistance
        {
            get => (target.position - end.position).Magnitude;
        }

        public int lastFrameIterations { get; private set; }

        public void Init()
        {
            UnityEngine.Debug.Log("FABRIK");
            root = GetComponent<Transform>();
            rootStartPos = root.position;
            joints = Utils.GetJoints(root);
            UnityEngine.Debug.Log(joints.Count);
            end = joints[joints.Count - 1].GetChildByTag("End");
            dists = Utils.GetDists(joints, end, out totalDistance);
        }

        void Update()
        {
            for (int k = 0; k < Controller.iterations; ++k)
            {
                if ((target.position - end.position).Magnitude < Controller.tolerance)
                {
                    lastFrameIterations = k;
                    return;
                }

                Forward();
                Backward();
            }

            lastFrameIterations = (int)Controller.iterations - 1;
        }

        /// <summary>
        /// Sets the positions for the joints in the forward direction (end-to-root).
        /// </summary>
        private void Forward()
        {
            end.position = target.position;
            for (int i = joints.Count - 1; i >= 0; --i)
            {
                Vector3 direction = (joints[i].position - joints[i].GetChildByTag(tags).position).Normalized;
                double length = dists[i];
                joints[i].position = joints[i].GetChildByTag(tags).position + length * direction;
            }
        }

        /// <summary>
        /// Sets the positions for the joints in the backward direction (root-to-end).
        /// </summary>
        private void Backward()
        {
            root.position = rootStartPos;
            for (int i = 0; i < joints.Count - 1; ++i)
            {
                Vector3 direction = (joints[i].GetChildByTag(tags).position - joints[i].position).Normalized;
                double length = dists[i];
                joints[i].GetChildByTag(tags).position = joints[i].position + length * direction;
            }
        }
    }
}