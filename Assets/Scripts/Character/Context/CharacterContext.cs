using StateData;
using System;

namespace Character.Context
{
    public abstract class CharacterContext : IContextEvents
    { 
        public abstract event Action onExecuteMoveTransition;
        public abstract event Action onExecuteReadyTransition;
        public abstract event Action onExecuteWeaponTransition;
        public abstract event Action<bool> onStoppingExecute;
    }
}