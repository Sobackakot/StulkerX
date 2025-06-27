using NPC;
using NPC.Behaviour; 
using UnityEngine;

public class NPCIdle : NPCBehaviourBase
{ 
    public NPCIdle(NPC_AI_Base npc) : base(npc)
    {
    }
    private float time;
    public override void OnEnable()
    { 
        npc.target.onNearbyEnemy += Target_onNearbyEnemy;  
    }
    private void Target_onNearbyEnemy()
    { 
        npc.target.onNearbyEnemy -= Target_onNearbyEnemy; 
        npc.data.SetIsReadyForBattle(true);
        npc.data.SetIsIdle(false); 
    }
    public override void Update()
    {
        SearchNearbyTargets();
        IdleState();
    } 
    public override void IdleState() 
    { 
    }
    private void SearchNearbyTargets()
    {
        if (time < Time.time)
        { 
            time = Time.time + 1;
            npc.target.SearchNearbyTargets(npc.npcTr.position, npc.visionRadius);
        }
    }
}
