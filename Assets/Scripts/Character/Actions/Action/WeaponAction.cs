using Character.Actions;
using Character.Context;
using State.Character.Weapon;
using State.CoreFSM;

public class WeaponAction : CharacterAction
{
    public WeaponAction(IStateMachine<WeaponStateType, IWeaponState> fsm) : base(fsm) { }

    public override void Subscribe(IContext context)
    {
        if (context == null || fsm ==null) return;
        context.onExecuteWeaponTransition += fsm.TransitionFSM;
    }

    public override void Unsubscribe(IContext context)
    {
        if (context == null || fsm == null) return;
        context.onExecuteWeaponTransition -= fsm.TransitionFSM;
    }
}
