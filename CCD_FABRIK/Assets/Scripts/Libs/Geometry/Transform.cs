using System.Drawing;
using Math;

namespace Geometry
{
    /// <summary>
    /// Custom <c>Transform</c> class.
    /// </summary>
    /// <remarks>Derives from <see cref="UnityEngine.MonoBehaviour"/>,
    /// to allow to add it as component script and synchronise with <see cref="UnityEngine.Transform"/> 
    /// on <see cref="Start"/> and <see cref="Update"/>
    /// </remarks>
    public class Transform : UnityEngine.MonoBehaviour
    {
        public Transform parent;
        public System.Collections.Generic.List<Transform> children;

        [UnityEngine.SerializeField]
        private Vector3 localPosition = Vector3.zero;
        public Vector3 position
        {
            get 
            {
                if (parent != null) {
                    return (parent.rotation * localPosition) + parent.position;
                }
                else {
                    return localPosition;
                }
            }
            set
            {
                if (parent != null)
                {
                    localPosition = parent.rotation.inverse * (value - parent.position);
                }
                else localPosition = value;
            }
        }
        public Quaternion localRotation = Quaternion.identity;
        public Quaternion rotation {
            get => (parent != null) ? parent.rotation * localRotation : localRotation;
            set
            {
                if (parent != null) localRotation = parent.rotation.inverse * value;
                else localRotation = value;
            }
        }
        public Vector3 eulerAngles
        {
            get { return Constants.Deg2Rad * localRotation.eulerAngles; }
            set
            {
                Vector3 reducedVal = value;
                reducedVal.x %= 360;
                reducedVal.y %= 360;
                reducedVal.z %= 360;
                localRotation = new Quaternion(reducedVal.x, reducedVal.y, reducedVal.z, true);
            }
        }
        public Vector3 scale = Vector3.zero;

        public Vector3 velocity = Vector3.zero;
        public Vector3 acceleration = Vector3.zero;

        public Vector3 angularVelocity = Vector3.zero;
        public Vector3 angularAcceleration = Vector3.zero;

        public Transform() { }

        public Transform(Transform transform)
        {
            this.parent = transform.parent;
            this.position = transform.position;
            this.rotation = transform.rotation;
            this.scale = transform.scale;
            this.children = transform.children;
        }

        public Transform(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
        public Transform(Quaternion rotation, Vector3 angularVelocity)
        {
            this.localRotation = rotation;
            this.angularVelocity = angularVelocity;
        }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public static implicit operator Transform(UnityEngine.Transform transform) =>
            new Transform(transform.position, transform.rotation, transform.lossyScale);

        /// <remarks>Synchronises with <see cref="UnityEngine.Transform"/></remarks>
        public void Setup()
        {
            this.position = this.transform.position;
            this.rotation = this.transform.rotation;
            this.scale = this.transform.lossyScale;
        }

        /// <remarks>Synchronises with <see cref="UnityEngine.Transform"/></remarks>
        public void Refresh()
        {
            this.transform.position = this.position;
            this.transform.rotation = this.rotation;
        }
    }
}