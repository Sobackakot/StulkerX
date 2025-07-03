using UnityEngine;
public class WalkBehaviour : MoveBehaviour
{ 
    private float speedMove;

    public WalkBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
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
        speedMove = character.contextStates?.InputAxis.z < 0 ? character.speedWalkBack : character.speedWalkForward;
        Vector3 newDirection = character.newDirection;
        WalkingBehaviour(speedMove, newDirection); 
    } 
    public override void WalkingBehaviour(float speed, Vector3 direction) 
    {
        MovingBehaviour(speed, direction);
        animator?.MoveAnimation(animator.speedWalk, character.contextStates.InputAxis); 
    }
}
