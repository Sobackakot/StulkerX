using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using Character.Context;
using State.Character.Battle;
using State.CoreFSM;
using System.Collections.Generic;

public class ReadyForBattleState : ReadyForBattleStateBase
{
    public ReadyForBattleState(

        IStateMachine<ReadyStateType, IReadyForBattleState> battleFSM,
        IContextStates stateData, 
        IBehaviourHandler behaviourHandler) : base(battleFSM, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler?.Get<IReadyForBattleBehaviour>(),
             behaviourHandler?.Get < IEquipWeaponBehaviour >() 
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
        var type = ReadyStateType.Ready; 
        battleFSM?.AddTransition(type, () => !contextStates.IsReadyForBattle ? ReadyStateType.None : type);
    }
    
}
