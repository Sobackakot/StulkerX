using Behaviour.Character;
using Behaviour;
using Behaviour.Handler;
using State;
using State.Character.Weapon;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

public class WeaponStateDefault : WeaponStateBase
{
    public WeaponStateDefault(

        IStateMachine<WeaponStateType, IWeaponState> weaponHandler, 
        CharacterStateContext stateData, 
        IBehaviourHandler behaviourHandler) : base(weaponHandler, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler?.Get<IReloadWeaponBehaviour>(),
            behaviourHandler?.Get<IAimWeaponBehaviour>() 
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
        var type = WeaponStateType.Default; 
        weaponFSM?.AddTransition(type, () => stateData.isAim ? WeaponStateType.Aim : type);
        weaponFSM?.AddTransition(type, () => stateData.isReloadingState ? WeaponStateType.Reload : type);
    }
}
