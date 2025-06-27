using UnityEngine;

namespace NPC.Behaviour
{
    public interface IRotateNPC : IBehaviourHandler
    {
        void Rotating(Quaternion targetRotation);
    }
}

