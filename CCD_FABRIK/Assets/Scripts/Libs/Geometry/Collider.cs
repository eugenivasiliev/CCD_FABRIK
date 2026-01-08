using Geometry;
using Math;

namespace Geometry
{
    public class Collider : UnityEngine.MonoBehaviour
    {
        private Transform Transform;
        public bool colliding { get; private set; }

        void Start()
        {
            Transform = GetComponent<Transform>();
        }

        private void OnTriggerStay(UnityEngine.Collider collider)
        {
            if (collider == null) return;
            colliding = true;
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            if (other == null) return;
            colliding = false;
        }
    }
}