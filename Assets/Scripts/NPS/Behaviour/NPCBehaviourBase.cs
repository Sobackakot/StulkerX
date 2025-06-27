
using UnityEngine;

namespace NPC.Behaviour 
{
    public abstract class NPCBehaviourBase:
        IIdleNPC,
        IMoveNPC,
        IRotateNPC,
        IRandomMoveNPC,
        IRandomRotateNPC,
        IFollowTargetNPC,
        ILoockTargetNPC 
    {
        public NPCBehaviourBase(NPC_AI_Base npc)
        {
            this.npc = npc;
        }
        public NPC_AI_Base npc { get; private set; }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
        public virtual void IdleState() { }
        public virtual void Moving(Vector3 targetMove) { }
        public virtual void Rotating(Quaternion targetRotation) { }
        public virtual void RandomMove() { }
        public virtual void RandomRotate() { }
        public virtual void FollowTarget() { }
        public virtual void LoockTarget() { }
    } 

}


