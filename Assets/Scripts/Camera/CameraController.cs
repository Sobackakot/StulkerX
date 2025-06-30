using StateData.Character;
using UnityEngine;

namespace Character.Camera
{
    public class CameraController
    {
        public CameraController(

            CharacterStateContext stateContext, RaycastCamera ray,
            IFreeCamera freeCamera, IFirstCamera firstCamera)
        {
            this.ray = ray;
            this.stateContext = stateContext;
            this.freeCamera = freeCamera;
            this.firstCamera = firstCamera;
        }

        public CharacterStateContext stateContext { get; private set; }
        private RaycastCamera ray;

        private IFreeCamera freeCamera;
        private IFirstCamera firstCamera;
        private ICameraCharacter activeCamera; 


        private void SwitchCamera()
        {
            activeCamera = stateContext.isFerst ? firstCamera : freeCamera;
        }
        public void Tick()
        {
            SwitchCamera();
            activeCamera.SwitchLookPointCamera(stateContext.isLeftTargerPoint, stateContext.isCrouch); 
            float angle = activeCamera.CheckCameraRotateAngle();
            activeCamera.SetInputAxis(stateContext.inputAxisCamera);
        }
        public void LateTick()
        {
            
            activeCamera.FollowCamera(); 
            activeCamera.RotateCamera(stateContext.isAim);
            activeCamera.ZoomCamera(stateContext.isAim, stateContext.isReloadingState);
        }

        public void FixedTick()
        {
            ray.RaycastHitForItemInteract();
            if (stateContext.isAim)
            {
                ray.UpdateRayPointAim();
                ray.Shooting(stateContext.isFire);
            }
        }
    }

}
