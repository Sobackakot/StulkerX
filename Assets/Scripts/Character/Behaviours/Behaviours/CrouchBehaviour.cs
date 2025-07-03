using UnityEngine;
public class CrouchBehaviour : MoveBehaviour
{
    public CrouchBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    {
        animator?.CrouchAnimation(character.contextStates.IsCrouch);
    }
    public override void DisableBeh()
    {
        animator?.CrouchAnimation(character.contextStates.IsCrouch);
    }
    public override void UpdateBeh()
    { 
    }
    public override void LateUpdateBeh()
    {
    }
    public override void FixedUpdateBeh()
    {
        float speedMove = character?.inputAxis.z < 0 ? character.speedWalkBack : character.speedWalkForward;
        Vector3 newDirection = character.newDirection;
        CrouchingBehaviour(speedMove, newDirection); 
    }
 
    public override void CrouchingBehaviour(float speed, Vector3 direction) 
    {
        MovingBehaviour(speed, direction); 
    }  
}
