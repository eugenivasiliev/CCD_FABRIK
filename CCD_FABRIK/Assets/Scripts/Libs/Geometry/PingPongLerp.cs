using Geometry;
using Math;

public class PingPongLerp : UnityEngine.MonoBehaviour
{
    private Transform Transform;
    [UnityEngine.SerializeField] private UnityEngine.Transform pos1;
    [UnityEngine.SerializeField] private UnityEngine.Transform pos2;
    [UnityEngine.SerializeField, UnityEngine.Range(0, 1)] private double speed;
    private ClampedDouble t = new ClampedDouble(0, 0, 1);
    private int dir = 1;

    private void Start()
    {
        Transform = GetComponent<Transform>();
    }

    void Update()
    {
        t += dir * speed;
        Transform.position = (float)t * pos2.position + (float)(1 - t) * pos1.position;
        if (t == 0 || t == 1) dir *= -1;
    }
}
