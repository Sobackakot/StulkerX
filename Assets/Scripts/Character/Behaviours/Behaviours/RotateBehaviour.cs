using Behaviour.Character.Base;
using UnityEngine;
public class RotateBehaviour : BehaviourCharBase
{
    public RotateBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
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
        float angleRotate = character.angleRotate;
        Vector3 loockDirection = character.directionForward;
        RotationBehaviour(angleRotate, loockDirection);
    }
    public override void RotationBehaviour(float angleRotate, Vector3 direction)
    {
        Rigidbody rb = character?.rbCharacter;
        Quaternion rotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
        Quaternion newRotate = Quaternion.Lerp(rb.rotation, rotate, Time.fixedDeltaTime * angleRotate);
        rb?.MoveRotation(newRotate);
    }
}
