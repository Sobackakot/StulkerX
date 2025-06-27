using NPC;
using NPC.Behaviour;
using UnityEngine;


public class NPCRotate : NPCBehaviourBase
{
    public NPCRotate(NPC_AI_Base npc) : base(npc)
    {
    }

    public override void OnDisable()
    { 
    }

    public override void OnEnable()
    { 
    }

    public override void Rotating(Quaternion targetRotation)
    {
        Quaternion newRot = Quaternion.Slerp(npc.npcTr.rotation, targetRotation, npc.rotateAngle * Time.fixedDeltaTime);
        npc.npcRb.MoveRotation(newRot);
    } 
}
