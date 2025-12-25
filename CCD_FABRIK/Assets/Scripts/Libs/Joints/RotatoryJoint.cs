using Geometry;
using Math;

namespace Joints
{
    /// <summary>
    /// Joint implementing <b>rotation</b> with <see cref="Quaternion"/>
    /// </summary>
    /// <remarks>Derives from <see cref="UnityEngine.MonoBehaviour"/> 
    /// to allow to add it as component script and use 
    /// <see cref="Start"/> and <see cref="Update"/>
    /// </remarks>
    public class RotatoryJoint : UnityEngine.MonoBehaviour, IJoint<Quaternion>
    {
        [UnityEngine.SerializeField] private string positiveMotionKeyId;
        public string PositiveMotionKeyId => positiveMotionKeyId;

        [UnityEngine.SerializeField] private string negativeMotionKeyId;
        public string NegativeMotionKeyId => negativeMotionKeyId;

        ClampedDouble IJoint<Quaternion>.t { get; set; }

        [UnityEngine.SerializeField] private double minRange;
        double IJoint<Quaternion>.MinRange => minRange;

        [UnityEngine.SerializeField] private double maxRange;
        double IJoint<Quaternion>.MaxRange => maxRange;

        [UnityEngine.SerializeField] private double delta;
        double IJoint<Quaternion>.Delta => delta;

        ClampedDouble IJoint<Quaternion>.prev { get; set; }

        public Vector3 direction = Vector3.up;
        [UnityEngine.SerializeField] private Quaternion startRotation;

        Quaternion IJoint<Quaternion>.GetLocalTransformation() => new Quaternion(Transform.rotation * direction, ((double)castedJoint.t - (double)castedJoint.prev), true);

        [UnityEngine.SerializeField] public Transform Transform;

        private IJoint<Quaternion> castedJoint;

        private void Start()
        {
            castedJoint = this;
            castedJoint.Setup();
            UnityEngine.InputSystem.InputSystem.actions.Enable();
        }

        public void Update()
        {
            Transform.rotation = castedJoint.GetLocalTransformation() * Transform.rotation;
            castedJoint.prev = castedJoint.t;
        }
    }
}