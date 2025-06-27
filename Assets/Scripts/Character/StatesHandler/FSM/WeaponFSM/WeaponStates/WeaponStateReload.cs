using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using State.Character.Weapon;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateReload : WeaponStateBase
{
    public WeaponStateReload(

        IStateMachine<WeaponStateType, IWeaponState> weaponHandler, 
        CharacterStateContext stateData, 
        IBehaviourHandler behaviourHandler) : base(weaponHandler, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
             behaviourHandler.Get<IReloadWeaponBehaviour>() 
        };
    }

    public override void EnterState()
    {
        AddTransition();
        foreach (var behaviour in activeBehaviours)
            behaviour?.EnableBeh();
        Debug.Log("reload state  enter ");
    }
    public override void ExitState()
    {
        foreach (var behaviour in activeBehaviours)
            behaviour?.DisableBeh();
        Debug.Log("reload state exit");
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
        weaponFSM.AddTransition(type, () => !stateData.isReloadingState ? WeaponStateType.Default : type);
    }
}
