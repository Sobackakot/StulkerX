using Character.Context;

public class CharacterAnimatorMain  
{ 
    public CharacterAnimatorMain(CharacterAnimatorInspector characterAnimator,
        IContextStates stateData, CharacterIK characterIK)
    {
        this.characterAnimator = characterAnimator; 
        this.contextStates = stateData;
        this.characterIK = characterIK;
    }
    private IContextStates contextStates;
    private CharacterAnimatorInspector characterAnimator; 
    private CharacterIK characterIK; 
    public void FixedTick()
    { 
        characterIK.WeightIKWeapon(contextStates.IsReloadingState, contextStates.IsEquippingState);
        if (!contextStates.IsReloadingState || !contextStates.IsEquippingState)
        {
            characterIK.BodyLoockTargetIK(contextStates.IsIdle, contextStates.IsAim);
            characterIK.WeaponParentIK(contextStates.IsReadyForBattle, contextStates.IsEquippingState);
            characterIK.AimWeaponParentIK(contextStates.IsAim, contextStates.IsReloadingState);
            characterIK.EquipWeaponParentIK(contextStates.IsReadyForBattle, contextStates.IsHasWeapon);
        }
    }
    public void Tick()
    {
        characterAnimator.SwitchAnimationTurn(contextStates.CurrentAngle);
        characterAnimator.TurnAnimation(contextStates.InputAxisCamera);
    }  
    public void LateTick()
    {
       
    }
  

}
