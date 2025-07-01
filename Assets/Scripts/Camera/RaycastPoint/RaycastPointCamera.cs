using Character.InputEvents;
using Inventory;
using StateData.Character;
using UnityEngine;
using Zenject;


namespace MainCamera.Raycast
{
    public class RaycastPointCamera : MonoBehaviour, IRaycastHitFPS, IRaycastHitItem, IRaycastHitParcour, IRaycastHitLootBox
    {
        private Transform targetAiming;
        [SerializeField] private float aimPointSpeed = 45f;
        [SerializeField] private Vector3 offsetPointRayFor = new Vector3(0, 0.25f, 0);

        private Transform pointRay;
        private Transform charTransPointRay;
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


        private WeaponHandle weapon;
        private CharacterInspector charact;
        private WindowUI windowUI; 

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
            pointRay = GetComponent<Transform>();
            weapon = FindObjectOfType<WeaponHandle>();
            windowUI = FindObjectOfType<WindowUI>();
            charact = FindObjectOfType<CharacterInspector>();
            charTransPointRay = charact.GetComponent<Transform>();
            targetAiming = GetComponentInChildren<TargetRayPointAim>()?.transform; 
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
                targetAiming.position = Vector3.Lerp(targetAiming.position, hitForward.point, Time.deltaTime * aimPointSpeed);
            else
                targetAiming.position = Vector3.Lerp(targetAiming.position, rayForward.GetPoint(1000), Time.deltaTime * aimPointSpeed);
        }
        void IRaycastHitItem.RaycastHitForItemInteract()
        {
            rayForward = GetRayForwardFromCamera();
            if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskItem.value))
            {
                stateData.isRayHitToItem = true;
                stateData.isRayHitToInventoryLootBox = false;
                windowUI?.SetInteractText("Take (F)");
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
            bool isHitForward = GetRayForwardFromCharacter(charTransPointRay, offsetPointRayFor);
            bool isHitDown = GetRayDownFromCharacter(this.hitForward, isHitForward);
            stateData.isRayHitToObstacle = isHitDown;
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
                stateData.isRayHitToItem = false;
                stateData.isRayHitToInventoryLootBox = true;
                windowUI?.SetInteractText("Search (F)");
            }
            else
            {
                stateData.isRayHitToItem = false;
                stateData.isRayHitToInventoryLootBox = false;
                windowUI?.SetInteractText(" ");
            }
        }
        private bool PickUpWeapon(PickUpItems pickUpItem, RaycastHit hit)
        {
            if (pickUpItem.IsWeapon())
                return weapon.SetWeapon(hit.collider.gameObject);
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
            return new Ray(pointRay.position, pointRay.forward);
        }
      
    }
}


