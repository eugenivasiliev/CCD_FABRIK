public class Controller : UnityEngine.MonoBehaviour
{
    static public uint iterations = 10;
    static public double tolerance = 0.01d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown("Z")) iterations++;
        if(UnityEngine.Input.GetKeyDown("X")) iterations--;

        if (UnityEngine.Input.GetKeyDown("C")) tolerance *= 1.5d;
        if (UnityEngine.Input.GetKeyDown("V")) tolerance *= 0.66d;
    }
}
