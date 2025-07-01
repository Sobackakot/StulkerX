using UnityEngine;

namespace MainCamera.Raycast
{
    public interface IRaycastHitFPS
    { 
        void UpdateRaycastHitPointAim();
        void RaycastHitShooting(bool isLeftButtonDown);
    }
}