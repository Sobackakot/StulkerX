using Behaviour.Character.Base;
using UnityEngine;
public class JumpBehaviour : BehaviourCharBase
{
    public JumpBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    {
        character.inputEvent.OnJump += JumpingBehaviour; 
    }
    public override void DisableBeh()
    {
        character.inputEvent.OnJump -= JumpingBehaviour; 
    }
    public override void UpdateBeh()
    { 
    }
    public override void LateUpdateBeh()
    {
    }
    public override void FixedUpdateBeh()
    { 
    }
    public override void JumpingBehaviour()
    { 
        character.rbCharacter.AddForce(Vector3.up * character.jumpForce, ForceMode.Impulse);
        animator.InputCharacter_OnJump();
    }
}
