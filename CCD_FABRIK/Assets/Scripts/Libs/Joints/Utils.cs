using Geometry;

namespace Joints
{
    public class Utils
    {
        /// <summary>
        /// Retrieves all joints for an arm.
        /// </summary>
        /// <param name="startJoint">Root joint.</param>
        /// <returns>Joint list ordered from base.</returns>
        public static System.Collections.Generic.List<Transform> GetJoints(Transform startJoint)
        {
            System.Collections.Generic.List<Transform> joints = new System.Collections.Generic.List<Transform> { startJoint };
            Transform curJoint = startJoint;
            while (curJoint.children.Count > 0)
            {
                Transform nextJoint = curJoint.GetChildByTag("Joint");
                if (nextJoint == null) break;

                joints.Add(nextJoint);
                curJoint = nextJoint;
            }
            //Remove last since it's the end effector, not a joint
            return joints;
        }

        /// <param name="joints">List of joints.</param>
        /// <param name="totalLength">Sum of all distances.</param>
        /// <returns>Distances between each pair of joints.</returns>
        public static System.Collections.Generic.List<double> GetDists(System.Collections.Generic.List<Transform> joints, Transform end, out double totalLength)
        {
            System.Collections.Generic.List<double> dists = new System.Collections.Generic.List<double>(0);
            double total = 0;
            double dist;
            for (int i = 0; i < joints.Count - 1; i++)
            {
                dist = (joints[i + 1].position - joints[i].position).Magnitude;
                dists.Add(dist);
                total += dist;
            }
            UnityEngine.Debug.Assert(end != null);
            UnityEngine.Debug.Assert(joints[joints.Count - 1] != null);
            dist = (end.position - joints[joints.Count - 1].position).Magnitude;
            dists.Add(dist);
            total += dist;
            totalLength = total;
            return dists;
        }
    }
}
