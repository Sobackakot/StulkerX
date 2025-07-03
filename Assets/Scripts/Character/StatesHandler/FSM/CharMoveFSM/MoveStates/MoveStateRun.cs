using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using State.Character.Move;
using State.CoreFSM;
using StateData.Character;
using System.Collections.Generic;

public class MoveStateRun : MoveStateBase
{
    public MoveStateRun(

        IStateMachine<MoveStateType, IMoveState> moveFSM,
        CharacterStateContext stateData, 
        IBehaviourHandler behaviourHandler) : base(moveFSM, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler?.Get<IRunBehaviour>(),
            behaviourHandler?.Get<IPickUpItemBehaviour>(),
            behaviourHandler?.Get<IRotateBehaviour>(),
            behaviourHandler?.Get<IJumpBehaviour>(),
            behaviourHandler?.Get<IParkourBehaviour>()
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
        var moveType = MoveStateType.Run;  
        moveFSM?.AddTransition(moveType, () => !stateData.IsRun ? MoveStateType.Idle : moveType);
        moveFSM?.AddTransition(moveType, () => stateData.IsWalk ? MoveStateType.Walk : moveType);
        moveFSM?.AddTransition(moveType, () => stateData.IsSprint ? MoveStateType.Sprint : moveType);
        moveFSM?.AddTransition(moveType, () => stateData.IsCrouch ? MoveStateType.Crouch : moveType); 
        moveFSM?.AddTransition(moveType, () => stateData.IsAim ? MoveStateType.Aim : moveType); 
    } 
}
