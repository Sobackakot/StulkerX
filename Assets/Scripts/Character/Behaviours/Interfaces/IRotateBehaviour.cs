using UnityEngine;

namespace Behaviour.Character
{
    public interface IRotateBehaviour : IUnitBehaviour
    {
        void RotationBehaviour(float angleRotate, Vector3 direction);
    }
}