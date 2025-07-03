using Character.InputEvents;
using Inventory;
using StateData.Character;
using System;
using UnityEngine;
using Zenject;


namespace Character.MainCamera.Raycast
{
    public class RaycastPointCamera : MonoBehaviour, IRaycastHitFPS, IRaycastHitItem, IRaycastHitParcour, IRaycastHitLootBox
    {
        public event Action<string> onShowTextByHitPoint;
        public event Func<GameObject,bool> onSetParentByWeapon;

        private Transform targetAimingTr;
        [SerializeField] private float aimPointSpeed = 45f;
        [SerializeField] private Vector3 offsetPointRayFor = new Vector3(0, 0.25f, 0);

        private Transform pointRayCameraTr;
        private Transform pointRayCharacterTr;
        [SerializeField] private float maxRayInteract = 4f;
        [SerializeField] private float maxRayForwardParcoure = 0.8f;
        [SerializeField] private float maxRayHeightParcoure = 6f;
        private float maxRayAiming = 1000f;

        public LayerMask layerMaskLootBox;
        public LayerMask layerMaskItem;
        public LayerMask ignorLayerMask;
        public LayerMask climbLayerMask;

        private Ray rayForward;
        private Ray rayDown;
        private RaycastHit hitForward;
        private RaycastHit hitDown;
         

        private CharacterStateContext stateData;
        private IInputEvents inputEvent;

        [Inject]
        private void Construct(IInputEvents inputEvent, CharacterStateContext stateData)
        {
            this.inputEvent = inputEvent;
            this.stateData = stateData;
        }
        private void Awake()
        {
            pointRayCameraTr = GetComponent<Transform>();  
            pointRayCharacterTr = FindObjectOfType<CharacterInspector>()?.transform;
            targetAimingTr = GetComponentInChildren<TargetRayPointAim>()?.transform; 
        }
        private void OnEnable()
        {
            if (inputEvent == null) return;
            inputEvent.OnActiveInventoryLootBox += OnActiveInventoryUIPanel;
            inputEvent.OnHasWeapon += OnRaycastHitForItem;
        }
        private void OnDisable()
        {
            if (inputEvent == null) return;
            inputEvent.OnActiveInventoryLootBox -= OnActiveInventoryUIPanel;
            inputEvent.OnHasWeapon -= OnRaycastHitForItem;
        }
        void IRaycastHitFPS.RaycastHitShooting(bool isLeftButtonDown)
        {
            if (isLeftButtonDown)
            {

                rayForward = GetRayForwardFromCamera();
                if (Physics.Raycast(rayForward, out hitForward, maxRayAiming))
                {
                    hitForward.rigidbody?.AddForce(-hitForward.normal * 1f, ForceMode.Impulse);
                }
            }
        }
        void IRaycastHitFPS.UpdateRaycastHitPointAim()
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayAiming, ~ignorLayerMask))
                targetAimingTr.position = Vector3.Lerp(targetAimingTr.position, hitForward.point, Time.deltaTime * aimPointSpeed);
            else
                targetAimingTr.position = Vector3.Lerp(targetAimingTr.position, rayForward.GetPoint(1000), Time.deltaTime * aimPointSpeed);
        }
        void IRaycastHitItem.RaycastHitForItemInteract()
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskItem.value))
            {
                stateData.IsRayHitToItem = true;
                stateData.IsRayHitToInventoryLootBox = false;
                onShowTextByHitPoint?.Invoke("Take (F)");
            }
            else RaycastHitForLootBox();
        }
    
        public void OnActiveInventoryUIPanel(bool isActive)
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskLootBox.value))
            {
                InventoryLootBoxGameObject box = hitForward.collider.transform?.GetComponent<InventoryLootBoxGameObject>();
                box?.ActiveInventoryUIPanel(isActive);
            }
        }
        public bool OnRaycastHitForItem()
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskItem.value))
            {
                PickUpItems pickUpItem = hitForward.collider.transform?.GetComponent<PickUpItems>();
                if (PickUpWeapon(pickUpItem, hitForward)) return true;
                Interactable interact = hitForward.collider.transform?.GetComponent<Interactable>();
                interact?.Interaction();
            }
            return false;
        }
       
        bool IRaycastHitParcour.SetRayHitParcour(out RaycastHit hitForward, out RaycastHit hitDown)
        {
            bool isHitForward = GetRayForwardFromCharacter(pointRayCharacterTr, offsetPointRayFor);
            bool isHitDown = GetRayDownFromCharacter(this.hitForward, isHitForward);
            stateData.IsRayHitToObstacle = isHitDown;
            if (isHitDown)
            {
                hitForward = this.hitForward;
                hitDown = this.hitDown;
            }
            else
            {
                hitForward = default;
                hitDown = default;
            }
            return isHitDown;
        }


        private void RaycastHitForLootBox()
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskLootBox.value))
            {
                stateData.IsRayHitToItem = false;
                stateData.IsRayHitToInventoryLootBox = true;
                onShowTextByHitPoint?.Invoke("Search (F)");
            }
            else
            {
                stateData.IsRayHitToItem = false;
                stateData.IsRayHitToInventoryLootBox = false;
                onShowTextByHitPoint?.Invoke(" ");
            }
        }
        private bool PickUpWeapon(PickUpItems pickUpItem, RaycastHit hit)
        {
            if (pickUpItem.IsWeapon() && onSetParentByWeapon!=null)
                return onSetParentByWeapon.Invoke(hit.collider.gameObject);
            else return false;
        }
        private bool GetRayForwardFromCharacter(Transform charTrans, Vector3 offset)
        {
            rayForward = new Ray(charTrans.position + offset, charTrans.forward);
            return Physics.Raycast(rayForward, out hitForward, maxRayForwardParcoure, climbLayerMask);
        }
        private bool GetRayDownFromCharacter(RaycastHit hit, bool isHitForward)
        {
            if (!isHitForward) return false;
            rayDown = new Ray(hit.point + (Vector3.up * maxRayHeightParcoure), Vector3.down);
            return Physics.Raycast(rayDown, out hitDown, maxRayHeightParcoure, climbLayerMask);
        }
        private Ray GetRayForwardFromCamera()
        {
            return new Ray(pointRayCameraTr.position, pointRayCameraTr.forward);
        }
      
    }
}


