using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using Character.InputEvents;
using UnityEngine;
public class CrouchBehaviour : BehaviourCharBase
{
    public CrouchBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<ICrouchBehaviour>(this);
    }
    public override void EnableBeh()
    {
        animator.CrouchAnimation(stateData.isCrouch);
    }
    public override void DisableBeh()
    {
        animator.CrouchAnimation(stateData.isCrouch);
    }
    public override void UpdateBeh()
    { 
    }
    public override void LateUpdateBeh()
    {
    }
    public override void FixedUpdateBeh()
    {
        float speedMove = character.inputAxis.z < 0 ? character.speedWalkBack : character.speedWalkForward;
        Vector3 newDirection = character.newDirection;
        CrouchingBehaviour(speedMove, newDirection); 
    }
 
    public override void CrouchingBehaviour(float speed, Vector3 direction) 
    {
        base.MovingBehaviour(speed, direction); 
    }  
}
