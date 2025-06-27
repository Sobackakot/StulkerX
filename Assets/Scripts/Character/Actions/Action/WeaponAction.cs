using Character.Actions;
using Character.Context;
using State.Character.Weapon;
using State.CoreFSM;

public class WeaponAction : CharacterAction
{
    public WeaponAction(IStateMachine<WeaponStateType, IWeaponState> fsm) : base(fsm) { }

    public override void Subscribe(IContext context)
    {
        context.onExecuteWeaponTransition += fsm.Transition;
    }

    public override void Unsubscribe(IContext context)
    {
        context.onExecuteWeaponTransition -= fsm.Transition;
    }
}
