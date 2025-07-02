using Behaviour.Character;
using Behaviour.Handler;
using Character.InputEvents;
using StateData.Character;
using UnityEngine;
public class SprintBehaviour : MoveBehaviour
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
        MovingBehaviour(speed, direction);
        animator?.MoveAnimation(animator.speedSprint, stateData.inputAxis);
        Debug.Log("sprint"); 
    }

}
