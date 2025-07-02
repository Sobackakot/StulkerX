using UnityEngine;


namespace Character.MainCamera.Raycast
{
    public interface IRaycastHitParcour
    {
        bool SetRayHitParcour(out RaycastHit hitForward, out RaycastHit hitDown);
    }
}