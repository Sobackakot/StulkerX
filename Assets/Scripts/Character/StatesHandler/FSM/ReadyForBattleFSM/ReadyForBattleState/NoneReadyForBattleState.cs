using Behaviour.Character;
using Behaviour;
using Behaviour.Handler;
using State;
using State.Character.Battle;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

public class NoneReadyForBattleState : ReadyForBattleStateBase
{
    public NoneReadyForBattleState(

        IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM, 
        CharacterStateContext stateData, 
        IBehaviourHandler behaviourHandler) : base(battleFSM, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler?.Get<IReadyForBattleBehaviour>(),
             behaviourHandler?.Get<IEquipWeaponBehaviour>()
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
        var type = ReadyStateType.None; 
        battleFSM?.AddTransition(type, () => stateData.isReadyForBattle ? ReadyStateType.Ready : type);
    }
}
