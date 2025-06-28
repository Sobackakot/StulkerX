using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using Character.InputEvents;
using StateData.Character;
using UnityEngine;

public class IdleBehaviour : BehaviourCharBase
{
    public IdleBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IIdleBehaviour>(this);
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
    }
    public override void FixedUpdateBeh()
    {
        Idling();
    }
    public override void Idling()
    {
        animator.MoveAnimation(0, Vector3.zero);
    }
}
