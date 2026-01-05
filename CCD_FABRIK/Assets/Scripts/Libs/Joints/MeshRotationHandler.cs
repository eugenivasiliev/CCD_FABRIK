using Math;
using Geometry;
using Game;

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
        if (MeshTransform.colliding) SceneManager.ReloadScene();
        MeshTransform.LookAtPivoting(Transform.GetChildByTag("Joint").position, Vector3.up);
    }
}
