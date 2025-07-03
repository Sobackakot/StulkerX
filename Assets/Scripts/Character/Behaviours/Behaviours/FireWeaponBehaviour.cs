using Behaviour.Character.Base;
public class FireWeaponBehaviour : BehaviourCharBase
{
    public FireWeaponBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    { 
    }
    public override void DisableBeh()
    { 
    }
    public override void UpdateBeh()
    {
    }
    public override void LateUpdateBeh()
    {
        FiringWeapon();
    }
    public override void FixedUpdateBeh()
    {
    }

    public override void FiringWeapon() 
    {
        character?.weaponEfect?.LateTick();
    } 
}
