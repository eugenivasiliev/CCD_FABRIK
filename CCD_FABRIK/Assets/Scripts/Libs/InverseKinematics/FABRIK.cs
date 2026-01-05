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

        

        void Start()
        {
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
                if ((target.position - end.position).Magnitude < Controller.tolerance) return;

                Forward();
                Backward();
            }
        }

        /// <summary>
        /// Sets the positions for the joints in the forward direction (end-to-root).
        /// </summary>
        private void Forward()
        {
            end.position = target.position;
            for (int i = joints.Count - 1; i >= 0; --i)
            {
                Vector3 direction = (joints[i].position - joints[i].firstChild.position).Normalized;
                double length = dists[i];
                joints[i].position = joints[i].firstChild.position + length * direction;
            }
        }

        /// <summary>
        /// Sets the positions for the joints in the backward direction (root-to-end).
        /// </summary>
        private void Backward()
        {
            root.position = rootStartPos;
            for (int i = 0; i < joints.Count; ++i)
            {
                Vector3 direction = (joints[i].firstChild.position - joints[i].position).Normalized;
                double length = dists[i];
                joints[i].firstChild.position = joints[i].position + length * direction;
            }
        }
    }
}