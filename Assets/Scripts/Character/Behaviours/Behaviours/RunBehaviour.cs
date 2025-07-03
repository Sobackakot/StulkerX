using UnityEngine;
public class RunBehaviour : MoveBehaviour
{ 
    
    public RunBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
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
        speedMove = character.contextStates?.InputAxis.z < 0 ? character.speedRunBack : character.speedRunForward;
        Vector3 newDirection = character.newDirection;
        RunningBehaviour(speedMove, newDirection); 
    }
    public override void RunningBehaviour(float speed, Vector3 direction)
    { 
        MovingBehaviour(speed, direction); 
        animator?.MoveAnimation(animator.speedRun, character.contextStates.InputAxis); 
    }
}
