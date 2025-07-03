using Character.Context;
using Character.MainCamera.Raycast;

namespace Character.MainCamera.BootStrap
{
    public class MainCameraEntryPoint
    {
        public MainCameraEntryPoint(

            IContextStates contextStates,
            IContextCommands contextCommands, 
            IRaycastHitItem raycastHitItem, 
            IRaycastHitFPS raycastHitFPS,
            IFreeCamera freeCamera, 
            IFirstCamera firstCamera)
        {
            this.contextStates = contextStates;
            this.contextCommands = contextCommands;
            this.raycastHitItem = raycastHitItem;
            this.raycastHitFPS = raycastHitFPS; 
            this.freeCamera = freeCamera;
            this.firstCamera = firstCamera;
        }

        public IContextStates contextStates { get; private set; }
        public IContextCommands contextCommands { get; private set; }

        private IRaycastHitItem raycastHitItem;
        private IRaycastHitFPS raycastHitFPS;

        private IFreeCamera freeCamera;
        private IFirstCamera firstCamera;
        private ICameraCharacter activeCamera; 


        private void SwitchCamera()
        {
            if (contextCommands == null) return;
            activeCamera = contextStates.IsFirstCamera ? firstCamera : freeCamera;
        }
        public void Tick()
        {
            SwitchCamera();
            activeCamera?.SwitchLookPointCamera(contextStates.IsLeftTargerPoint, contextStates.IsCrouch);  
            contextCommands.SetCurrentAngle(activeCamera.CheckCameraRotateAngle());
            activeCamera?.SetInputAxis(contextStates.InputAxisCamera);
        }
        public void LateTick()
        { 
            activeCamera?.FollowCamera();
            activeCamera?.RotateCamera(contextStates.IsAim);
            activeCamera?.ZoomCamera(contextStates.IsAim, contextStates.IsReloadingState); 
        }

        public void FixedTick()
        {
            raycastHitItem?.RaycastHitForItemInteract();
            if (contextStates.IsAim)
            {
                raycastHitFPS?.UpdateRaycastHitPointAim();
                raycastHitFPS?.RaycastHitShooting(contextStates.IsFire);
            }
        }
    }

}
