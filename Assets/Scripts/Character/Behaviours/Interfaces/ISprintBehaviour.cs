using UnityEngine;

namespace Behaviour.Character
{
    public interface ISprintBehaviour : IUnitBehaviour
    {
        void SprintingBehaviour(float speed, Vector3 direction); 
    }
}