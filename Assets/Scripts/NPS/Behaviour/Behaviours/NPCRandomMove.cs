using NPC;
using NPC.Behaviour;
using UnityEngine;


public class NPCRandomMove : NPCMove
{
    public NPCRandomMove(NPC_AI_Base npc) : base(npc)
    {
    }
    public override void OnEnable()
    {
    }

    public override void OnDisable()
    { 
    }
    public override void FixedUpdate()
    {
        RandomMove();
    }
    public override void RandomMove()
    {
        
    }
}
