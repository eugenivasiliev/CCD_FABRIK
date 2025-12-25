using Math;
using Geometry;

/// <summary>
/// Joint implementing <b>translation</b> with <see cref="Matrix4x4"/>
/// </summary>
/// <remarks>Derives from <see cref="UnityEngine.MonoBehaviour"/> 
/// to allow to add it as component script and use 
/// <see cref="Start"/> and <see cref="Update"/>
/// </remarks>
public class LinearJoint : UnityEngine.MonoBehaviour, IJoint<Matrix4x4>
{
    [UnityEngine.SerializeField] private string positiveMotionKeyId;
    public string PositiveMotionKeyId => positiveMotionKeyId;

    [UnityEngine.SerializeField] private string negativeMotionKeyId;
    public string NegativeMotionKeyId => negativeMotionKeyId;

    ClampedDouble IJoint<Matrix4x4>.t { get; set; }
    ClampedDouble IJoint<Matrix4x4>.prev { get; set; }

    [UnityEngine.SerializeField] private double minRange;
    double IJoint<Matrix4x4>.MinRange => minRange;

    [UnityEngine.SerializeField] private double maxRange;
    double IJoint<Matrix4x4>.MaxRange => maxRange;

    [UnityEngine.SerializeField] private double delta;
    double IJoint<Matrix4x4>.Delta => delta;

    [UnityEngine.SerializeField] private Vector3 direction = Vector3.up;

    Matrix4x4 IJoint<Matrix4x4>.GetLocalTransformation() => 
        Matrix4x4.CreateTranslation(((double)castedJoint.t - (double)castedJoint.prev) * (this.Transform.rotation * direction));

    [UnityEngine.SerializeField] public Transform Transform;

    private IJoint<Matrix4x4> castedJoint;

    private void Start()
    {
        castedJoint = this;
        castedJoint.Setup();
        UnityEngine.InputSystem.InputSystem.actions.Enable();
    }

    public void Update()
    {
        this.Transform.position = castedJoint.GetLocalTransformation() * this.Transform.position;
        castedJoint.prev = castedJoint.t;
    }
}
