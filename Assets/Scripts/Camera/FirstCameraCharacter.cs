
using UnityEngine;

namespace Character.MainCamera
{
    public class FirstCameraCharacter : MonoBehaviour, IFirstCamera
    {
        private Transform targetLookPoint;
        [HideInInspector] public Transform transformCamera;

        [SerializeField] private float sensitivityMouse = 6f;
        [SerializeField] private float transitionSpeed = 9f;

        private Vector3 offset;
        private float mouseAxisX;
        private float mouseAxisY;

        private float minAngle = -90f;
        private float maxAngle = 90f;
        private float mouseZoom;

        private float minZoom = 0f;
        private float maxZoom = 0f;

   
        private void Awake()
        {
            transformCamera = GetComponent<Transform>();
            targetLookPoint = FindObjectOfType<FirstLookPointCamera>()?.transform;
        }
 
        void Start()
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
            mouseZoom = isAiming ? (isReloadWeapon ? maxZoom : minZoom) : maxZoom;
            transformCamera.position = targetLookPoint.position - transformCamera.forward * mouseZoom;
        }

        public void SetInputAxis(Vector2 inputAxis)
        { 
            mouseAxisX += inputAxis.x * sensitivityMouse * Time.deltaTime;
            mouseAxisY -= inputAxis.y * sensitivityMouse * Time.deltaTime;
        }
        public void InputCamera_OnScrollMouse(Vector2 scrollMouse) { } 
        public float CheckCameraRotateAngle() { return 0; } 
        public void SwitchLookPointCamera(bool isLeftPointLook, bool isCrouching) { }
    }
}