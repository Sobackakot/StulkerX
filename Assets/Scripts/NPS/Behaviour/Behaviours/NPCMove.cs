using NPC;
using NPC.Behaviour;
using UnityEngine;


public class NPCMove : NPCBehaviourBase
{
    public NPCMove(NPC_AI_Base npc) : base(npc)
    {
    }  
    public override void OnDisable()
    { 
    }

    public override void OnEnable()
    { 
    }
    public override void Moving(Vector3 targetMove)
    {
        npc.npcRb.MovePosition(npc.npcRb.position + targetMove * npc.speedWalk * Time.fixedDeltaTime);
    }
}
