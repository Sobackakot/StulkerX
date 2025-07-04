using Behaviour;
using Behaviour.Character;
using Behaviour.Handler;
using Character.Context;
using State.Character.Move;
using State.CoreFSM;
using System.Collections.Generic;

public class MoveStateAim : MoveStateBase
{
    public MoveStateAim(

        IStateMachine<MoveStateType, IMoveState> moveFSM,
        IContextStates stateData, 
        IBehaviourHandler behaviourHandler) : base(moveFSM, stateData, behaviourHandler)
    {
        activeBehaviours = new List<IUnitBehaviour>
        {
            behaviourHandler?.Get<IAimWeaponBehaviour>(),
            behaviourHandler?.Get<ICrouchBehaviour >(),
            behaviourHandler?.Get<IWalkBehaviour >(),
            behaviourHandler?.Get<IRotateBehaviour >()
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
        var typeAim = MoveStateType.Aim;
        moveFSM?.AddTransition(typeAim, () => !contextStates.IsAim ? MoveStateType.Run : typeAim);
        moveFSM?.AddTransition(typeAim, () => contextStates.IsCrouch ? MoveStateType.Crouch : typeAim);
    } 
}
