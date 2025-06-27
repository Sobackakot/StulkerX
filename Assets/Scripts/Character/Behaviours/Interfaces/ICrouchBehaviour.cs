using UnityEngine;

namespace Behaviour.Character
{
    public interface ICrouchBehaviour : IUnitBehaviour
    {
        void CrouchingBehaviour(float speed, Vector3 direction); 
    }
}