using NPC;
using NPC.Behaviour;

public class NPCFollowTarget : NPCBehaviourBase
{
    public NPCFollowTarget(NPC_AI_Base npc) : base(npc)
    {
    }
    public override void OnEnable()
    {  
    }

    public override void Update()
    {
        FollowTarget();
    }
    public override void FollowTarget()
    {
        npc?.SetFocus(npc.target.GetTarget);
        if (npc.target.GetTarget != null && npc.IsMinDistance(npc.visionRadius)&& npc.agent.isOnNavMesh)
        {
            npc.agent.SetDestination(npc.targetTr.position);
            npc.StoppedDestination(); 
        }
        else
        {
            npc.ResetFocus();
            npc.data.SetIsFollowTarget(false);
            npc.data.SetIsIdle(true);
        }
    } 
}
