using Behaviour.Character.Base;
using UnityEngine;
public class MoveBehaviour : BehaviourCharBase
{
    public MoveBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
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
    }
 
    public override void MovingBehaviour(float speed, Vector3 direction)
    {
        base.MovingBehaviour(speed, direction); 
    }  
 
}
