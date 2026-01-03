using Math;
using Geometry;

public class MeshRotationHandler : UnityEngine.MonoBehaviour
{
    private Transform Transform;
    private Transform MeshTransform;

    void Start()
    {
        Transform = this.GetComponent<Transform>();
        MeshTransform = Transform.GetComponentInChildren<UnityEngine.MeshRenderer>().GetComponent<Transform>();
        UnityEngine.Debug.Log(MeshTransform.gameObject.name);
    }

    void Update()
    {
        MeshTransform.LookAt(Transform.children[0].position, Vector3.up + 0.1d * Vector3.forward);
    }
}
