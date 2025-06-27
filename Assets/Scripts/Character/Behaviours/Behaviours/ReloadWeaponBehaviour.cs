using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
public class ReloadWeaponBehaviour : BehaviourCharBase
{
    public ReloadWeaponBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        CharacterInputEventHandler inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IReloadWeaponBehaviour>(this);
    }
    public override void EnableBeh()
    {
        inputEvent.OnReloadWeapon += ReloadingWeapon;
    }
    public override void DisableBeh()
    {
        inputEvent.OnReloadWeapon -= ReloadingWeapon;
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
 
