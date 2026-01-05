using System.Collections.Generic;
using Geometry;
using InverseKinematics;
using Math;

public class CCDArmCreator : UnityEngine.MonoBehaviour
{
    [UnityEngine.SerializeField] private Vector3 startPos;
    [UnityEngine.SerializeField] private Transform target;
    [UnityEngine.SerializeField] private TransformManager transformManager;
    [UnityEngine.SerializeField] private UnityEngine.GameObject rootJointPrefab;
    [UnityEngine.SerializeField] private Transform root;
    [UnityEngine.SerializeField] private UnityEngine.GameObject jointPrefab;
    [UnityEngine.SerializeField] private UnityEngine.GameObject endEffectorPrefab;
    [UnityEngine.SerializeField] private uint armLength;

    private void Awake()
    {
        List<Transform> joints = new List<Transform>();
        UnityEngine.GameObject rootGO = Instantiate(rootJointPrefab, startPos, Quaternion.identity);
        root = rootGO.transform.GetChild(0).GetComponent<Transform>();
        joints.Add(root);
        for (int i = 1; i < armLength; i++)
        {
            Transform trans = Instantiate(jointPrefab, startPos + i * 0.5d * Vector3.forward, Quaternion.identity).transform.GetChild(0).GetComponent<Transform>();
            trans.parent = joints[i - 1];
            trans.Tag = "Joint";
            joints.Add(trans);
        }
        
        Transform end = Instantiate(endEffectorPrefab, startPos + armLength * 0.5d * Vector3.forward, Quaternion.identity).GetComponent<Transform>();
        end.parent = joints[(int)armLength - 1];
        joints[(int)armLength - 1].children.Add(end);

        for (int i = 0; i < armLength - 1; i++)
        {
            joints[i].children.Add(joints[i+1]);
            transformManager.transforms.Add(joints[i]);
        }
        transformManager.transforms.Add(joints[(int)armLength-1]);
        transformManager.transforms.Add(end);
        root.GetComponent<CCD>().target = target;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root.GetComponent<CCD>().Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
