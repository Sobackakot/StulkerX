using Behaviour.Character.Base;
public class AimWeaponBehaviour : BehaviourCharBase
{
    public AimWeaponBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator) : base(character, animator) { }
    public override void EnableBeh()
    { 
        AimingWeapon(character.contextStates.IsAim);
    }
    public override void DisableBeh()
    {
        AimingWeapon(character.contextStates.IsAim);
    }
    public override void UpdateBeh()
    {
    }
    public override void LateUpdateBeh()
    {
       
    }
    public override void FixedUpdateBeh()
    {
    } 
    public override void AimingWeapon(bool isAim) 
    {
        animator?.AimingAnimation(isAim); 
    }
}
