using Behaviour.Character.Base;
public class ReloadWeaponBehaviour : BehaviourCharBase
{
    public ReloadWeaponBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    {
        if (character.inputEvent != null)
        {
            character.inputEvent.OnReloadWeapon += ReloadingWeapon;
        } 
    }
    public override void DisableBeh()
    {
        if (character.inputEvent != null)
        {
            character.inputEvent.OnReloadWeapon -= ReloadingWeapon;
        }
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
    public override void ReloadingWeapon()
    {
        animator.ReloadWeeaponAnimation();
    }
}
 
