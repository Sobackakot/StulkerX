using System;

namespace Character.Context
{
    public interface IContext
    { 
        event Action onExecuteMoveTransition;
        event Action onExecuteReadyTransition;
        event Action onExecuteWeaponTransition;
        event Action<bool> onStoppingExecute;
    }
}

