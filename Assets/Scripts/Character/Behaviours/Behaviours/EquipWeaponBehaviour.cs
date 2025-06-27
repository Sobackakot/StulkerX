using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using UnityEngine;
public class EquipWeaponBehaviour : BehaviourCharBase
{
    public EquipWeaponBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        CharacterInputEventHandler inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IEquipWeaponBehaviour>(this);
    } 
    public override void EnableBeh()
    {
        EquipingWeapon(stateData.isReadyForBattle);
    }
    public override void DisableBeh()
    {
        EquipingWeapon(stateData.isReadyForBattle);
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
        animator.EquipWeaponAnimation(isReady); 
    }
}
