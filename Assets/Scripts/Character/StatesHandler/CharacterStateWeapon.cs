using State.Character;
using System;

public class CharacterStateWeapon 
{
    public bool isAim { get; private set; }
    public bool isFire { get; private set; } 
    public bool isReadyForBattle { get; private set; }    
    public bool isEquipping { get; private set; }
    public bool isPickUpWeapon { get; private set; }
    public bool isReload { get; private set; }      

     
    public void InputCharacter_OnEquipWeapon(EquipWeaponToggleEvent equipEvent)
    {
        //if (!isAim && !isReload && isPickUpWeapon)
        //{
        //    isReadyForBattle = !isReadyForBattle;
        //    OnEquipAnim?.Invoke(isReadyForBattle); 
        //    OnReadyForBattleAnim?.Invoke();
        //}
    }
    public void InputCharacter_OnAim(AimInputEvent aimEvent)
    {
        if (isReadyForBattle && !isReload && !isEquipping)
            isAim = aimEvent.inputValue; 
    }
    public void InputCharacter_OnFire(FireInputEvent fireEvent)
    {
        if (isReadyForBattle && isAim)
            isFire = fireEvent.inputValue;
    }
    public void InputCharacter_OnReloadWeapon(ReloadWeaponEvent reloadEvent)
    {
        //if (!isAim && isReadyForBattle && !isReload)
        //{ 
        //    OnReloadWeapon?.Invoke();
        //}
    }
    public void SetReloadWeaponAnimationState(bool isReload)
    {
        //this.isReload = isReload; 
        //if (isReload)
        //    OnSetParentWeapon?.Invoke();
        //else
        //    OnResetParenWeapon?.Invoke();
    }
    public void SetAvailableWeapon(bool isPickUpWeapon)
    {
        this.isPickUpWeapon = isPickUpWeapon;
    }
    
    public void SetEquippWeaponAnimationState(bool isEquipping)
    {
        //this.isEquipping = isEquipping;
        //if (isEquipping)
        //    OnSetParentWeapon?.Invoke();
        //else
        //    OnResetParenWeapon?.Invoke();
    } 
}
