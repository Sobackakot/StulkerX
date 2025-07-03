using Behaviour.Character;
using Behaviour.Handler;
using Character.InputEvents;
using StateData.Character;
using UnityEngine;
public class RunBehaviour : MoveBehaviour
{
    public RunBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<IRunBehaviour>(this);
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
        speedMove = stateData?.inputAxis.z < 0 ? character.speedRunBack : character.speedRunForward;
        Vector3 newDirection = character.newDirection;
        RunningBehaviour(speedMove, newDirection); 
    }
    public override void RunningBehaviour(float speed, Vector3 direction)
    { 
        MovingBehaviour(speed, direction); 
        animator?.MoveAnimation(animator.speedRun,stateData.inputAxis);
        Debug.Log("Running"); 
    }
}
