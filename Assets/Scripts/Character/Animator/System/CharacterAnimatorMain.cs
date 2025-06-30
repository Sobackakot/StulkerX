
using State.Character;
using StateData.Character;

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

    public void Tick()
    {
        characterAnimator.SwitchAnimationTurn(stateData.currentAngle);
        characterAnimator.TurnAnimation(stateData.inputAxisCamera);
    }  
    public void LateTick()
    {
       
    }
    public void FixedTick()
    {
        characterIK.WeightIKWeapon(stateData.isReloadingState, stateData.isEquippingState);
        if (!stateData.isReloadingState || !stateData.isEquippingState)
        {
            characterIK.BodyLoockTargetIK(stateData.isIdle, stateData.isAim);
            characterIK.WeaponParentIK(stateData.isReadyForBattle, stateData.isEquippingState);
            characterIK.AimWeaponParentIK(stateData.isAim, stateData.isReloadingState);
            characterIK.EquipWeaponParentIK(stateData.isReadyForBattle, stateData.isHasWeapon);
        }
    }

}
