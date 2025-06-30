using System;
using System.Collections.Generic;
using UnityEngine;

namespace State.CoreFSM
{
    public abstract class StateMachine<StateType,TInterface> : IStateMachine<StateType, TInterface>
    where StateType : Enum
    where TInterface : class
    {  
        private StateType currentStateType;
        public TInterface currentState;

        private readonly Dictionary<StateType, TInterface> states = new();
        //это словарь списков, где каждое состояние может иметь несколько правил перехода.
        private readonly Dictionary<StateType, List<Func<StateType>>> transitionRules = new();


        public virtual void SetFSM(StateType newState)
        { 
            if (!states.ContainsKey(newState)) return;  
            (currentState as IState)?.ExitState();
            currentState = states[newState];
            currentStateType = newState;
            (currentState as IState)?.EnterState();
        }
        public virtual void Transition()
        { 
            if (transitionRules.TryGetValue(currentStateType, out var rules))
            {
                foreach (var rule in rules)
                {
                    var next = rule.Invoke(); 
                    if (!EqualityComparer<StateType>.Default.Equals(next, currentStateType))
                    {
                        //Debug.Log($"[FSM] Transitioning from {currentStateType} to {next}");
                        SetFSM(next);
                        break;
                    }
                }
            }
        }
        public virtual void UpdateFSM() => (currentState as IState)?.UpdateState();
     
        public virtual void LateUpdateFSM() => (currentState as IState)?.LateUpdateState();
        public virtual void FixedUpdateFSM() => (currentState as IState)?.FixedUpdateState();
        // назначаем ключ и присваиваем новое значение.
        public virtual void RegisterFSM(StateType type, TInterface state)
        {
            states[type] = state; 
        }
        public virtual void AddTransition(StateType fromState, Func<StateType> transition)
        { 
            //инициализируем пустой список, только если он не существует.
            if (!transitionRules.ContainsKey(fromState))
            { 
                transitionRules[fromState] = new List<Func<StateType>>();
            } 
            //добавляем функцию перехода в этот список.
            transitionRules[fromState].Add(transition);
        }
    } 
}

