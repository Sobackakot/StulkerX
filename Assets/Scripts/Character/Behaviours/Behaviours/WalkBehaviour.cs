using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using UnityEngine;
public class WalkBehaviour : BehaviourCharBase
{
    public WalkBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        CharacterInputEventHandler inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IWalkBehaviour>(this);
    }
    private float speedMove;
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
        speedMove = stateData.inputAxis.z < 0 ? character.speedWalkBack : character.speedWalkForward;
        Vector3 newDirection = character.newDirection;
        WalkingBehaviour(speedMove, newDirection); 
    } 
    public override void WalkingBehaviour(float speed, Vector3 direction) 
    {
        base.MovingBehaviour(speed, direction);
        animator.MoveAnimation(animator.speedWalk, stateData.inputAxis);
    } 
}
