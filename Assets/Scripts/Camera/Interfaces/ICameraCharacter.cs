 
using UnityEngine; 

namespace Character.Camera
{
    public interface ICameraCharacter
    {
        void RotateCamera(bool isAim);
        void FollowCamera();
        void ZoomCamera(bool isAiming, bool isReload);
        void SetInputAxis(Vector2 inputAxis);
        void InputCamera_OnScrollMouse(Vector2 scrollMouse);
        float CheckCameraRotateAngle();
        void SwitchLookPointCamera(bool isLeftPointLook, bool isCrouching);

    }
}

