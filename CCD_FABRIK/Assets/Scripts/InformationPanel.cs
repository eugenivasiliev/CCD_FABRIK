using InverseKinematics;

public class InformationPanel : UnityEngine.MonoBehaviour
{

    [UnityEngine.SerializeField] TMPro.TMP_Text AlgorithmName;
    [UnityEngine.SerializeField] TMPro.TMP_Text LastFrameIterations;
    [UnityEngine.SerializeField] TMPro.TMP_Text TargetDistance;
    public CCD ccd;
    public FABRIK fabrik;

    void Start()
    {
        if (ccd != null) AlgorithmName.text = "CCD";
        if (fabrik != null) AlgorithmName.text = "FABRIK";
    }

    void Update()
    {
        if (ccd != null) TargetDistance.text = "Dist: " + ccd.targetDistance;
        if (fabrik != null) TargetDistance.text = "Dist: " + fabrik.targetDistance;

        if (ccd != null) LastFrameIterations.text = "Iters: " + ccd.lastFrameIterations;
        if (fabrik != null) LastFrameIterations.text = "Iters: " + fabrik.lastFrameIterations;
    }
}
