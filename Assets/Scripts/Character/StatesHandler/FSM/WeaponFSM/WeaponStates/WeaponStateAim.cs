using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using State;
using State.Character.Weapon;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateAim : WeaponStateBase
{
    public WeaponStateAim(

        IStateMachine<WeaponStateType, IWeaponState> weaponHandler, 
        CharacterStateContext stateData, 
        IBehaviourHandler behaviourHandler) : base(weaponHandler, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler.Get<IAimWeaponBehaviour>(),
            behaviourHandler.Get<IFireWeaponBehaviour>() 
        };
    }

    public override void EnterState()
    {
        AddTransition();
        foreach (var behaviour in activeBehaviours)
        {
            behaviour?.EnableBeh(); 
        }
            
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
        var type = WeaponStateType.Aim; 
        weaponFSM.AddTransition(type, () => !stateData.isAim ? WeaponStateType.Default : type);
    }
    
}
