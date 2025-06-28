using Behaviour.Character;
using Behaviour.Character.Base;
using Behaviour.Handler;
using StateData.Character;
using Character.InputEvents;
public class LeanBehaviour : BehaviourCharBase
{
    public LeanBehaviour(

        CharacterInspector character,
        CharacterAnimatorInspector animator,
        CharacterStateContext stateData,
        IInputEvents inputEvent,
        IBehaviourHandler behaviourHandler) : base(character, animator, stateData, inputEvent, behaviourHandler)
    {
        behaviourHandler.Register<ILeanBehaviour>(this);
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
    public override void LeaningLeftBehaviour() { }
    public override void LeaningRightBehaviour() { }
}
