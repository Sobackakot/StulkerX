using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using Character.Context;
using State.Character.Move;
using State.CoreFSM;
using System.Collections.Generic;

public class MoveStateIdle : MoveStateBase
{
    public MoveStateIdle(

        IStateMachine<MoveStateType, IMoveState> moveFSM,
        IContextStates stateData, 
        IBehaviourHandler behaviourHandler) : base(moveFSM, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        { 
            behaviourHandler?.Get<IIdleBehaviour>(),
            behaviourHandler?.Get<IPickUpItemBehaviour>(),
            behaviourHandler?.Get<IRotateBehaviour>(),
            behaviourHandler?.Get<IJumpBehaviour>(),
            behaviourHandler?.Get<IParkourBehaviour>()
        };
    }

    public override void EnterState()
    { 
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
        var moveType = MoveStateType.Idle;  
        moveFSM?.AddTransition(moveType, () => contextStates.IsRun ? MoveStateType.Run : moveType);
        moveFSM?.AddTransition(moveType, () => contextStates.IsSprint ? MoveStateType.Sprint : moveType);
        moveFSM?.AddTransition(moveType, () => contextStates.IsCrouch ? MoveStateType.Crouch : moveType);
        moveFSM?.AddTransition(moveType, () => contextStates.IsWalk ? MoveStateType.Walk : moveType);
        moveFSM?.AddTransition(moveType, () => contextStates.IsAim ? MoveStateType.Aim : moveType);
    }  
}
