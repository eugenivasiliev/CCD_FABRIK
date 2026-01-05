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
    }

    void Update()
    {
        if (MeshTransform.colliding) SceneManager.ReloadScene();
        Transform target = Transform.GetChildByTag(new System.Collections.Generic.List<string>{ "Joint", "End" });
        MeshTransform.LookAtPivoting(target.position, Vector3.up);
    }
}
