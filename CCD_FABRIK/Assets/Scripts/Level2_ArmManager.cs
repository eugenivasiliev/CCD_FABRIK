using Game;
using Geometry;
using Joints;
using Math;

public class Level2_ArmManager : UnityEngine.MonoBehaviour
{
    private Transform Transform;
    private System.Collections.Generic.List<Transform> joints;
    private int score = 0;

    void Start()
    {
        Transform = GetComponent<Transform>();
        joints = Utils.GetJoints(Transform);
    }

    void Update()
    {
        if(score == 3)
        {
            SceneManager.LoadScene("Win");
        }

        foreach(var go in UnityEngine.GameObject.FindGameObjectsWithTag("Collectible"))
        {
            if (Vector3.Distance(
            joints[joints.Count - 1].GetChildByTag("End").position, go.transform.position
            )
            < 1d
            )
            {
                Destroy( go );
                score++;
            }
        }
        

        foreach (var joint in joints)
        {
            if (joint.GetComponentInChildren<Collider>().colliding) 
                SceneManager.ReloadScene();
        }
    }
}
