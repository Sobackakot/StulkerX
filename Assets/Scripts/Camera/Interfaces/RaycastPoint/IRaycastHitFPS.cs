using UnityEngine;

namespace Character.MainCamera.Raycast
{
    public interface IRaycastHitFPS
    { 
        void UpdateRaycastHitPointAim();
        void RaycastHitShooting(bool isLeftButtonDown);
    }
}