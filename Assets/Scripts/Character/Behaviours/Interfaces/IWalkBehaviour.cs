using UnityEngine;

namespace Behaviour.Character
{
    public interface IWalkBehaviour : IUnitBehaviour
    {
        void WalkingBehaviour(float speed, Vector3 direction); 
    }
}