using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using Character.InputEvents;
using UnityEngine;
public class JumpBehaviour : BehaviourCharBase
{
    public JumpBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<IJumpBehaviour>(this);
    }
    public override void EnableBeh()
    {
        inputEvent.OnJump += JumpingBehaviour; 
    }
    public override void DisableBeh()
    {
        inputEvent.OnJump -= JumpingBehaviour; 
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
