using Behaviour.Character.Base;
using UnityEngine;

public class IdleBehaviour : BehaviourCharBase
{
    public IdleBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
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
        Idling();
    }
    public override void Idling()
    {
        animator?.MoveAnimation(0, Vector3.zero);
    }
}
