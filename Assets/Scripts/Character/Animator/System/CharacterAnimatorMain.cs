
using State.Character;
using StateData.Character;

public class CharacterAnimatorMain  
{ 
    public CharacterAnimatorMain(CharacterAnimatorInspector characterAnimator,
        CharacterStateContext stateData, CharacterIK characterIK, CharacterStateBootstrap state)
    {
        this.characterAnimator = characterAnimator; 
        this.stateData = stateData;
        this.characterIK = characterIK;
        this.state = state;
    }
    private CharacterStateContext stateData;
    private CharacterAnimatorInspector characterAnimator; 
    private CharacterIK characterIK;

    private CharacterStateBootstrap state;
    public void Tick()
    {
        var camera = state.Camera;
        characterAnimator.SwitchAnimationTurn(camera.currentAngle, camera.isStopingRotate);
        characterAnimator.TurnAnimation(camera.inputAxis, camera.isStopingRotate, camera.isMaxAngle);

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
