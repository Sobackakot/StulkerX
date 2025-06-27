using UnityEngine;

namespace Behaviour.Character
{
    public interface IMoveBehaviour : IUnitBehaviour
    {
        void MovingBehaviour(float speed, Vector3 direction); 
    }
}