using UnityEngine;

public class CharacterAnimatorInspector : MonoBehaviour
{ 
    public Animator anim { get; private set; }

    [field: SerializeField] public float speedWalk { get; private set; } = 0.5f;
    [field: SerializeField] public float speedRun { get; private set; } = 0.8f;
    [field: SerializeField] public float speedSprint { get; private set; } = 1f;

    public Vector3 newAngleRotateCharacter { get; private set; }  
    public Vector3 inputAxis { get; private set; }  
    private float angleTurn = 45f;
    private float angleMaxTurn = 90f;
    private float switchAngleTurn;  
    public int runningLayer { get; private set; }

    private int pickUpItemLayer;
    private int reloadWeaponLayer;
    private int equipWeaponLayer;
     
    private void Awake()
    {     
        anim = GetComponent<Animator>();  
        pickUpItemLayer = anim.GetLayerIndex("PickUpItem_Layer");
        reloadWeaponLayer = anim.GetLayerIndex("ReloadWeapon_Layer");
        equipWeaponLayer = anim.GetLayerIndex("Take_Weapon_Layer");
        
        
    }
  
    private void OnEnable()
    { 
        //state.Move.OnJumping += InputCharacter_OnJump;  
        //state.Item.OnPickUpItemAnim += CharacterState_OnPickUpItem;
        //state.Weapon.OnEquipAnim += CharacterState_OnWeaponEquip; 
        //parkour.onAnimParkour += CharacterState_OnAnimParcoure;
        //state.Weapon.OnReloadWeapon += CharacterState_OnReloadWeeapon;
    }
    private void OnDisable()
    { 
        //state.Move.OnJumping -= InputCharacter_OnJump;  
        //state.Item.OnPickUpItemAnim -= CharacterState_OnPickUpItem;
        //state.Weapon.OnEquipAnim -= CharacterState_OnWeaponEquip;
        //parkour.onAnimParkour -= CharacterState_OnAnimParcoure;
        //state.Weapon.OnReloadWeapon -= CharacterState_OnReloadWeeapon;
    } 

    public void MoveAnimation(float speedAnimation, Vector3 inputAxis)
    { 
        if (inputAxis.sqrMagnitude > 0.2f)
        {
            anim.SetFloat("X", inputAxis.x * speedAnimation, 0.2f, Time.smoothDeltaTime);
            anim.SetFloat("Y", inputAxis.z * speedAnimation, 0.2f, Time.smoothDeltaTime);
        }
        else
        { 
            anim.SetFloat("Y", 0, 0.2f, Time.smoothDeltaTime);
            anim.SetFloat("X", 0, 0.2f, Time.smoothDeltaTime);
        }
    }
    public void CrouchAnimation(bool isCrouching)
    {
        anim.SetBool("isCrouching", isCrouching);
    }
    public void StartingRunning()
    {
        runningLayer = anim.GetLayerIndex("Running_Layer");
        anim.SetLayerWeight(runningLayer, 1);
        anim.SetTrigger("StartingRun");
    }
    public void StoppingRunning()
    {
        runningLayer = anim.GetLayerIndex("Running_Layer");
        anim.SetLayerWeight(runningLayer, 1);
        anim.SetTrigger("StoppingRun");
    }
   
    public void AimingAnimation(bool isAiming)
    {
        anim.SetBool("isAiming", isAiming);
    }

    public void TurnAnimation(Vector3 input, bool isRotate, bool isLimitAngle)
    {
        
        if (isRotate && isLimitAngle && Mathf.Abs(input.x) > 0.1f)
        {
            float currentDeltaMouse = anim.GetFloat("DeltaMouse");
            float smoothDeltaMouse = Mathf.Lerp(currentDeltaMouse, input.x * switchAngleTurn, 0.1f);
            anim.SetFloat("DeltaMouse", smoothDeltaMouse, 0.1f, Time.smoothDeltaTime); 
        } else anim.SetFloat("DeltaMouse", 0, 0.1f, Time.smoothDeltaTime); 
    } 
 
    public void SwitchAnimationTurn(float angle,bool isRotate)
    {
        if (isRotate)
            switchAngleTurn = angle >= 80 ? angleMaxTurn : angleTurn; 
    } 
  
    public void InputCharacter_OnJump()
    {
        anim.SetTrigger("isJumping"); 
    }
    public void ReloadWeeaponAnimation()
    {
        anim.SetLayerWeight(reloadWeaponLayer, 1);
        anim.SetTrigger("ReloadWeapon_Trigger");
    }

    public void PickUpItemAnimation()
    {
        anim.SetLayerWeight(pickUpItemLayer, 1);
        anim.SetTrigger("PickUpItem_Trigger"); 
    }
    public void ReadyForBattleAnim(bool isReadyForBattle)
    {
        anim.SetBool("isReadyForBattle", isReadyForBattle); 
    }
    public void EquipWeaponAnimation(bool isEquipWeapon)
    {
        anim.SetLayerWeight(equipWeaponLayer, 1); 
        if (isEquipWeapon)
            anim.SetTrigger("EquipWeapon");
        else  anim.SetTrigger("UnquipWeapon");
    }
    public void CharacterState_OnAnimParcoure(string nameAnim)
    {
        anim.CrossFade(nameAnim, 0.2f);
    }
   
}
