using Behaviour.Character.Base;
public class EquipWeaponBehaviour : BehaviourCharBase
{
    public EquipWeaponBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    {
        EquipingWeapon(character.contextStates.IsReadyForBattle);
    }
    public override void DisableBeh()
    {
        EquipingWeapon(character.contextStates.IsReadyForBattle);
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
    
    public override void EquipingWeapon(bool isReady)
    {
        animator?.EquipWeaponAnimation(isReady); 
    }
}
