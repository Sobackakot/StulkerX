using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using Character.Context;
using State.Character.Weapon;
using State.CoreFSM;
using System.Collections.Generic;

public class WeaponStateReload : WeaponStateBase
{
    public WeaponStateReload(

        IStateMachine<WeaponStateType, IWeaponState> weaponHandler,
        IContextStates stateData, 
        IBehaviourHandler behaviourHandler) : base(weaponHandler, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
             behaviourHandler?.Get<IReloadWeaponBehaviour>() 
        };
    }

    public override void EnterState()
    {
        AddTransition();
        foreach (var behaviour in activeBehaviours)
            behaviour?.EnableBeh(); 
    }
    public override void ExitState()
    {
        foreach (var behaviour in activeBehaviours)
            behaviour?.DisableBeh(); 
    }
    public override void UpdateState()
    {
        foreach (var behaviour in activeBehaviours)
            behaviour?.UpdateBeh();
    }
    public override void LateUpdateState()
    {
        foreach (var behaviour in activeBehaviours)
            behaviour?.LateUpdateBeh();
    }
    public override void FixedUpdateState()
    {
        foreach (var behaviour in activeBehaviours)
            behaviour?.FixedUpdateBeh();
    }
    private void AddTransition()
    { 
        var type = WeaponStateType.Reload; 
        weaponFSM?.AddTransition(type, () => !contextStates.IsReloadingState ? WeaponStateType.Default : type);
    }
}
