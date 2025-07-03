using StateData.Character;
using Character.MainCamera.Raycast;
using Window.UI;

namespace Character.MainCamera.BootStrap
{
    public class MainCameraEntryPoint
    {
        public MainCameraEntryPoint(

            CharacterStateContext stateContext, 
            IRaycastHitItem raycastHitItem, 
            IRaycastHitFPS raycastHitFPS,
            IFreeCamera freeCamera, 
            IFirstCamera firstCamera)
        {
            this.raycastHitItem = raycastHitItem;
            this.raycastHitFPS = raycastHitFPS;
            this.stateContext = stateContext;
            this.freeCamera = freeCamera;
            this.firstCamera = firstCamera;
        }

        public CharacterStateContext stateContext { get; private set; }

        private IRaycastHitItem raycastHitItem;
        private IRaycastHitFPS raycastHitFPS;

        private IFreeCamera freeCamera;
        private IFirstCamera firstCamera;
        private ICameraCharacter activeCamera; 


        private void SwitchCamera()
        {
            if (stateContext == null) return;
            activeCamera = stateContext.isFirstCamera ? firstCamera : freeCamera;
        }
        public void Tick()
        {
            SwitchCamera();
            activeCamera?.SwitchLookPointCamera(stateContext.isLeftTargerPoint, stateContext.isCrouch);  
            stateContext.currentAngle = activeCamera.CheckCameraRotateAngle();
            activeCamera?.SetInputAxis(stateContext.inputAxisCamera);
        }
        public void LateTick()
        { 
            activeCamera?.FollowCamera();
            activeCamera?.RotateCamera(stateContext.isAim);
            activeCamera?.ZoomCamera(stateContext.isAim, stateContext.isReloadingState); 
        }

        public void FixedTick()
        {
            raycastHitItem?.RaycastHitForItemInteract();
            if (stateContext.isAim)
            {
                raycastHitFPS?.UpdateRaycastHitPointAim();
                raycastHitFPS?.RaycastHitShooting(stateContext.isFire);
            }
        }
    }

}
