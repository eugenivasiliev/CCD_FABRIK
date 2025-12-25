using System.Collections.Generic;

namespace Geometry {
    /// <summary>
    /// Class designed for correct initialisation of the <see cref="Transform"/> hierarchy.
    /// </summary>
    public class TransformManager : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private List<Transform> transforms = new List<Transform>();
        void Start()
        {
            foreach (Transform t in transforms) t.Setup();
        }

        void LateUpdate()
        {
            foreach (Transform t in transforms) t.Refresh();
        }
    }
}
