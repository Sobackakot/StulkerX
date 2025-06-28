using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using Character.InputEvents;
using UnityEngine;
public class MoveBehaviour : BehaviourCharBase
{
    public MoveBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<IMoveBehaviour>(this);
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
