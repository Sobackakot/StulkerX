
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.Camera
{
    public class FreeCameraCharacter : MonoBehaviour, IFreeCamera
    {
        private Transform targetLookPoint;
        private Transform rightLookPointTr;
        private Transform leftLookPointTr;
        public Transform transformCamera { get; private set; }
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

     
        private void Awake()
        {
            transformCamera = GetComponent<Transform>();
            rightLookPointTr = FindObjectOfType<RightFreeLookPointCamera>().transform;
            leftLookPointTr = FindObjectOfType<LeftFreeLookPointCamera>().transform;
        }
 
        private void Start()
        {
            targetLookPoint = rightLookPointTr;
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

        public void SetInputAxis(Vector2 inputAxis)
        {
            mouseAxisX += inputAxis.x * sensitivityMouse * Time.unscaledDeltaTime;
            mouseAxisY -= inputAxis.y * sensitivityMouse * Time.unscaledDeltaTime;
        }
        public void InputCamera_OnScrollMouse(Vector2 scrollMouse)
        {
            mouseZoom -= scrollMouse.y * scrollSpeed * Time.deltaTime;
        }


        public float CheckCameraRotateAngle()
        {
            Vector3 cameraZ = Vector3.ProjectOnPlane(transformCamera.forward, Vector3.up).normalized;
            Vector3 characterZ = Vector3.ProjectOnPlane(targetLookPoint.forward, Vector3.up).normalized;
            return Vector3.SignedAngle(cameraZ, characterZ, Vector3.up);
        }

        public void SwitchLookPointCamera(bool isLeftPointLook, bool isCrouching)
        {
            targetLookPoint = isLeftPointLook ? leftLookPointTr : rightLookPointTr;
        }

    }
}