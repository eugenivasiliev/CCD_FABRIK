using Game;
using Geometry;
using Joints;
using Math;

public class Level1_ArmManager : UnityEngine.MonoBehaviour
{
    private Transform Transform;
    private System.Collections.Generic.List<Transform> joints;

    void Start()
    {
        Transform = GetComponent<Transform>();
        joints = Utils.GetJoints(Transform);
    }

    void Update()
    {
        if (Vector3.Distance(
            joints[joints.Count - 1].GetChildByTag("End").position,
            UnityEngine.GameObject.FindGameObjectWithTag("Collectible").transform.position)
            < 1d
            )
        {
            SceneManager.LoadScene("Level2");
        }

        foreach (var joint in joints)
        {
            if (joint.gameObject.GetComponent<Collider>().colliding) 
                SceneManager.ReloadScene();
        }
    }
}
