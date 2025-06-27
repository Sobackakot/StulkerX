using UnityEngine;

namespace NPC.Behaviour
{
    public interface IMoveNPC : IBehaviourHandler
    {
        void Moving(Vector3 targetMove);
    } 
}
