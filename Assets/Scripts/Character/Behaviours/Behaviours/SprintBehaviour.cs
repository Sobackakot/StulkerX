using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using UnityEngine;
using Character.InputEvents;
public class SprintBehaviour : BehaviourCharBase
{
    public SprintBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<ISprintBehaviour>(this);
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
        speedMove = stateData?.inputAxis.z < 0 ? character.speedRunBack : character.speedSprint;
        Vector3 newDirection = character.newDirection;
        SprintingBehaviour(speedMove, newDirection);
       
    }
 
    public override void SprintingBehaviour(float speed, Vector3 direction) 
    {
        base.MovingBehaviour(speed, direction);
        animator?.MoveAnimation(animator.speedSprint, stateData.inputAxis);
    } 

}
