
using UnityEngine;

namespace NPC 
{
    public class RaycastNPC : MonoBehaviour
    {
        private Transform pointRay;

        [field: SerializeField] public float maxDistanceRay { get; private set; } = 25;
        [field: SerializeField] public LayerMask targetLayerMask { get; private set; }
        private Ray ray;
        private Vector3 directionTarget;

        public bool isHasRaycastHitTarget { get; private set; }

        private void Awake()
        {
            pointRay = GetComponent<Transform>();
        }

        public bool RaycastForward(Vector3 targetPos)
        {
            directionTarget = GetDirectionTarget(targetPos);
            ray = GetRayForward(directionTarget);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceRay, targetLayerMask))
            {
                return true;
            } 
            else return false; 
        }
        private Vector3 GetDirectionTarget(Vector3 targetPos)
        {
            return targetPos - pointRay.position;
        }
        private Ray GetRayForward(Vector3 directionTarget)
        {
            return new Ray(pointRay.position, directionTarget);
        }
    }
}


