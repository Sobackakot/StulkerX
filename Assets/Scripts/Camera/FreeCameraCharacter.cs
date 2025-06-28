 
using UnityEngine;
using Zenject;
using State.Character;

namespace Character.Camera
{
    public class FreeCameraCharacter : MonoBehaviour, ICameraCharacter
    {
        private Transform targetLookPoint;

        [HideInInspector] public Transform transformCamera;
        [SerializeField] private float sensitivityMouse = 45f;
        [SerializeField] private float scrollSpeed = 3f;
        [SerializeField] private float transitionSpeed = 9f;
        [SerializeField] private float leftTarget = -0.500f;
        [SerializeField] private float rightTarget = 0.500f;


        private Vector3 offset;
        public float mouseAxisX;
        private float mouseAxisY;
        private float mouseZoom;

        private float minAngle = -90f;
        private float maxAngle = 90f;
        private float minZoom = 1f;
        private float maxZoom = 2f;

        private float newHeigth;

        private CharacterStateBootstrap state;

        [Inject]
        private void Construct(CharacterStateBootstrap state)
        {
            this.state = state;
        }
        private void Awake()
        {
            transformCamera = GetComponent<Transform>();
            targetLookPoint = FindObjectOfType<TargetFreeCamera>()?.transform;
        }
        private void OnEnable()
        {
            state.Camera.OnInputAxis += InputCamera_OnInputAxis;
        }
        private void OnDisable()
        {
            state.Camera.OnInputAxis -= InputCamera_OnInputAxis;
        }
        private void Start()
        {
            offset = transformCamera.position - targetLookPoint.position;
        }
        public void FollowCamera()
        {
            Vector3 newPosition = transformCamera.localRotation * offset + targetLookPoint.position;
            transformCamera.position = Vector3.Lerp(transformCamera.position, newPosition, Time.deltaTime * transitionSpeed);
        }
        public void RotateCamera(bool isAim)
        {
            transitionSpeed = isAim ? 15 : 9;
            mouseAxisY = Mathf.Clamp(mouseAxisY, minAngle, maxAngle);
            Quaternion newRot = Quaternion.Euler(mouseAxisY, mouseAxisX, 0);
            transformCamera.rotation = Quaternion.Slerp(transformCamera.rotation, newRot, Time.smoothDeltaTime * transitionSpeed);
        }
        public void ZoomCamera(bool isAiming, bool isReloadWeapon)
        {
            float targetZoom = isAiming ? (isReloadWeapon ? maxZoom : minZoom) : maxZoom;
            mouseZoom = Mathf.Lerp(mouseZoom, targetZoom, Time.deltaTime * transitionSpeed);

            transformCamera.position = targetLookPoint.position - transformCamera.forward * mouseZoom;
        }

        public void InputCamera_OnInputAxis(Vector2 inputAxis)
        {
            if (state.Camera.isStopingRotate)
            {
                mouseAxisX += inputAxis.x * sensitivityMouse * Time.deltaTime;
                mouseAxisY -= inputAxis.y * sensitivityMouse * Time.deltaTime;
            }
        }
        public void InputCamera_OnScrollMouse(Vector2 scrollMouse)
        {
            if (state.Camera.isStopingRotate)
            {
                mouseZoom -= scrollMouse.y * scrollSpeed * Time.deltaTime;
            }
        }


        public float CheckCameraRotateAngle()
        {
            Vector3 cameraZ = Vector3.ProjectOnPlane(transformCamera.forward, Vector3.up).normalized;
            Vector3 characterZ = Vector3.ProjectOnPlane(targetLookPoint.forward, Vector3.up).normalized;
            return Vector3.SignedAngle(cameraZ, characterZ, Vector3.up);
        }

        public void SwitchLookPointCamera(bool isLeftPointLook, bool isCrouching)
        {
            float heightPoint = isCrouching ? 1.2f : 1.6f;
            newHeigth = Mathf.Lerp(newHeigth, heightPoint, Time.deltaTime * transitionSpeed);
            Vector3 targetPosition = new Vector3(isLeftPointLook ? leftTarget : rightTarget, newHeigth, 0);
            targetLookPoint.localPosition = Vector3.Lerp(targetLookPoint.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
        }

    }
}