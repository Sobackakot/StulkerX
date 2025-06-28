
using State.Character;
using UnityEngine;
using Zenject;
using Inventory_;
using StateData.Character;
using Character.InputEvents;

public class RaycastCamera : MonoBehaviour
{
    private Transform targetAiming;
    [SerializeField] private float  aimPointSpeed = 45f;
    [SerializeField] private Vector3 offsetPointRayFor = new Vector3(0,0.25f,0);
    [SerializeField] private Vector3 offsetPointRayDown = new Vector3(0,6,0.6f);

    private Transform pointRay;
    private Transform charTransPointRay; 
    [SerializeField] private float maxRayInteract = 4f;
    [SerializeField] private float maxRayForwardParcoure = 0.8f;
    [SerializeField] private float maxRayHeightParcoure = 6f;
    private float maxRayAiming = 1000f;
     
    public LayerMask layerMaskBox;
    public LayerMask layerMaskTake;
    public LayerMask ignorLayerMask;
    public LayerMask climbLayerMask;

    private Ray rayForward;
    private Ray rayDown;
    private RaycastHit hitForward;
    private RaycastHit hitDown;
     
    
    private WeaponHandle weapon;
    private CharacterInspector charact;
    private WindowUI windowUI;
    private bool isActiveInventoryBox;

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
        if(inputEvent!= null)
        {
            inputEvent.OnActiveInventoryLootBox += InputCharacter_OnSearcheInventoryLootBox;
            inputEvent.OnHasWeapon += InputCharacter_IsRaycastHitItem;
        } 
    }
    private void OnDisable()
    {
        if (inputEvent != null)
        {
            inputEvent.OnActiveInventoryLootBox -= InputCharacter_OnSearcheInventoryLootBox;
            inputEvent.OnHasWeapon -= InputCharacter_IsRaycastHitItem;
        }
    }
    public void Shooting(bool isLeftButtonDown)
    {
        if (isLeftButtonDown )
        {
           
            rayForward = GetRayForward();
            if (Physics.Raycast(rayForward, out hitForward, maxRayAiming))
            {
                hitForward.rigidbody?.AddForce(-hitForward.normal * 1f, ForceMode.Impulse);
            }
        }  
    }
    public void GetPointRayAim()
    { 
        rayForward = GetRayForward();
        if (Physics.Raycast(rayForward, out hitForward, maxRayAiming, ~ignorLayerMask))
            targetAiming.position = Vector3.Lerp(targetAiming.position, hitForward.point, Time.deltaTime * aimPointSpeed);
        else
            targetAiming.position = Vector3.Lerp(targetAiming.position, rayForward.GetPoint(1000), Time.deltaTime * aimPointSpeed);
    }
    public void RayHitTakeItemInteract()
    {
        rayForward = GetRayForward(); 
        if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskTake.value))
        {
            stateData.isRayHitToItem = true;
            stateData.isRayHitToInventoryLootBox = false;
            windowUI.SetInteractText("Take (F)"); 
        }
        else RayHitInventoryBoxInteract();
    }
    private void RayHitInventoryBoxInteract()
    {
        rayForward = GetRayForward();
        if (Physics.Raycast(rayForward, out hitForward, maxRayInteract, layerMaskBox.value))
        {
            stateData.isRayHitToItem = false;
            stateData.isRayHitToInventoryLootBox = true;
            windowUI.SetInteractText("Search (F)");
        }
        else
        {
            stateData.isRayHitToItem = false;
            stateData.isRayHitToInventoryLootBox = false;
            windowUI.SetInteractText(" ");
        }
            
    } 
    public void InputCharacter_OnSearcheInventoryLootBox(bool isActive)
    {
        rayForward = GetRayForward();
        isActiveInventoryBox = isActive;
        if (Physics.Raycast(rayForward, out hitForward, maxRayInteract))
        {
            InventoryLootBoxGameObject box = hitForward.collider.transform.GetComponent<InventoryLootBoxGameObject>(); 
            box?.OnActiveInventoryLootBox(isActiveInventoryBox); 
        } 
    }
    public bool InputCharacter_IsRaycastHitItem()
    {
        rayForward = GetRayForward();
        if (Physics.Raycast(rayForward, out hitForward, maxRayInteract))
        {
            PickUpItems pickUpItem = hitForward.collider.transform.GetComponent<PickUpItems>(); 
            if (PickUpWeapon(pickUpItem, hitForward)) return true; 
            Interactable interact = hitForward.collider.transform.GetComponent<Interactable>();
            interact?.Interaction();
        }
        return false; 
    }
    public bool PickUpWeapon(PickUpItems pickUpItem, RaycastHit hit)
    {
        if (pickUpItem.IsWeapon())
            return weapon.SetWeapon(hit.collider.gameObject);
        else return false;
    } 
    public bool SetRayHitParcour(out RaycastHit hitForward,out RaycastHit hitDown)
    {
        bool isHitForward = RayForward(charTransPointRay, offsetPointRayFor);
        bool isHitDown = RayDown(this.hitForward, isHitForward);
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
   
    private bool RayForward(Transform charTrans, Vector3 offset)
    {  
        rayForward = new Ray(charTrans.position + offset, charTrans.forward);
        return Physics.Raycast(rayForward, out hitForward, maxRayForwardParcoure, climbLayerMask);  
    }
    private bool RayDown(RaycastHit hit, bool isHitForward)
    {
        if (!isHitForward) return false; 
        rayDown = new Ray(hit.point + (Vector3.up * maxRayHeightParcoure), Vector3.down);
        return Physics.Raycast(rayDown, out hitDown, maxRayHeightParcoure, climbLayerMask);  
    } 
    private Ray GetRayForward()
    {
        return new Ray(pointRay.position, pointRay.forward);
    }

}

