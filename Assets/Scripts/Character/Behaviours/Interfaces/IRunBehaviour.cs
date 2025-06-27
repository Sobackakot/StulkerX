using UnityEngine;

namespace Behaviour.Character
{
    public interface IRunBehaviour : IUnitBehaviour
    {
        void RunningBehaviour(float speed, Vector3 direction);
    }
}