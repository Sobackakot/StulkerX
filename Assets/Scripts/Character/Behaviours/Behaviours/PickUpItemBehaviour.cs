using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using Character.InputEvents;
using StateData.Character; 
using UnityEngine;
public class PickUpItemBehaviour : BehaviourCharBase
{
    public PickUpItemBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler)
        : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler?.Register<IPickUpItemBehaviour>(this); 
    }

    public override void EnableBeh()
    {
        inputEvent.OnPickUpItem += PickUpItem; 
    }
    public override void DisableBeh()
    {
        inputEvent.OnPickUpItem -= PickUpItem;
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
    public override void PickUpItem() => animator.PickUpItemAnimation();
}
