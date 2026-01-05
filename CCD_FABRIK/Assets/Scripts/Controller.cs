using Geometry;
using Math;

public class Controller : UnityEngine.MonoBehaviour
{
    static public uint iterations = 10;
    static public double tolerance = 0.01d;

    [UnityEngine.SerializeField] private double speed = 1d;
    private Transform Transform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown("z")) iterations++;
        if(UnityEngine.Input.GetKeyDown("x")) iterations = Functions.Max(iterations - 1, 1u);

        if (UnityEngine.Input.GetKeyDown("c")) tolerance *= 1.5d;
        if (UnityEngine.Input.GetKeyDown("v")) tolerance *= 0.66d;

        Vector3 horizontal = UnityEngine.Input.GetAxisRaw("Horizontal") * speed * Vector3.right;
        Vector3 forward = UnityEngine.Input.GetAxisRaw("Vertical") * speed * Vector3.forward;
        Vector3 vertical = Vector3.zero;
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift)) vertical += speed * Vector3.up;
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl)) vertical -= speed * Vector3.up;

        Transform.position += horizontal + vertical + forward;
    }
}
