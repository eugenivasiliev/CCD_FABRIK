using Geometry;

namespace Math
{
    /// <summary>
    /// Static class implementing Gradient Descent for <a cref="RotatoryJoint"/>s.
    /// </summary>
    public static class GradientDescent
    {
        #region CONSTS
        public static readonly double alpha = 0.1d;
        public static readonly double costTolerance = 0.001d;
        public static readonly uint iterations = 10;
        public static readonly double momentum = 0.95d;

        public static readonly double velMax = 0.2d;

        public static readonly double stuckTolerance = 0.01d;
        public static readonly uint stuckMaxIterations = 30;

        public static readonly double randomMovementMagnitude = 0.01d;
        #endregion

        /// <summary>
        /// Check for stuck situations where a local minima might have been reached.
        /// </summary>
        public static uint stuckIterations = 0;
        public static bool isStuck => stuckIterations > stuckMaxIterations;

        /// <summary>
        /// Holds the velocities of the joint rotations for smoother movement.
        /// </summary>
        public static System.Collections.Generic.Dictionary<RotatoryJoint, double> velocities = 
            new System.Collections.Generic.Dictionary<RotatoryJoint, double>();

        /// <summary>
        /// Performs <c>iterations</c> iterations of Gradient Descent, with adaptive learning rate.
        /// </summary>
        /// <param name="startJoint"></param>
        /// <param name="target"></param>
        public static void Update(RotatoryJoint startJoint, Transform target)
        {
            double adaptiveAlpha = alpha;
            for(int i = 0; i < iterations; i++) adaptiveAlpha = Solve(startJoint, target, adaptiveAlpha);
        }

        /// <summary>
        /// Retrieves the arm references and applies the Gradient Descent.
        /// </summary>
        /// <param name="startJoint"></param>
        /// <param name="target"></param>
        /// <param name="alpha"></param>
        /// <returns>New adaptive learning rate.</returns>
        private static double Solve(RotatoryJoint startJoint, Transform target, double alpha)
        {
            System.Collections.Generic.List<RotatoryJoint> joints = GetJoints(startJoint);

            if (!velocities.ContainsKey(startJoint))
                foreach (RotatoryJoint joint in joints)
                    velocities.Add(joint, 0);

            double cost = (joints[joints.Count - 1].Transform.children[0].position - target.position).SqrMagnitude;
            if (cost <= costTolerance) return alpha;

            Vector angles = new Vector(joints.Count);
            for (int i = 0; i < angles.values.Count; i++)
                angles[i] = ((IJoint<Quaternion>)joints[i]).t;

            Vector gradient = CalculateGradient(joints, target);
            for (int i = 0; i < joints.Count; ++i)
                velocities[joints[i]] = Functions.Clamp(momentum * velocities[joints[i]] - alpha * gradient[i], -velMax, velMax);

            for (int i = 0; i < angles.values.Count; ++i)
            {
                ClampedDouble cD = ((IJoint<Quaternion>)joints[i]).t;
                cD.Value = angles[i] + velocities[joints[i]] + (isStuck ? RandomMovement() : 0);
                ((IJoint<Quaternion>)joints[i]).t = cD;
            }

            if(isStuck) stuckIterations = 0;

            double newCost = (joints[joints.Count - 1].Transform.children[0].position - target.position).SqrMagnitude;

            if (newCost < cost) return alpha *= 1.1d;

            if (newCost - cost < stuckTolerance) stuckIterations++;
            return alpha * 0.5d;
        }

        /// <summary>
        /// Computes the gradient using <a cref="Vector3.Cross(Vector3, Vector3)"></a>.
        /// </summary>
        /// <param name="joints"></param>
        /// <param name="target"></param>
        /// <returns>Gradient of the function.</returns>
        private static Vector CalculateGradient(System.Collections.Generic.List<RotatoryJoint> joints, Transform target)
        {
            Vector gradient = new Vector(joints.Count);

            Vector3 endPos = joints[joints.Count - 1].Transform.children[0].position;

            for (int i = 0; i < gradient.values.Count; i++)
            {
                Vector3 jointPos = joints[i].Transform.position;
                Vector3 derivative = Vector3.Cross(joints[i].direction, endPos - jointPos);
                gradient[i] = 2.0 * (endPos - target.position) * derivative;
            }

            return gradient;
        }

        /// <summary>
        /// Retrieves all joints for an arm.
        /// </summary>
        /// <param name="startJoint"></param>
        /// <returns>Joint list from base.</returns>
        private static System.Collections.Generic.List<RotatoryJoint> GetJoints(RotatoryJoint startJoint)
        {
            System.Collections.Generic.List<RotatoryJoint> joints = new System.Collections.Generic.List<RotatoryJoint> { startJoint };
            RotatoryJoint curJoint = startJoint;
            while (curJoint.Transform.children.Count > 0)
            {
                if (!curJoint.Transform.children[0].TryGetComponent(out RotatoryJoint temp)) break;
                joints.Add(temp);
                curJoint = temp;
            }
            return joints;
        }

        /// <returns>Small perturbance to avoid local minima.</returns>
        private static double RandomMovement() => (UnityEngine.Random.Range(-1f, 1f)) * randomMovementMagnitude;
    }
}