using Behaviour.Character.Base;
public class PickUpItemBehaviour : BehaviourCharBase
{
    public PickUpItemBehaviour(CharacterInspector character, CharacterAnimatorInspector animator) : base(character, animator)
    {
    }

    public override void EnableBeh()
    {
        character.inputEvent.OnPickUpItem += PickUpItem; 
    }
    public override void DisableBeh()
    {
        character.inputEvent.OnPickUpItem -= PickUpItem;
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
