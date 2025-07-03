using StateData.Character;
using UnityEngine;
using Window.UI;

public class CharacterAnimatorMain  
{ 
    public CharacterAnimatorMain(CharacterAnimatorInspector characterAnimator,
        CharacterStateContext stateData, CharacterIK characterIK)
    {
        this.characterAnimator = characterAnimator; 
        this.stateData = stateData;
        this.characterIK = characterIK;
    }
    private CharacterStateContext stateData;
    private CharacterAnimatorInspector characterAnimator; 
    private CharacterIK characterIK; 
    public void FixedTick()
    { 
        characterIK.WeightIKWeapon(stateData.IsReloadingState, stateData.IsEquippingState);
        if (!stateData.IsReloadingState || !stateData.IsEquippingState)
        {
            characterIK.BodyLoockTargetIK(stateData.IsIdle, stateData.IsAim);
            characterIK.WeaponParentIK(stateData.IsReadyForBattle, stateData.IsEquippingState);
            characterIK.AimWeaponParentIK(stateData.IsAim, stateData.IsReloadingState);
            characterIK.EquipWeaponParentIK(stateData.IsReadyForBattle, stateData.IsHasWeapon);
        }
    }
    public void Tick()
    {
        characterAnimator.SwitchAnimationTurn(stateData.currentAngle);
        characterAnimator.TurnAnimation(stateData.inputAxisCamera);
    }  
    public void LateTick()
    {
       
    }
  

}
