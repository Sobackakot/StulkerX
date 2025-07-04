using State.Character.Move;
using System;

namespace Character.Context
{
    public interface IContextEvents
    { 
        event Action onExecuteMoveTransition;
        event Action onExecuteReadyTransition;
        event Action onExecuteWeaponTransition;
        event Action<MoveStateType> OnMovementStateChanged;
    }
}

