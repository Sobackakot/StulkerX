using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NPC.Behaviour
{
    public interface IFollowTargetNPC : IBehaviourHandler
    {
        void FollowTarget();
    }
}

