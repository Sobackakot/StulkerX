using System;


namespace State.CoreFSM
{
    public interface IStateMachine<StateType, TInterface> : IFSM
    where StateType : Enum
    where TInterface : class  
    {
        void AddTransition(StateType fromState, Func<StateType> transition);
        void RegisterFSM(StateType type, TInterface state);
        void SetFSM(StateType newState);
        void Transition();
        void UpdateFSM();
        void LateUpdateFSM();
        void FixedUpdateFSM();
    }
}

