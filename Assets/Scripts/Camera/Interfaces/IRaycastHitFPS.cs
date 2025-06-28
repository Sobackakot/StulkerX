using UnityEngine;

public interface IRaycastHitFPS 
{
    Ray GetRayForwardFromCamera();
    void UpdateRayPointAim();
    void Shooting(bool isLeftButtonDown);
}
