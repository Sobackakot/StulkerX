using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using Character.InputEvents;    
using UnityEngine;
public class FireWeaponBehaviour : BehaviourCharBase
{
    public FireWeaponBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<IFireWeaponBehaviour>(this);
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
