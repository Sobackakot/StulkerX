using UnityEngine;
public class SprintBehaviour : MoveBehaviour
{ 
    public SprintBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
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
        speedMove = character.contextStates?.InputAxis.z < 0 ? character.speedRunBack : character.speedSprint;
        Vector3 newDirection = character.newDirection;
        SprintingBehaviour(speedMove, newDirection);
       
    }
 
    public override void SprintingBehaviour(float speed, Vector3 direction) 
    {
        MovingBehaviour(speed, direction);
        animator?.MoveAnimation(animator.speedSprint, character.contextStates.InputAxis); 
    }

}
