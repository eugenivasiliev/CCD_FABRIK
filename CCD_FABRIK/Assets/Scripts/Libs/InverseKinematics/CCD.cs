using Geometry;
using Math;
using Joints;
using UnityEngine.UIElements;
using NUnit.Framework.Interfaces;

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

        float rotPercent = 0.1f;
        bool incrRaiseRotAmt = true;

        private readonly double smartAngleTolerance = 0.1d;


        public void Init()
        {
            root = GetComponent<Transform>();
            joints = Utils.GetJoints(root);
            end = joints[joints.Count - 1].GetChildByTag("End");
            dists = Utils.GetDists(joints, end, out totalDistance);
        }

        /*
         List<Node> nodes, 
    Vector3 targ, 
    float rotPercent        = 0.1f, 
    bool incrRaiseRotAmt    = true, 
    int iterCt              = 20, 
    float eps               = 0.001f)
{
    int lastNodeIdx = nodes.Count - 1;
    Transform endEffector = nodes[lastNodeIdx].transform;
    float rotAmt = rotPercent;
    // For each iteration
    for(int i = 0; i < iterCt; ++i)
    { 
        // For each bone in the kinematic chain
        for(int nit = 0; nit < lastNodeIdx; ++nit)
        { 
            
            Vector3 basePos = nodes[nit].transform.position;
            Vector3 EEPos = endEffector.position;

            // Calculate Bone->EE
            Vector3 baseToEE = EEPos - basePos;

            // Calculate Bone->Trg
            Vector3 baseToTarg = targ - basePos;

            // Calculate rotation
            Quaternion rotFromto = Quaternion.FromToRotation(baseToEE, baseToTarg);

            // Calculate partial rotation & apply partial rotation
            Quaternion rotRestrained = Quaternion.Lerp(Quaternion.identity, rotFromto, rotAmt);
            nodes[nit].transform.rotation = rotRestrained * nodes[nit].transform.rotation;
        }

        // If increased greediness, increase to where rotation amount is 
        // close to 1.0 on the final iteration.
        if(incrRaiseRotAmt)
            rotAmt += (1.0f - rotPercent)/(iterCt - 1);

        // Check if our IK is at a solution that's "good enough"
        float distEEtoTarg = (endEffector.position - targ).magnitude;
        if(distEEtoTarg <= eps)
            break;

    }*/

        void Update()
        {
            if ((target.position - end.position).Magnitude < Controller.tolerance) return;

            double rotAmt = rotPercent;

            for(int k = 0; k < Controller.iterations; ++k)
            {
                for (int i = joints.Count - 1; i >= 0; i--)
                {

                    Vector3 basePos = joints[i].transform.position;
                    Vector3 EEPos = end.position;

                    // Calculate Bone->EE
                    Vector3 baseToEE = EEPos - basePos;

                    // Calculate Bone->Trg
                    Vector3 baseToTarg = target.position - basePos;

                    double ang = Vector3.Angle(baseToEE, baseToTarg);
                    Vector3 axis = Vector3.Cross(baseToEE, baseToTarg);

                    joints[i].rotation *= new Quaternion(axis, ang);

                    if (incrRaiseRotAmt)
                        rotAmt += (1.0f - rotPercent) / (Controller.iterations - 1);

                    //Early exit
                    if ((target.position - end.position).Magnitude < Controller.tolerance || k > Controller.iterations) return;
                }
            }
        }

        private (Vector3 endVector, Vector3 targetVector) GetRotationVectors(Transform joint)
        {
            return ((end.position - joint.position).Normalized, (target.position - joint.position).Normalized);
        }
    }
}