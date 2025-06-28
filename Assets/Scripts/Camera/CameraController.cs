
using State.Character;
using StateData.Character;
using System.Collections.Generic;
using Zenject;

namespace Character.Camera
{
    public class CameraController
    {
        public CameraController(CharacterStateBootstrap state, CharacterStateContext stateData, RaycastCamera ray,
        [Inject(Id = "cameraTird")] ICameraCharacter cameraTird, [Inject(Id = "firstCam")] ICameraCharacter cameraFerst)
        {
            this.ray = ray;
            this.state = state;
            this.stateData = stateData;

            cameras = new Dictionary<bool, ICameraCharacter>
        {
            { true, cameraFerst },
            { false, cameraTird }
        };
            activeCamera = cameras[this.state.Camera.isFerst];

        }

        private CharacterStateContext stateData;
        private CharacterStateBootstrap state;
        private RaycastCamera ray;

        private ICameraCharacter activeCamera;
        private readonly Dictionary<bool, ICameraCharacter> cameras;


        private void SwitchCamera()
        {
            activeCamera = cameras[state.Camera.isFerst];
        }
        public void Tick()
        {
            SwitchCamera();
            activeCamera.SwitchLookPointCamera(stateData.isLeftTargerPoint, stateData.isCrouch);
            state.Camera.SetStateRotateCamera(stateData.isActiveInventory);
            float angle = activeCamera.CheckCameraRotateAngle();
            state.Camera.SetAngleForCamera(angle);
        }
        public void LateTick()
        {
            activeCamera.FollowCamera();
            if (state.Camera.isStopingRotate)
                activeCamera.RotateCamera(stateData.isAim);
            activeCamera.ZoomCamera(stateData.isAim, stateData.isReloadingState);
        }

        public void FixedTick()
        {
            ray.RaycastHitForItemInteract();
            if (stateData.isAim)
            {
                ray.UpdateRayPointAim();
                ray.Shooting(stateData.isFire);
            }
        }
    }

}
