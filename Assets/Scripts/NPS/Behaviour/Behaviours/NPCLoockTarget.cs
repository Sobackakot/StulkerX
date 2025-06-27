using NPC;
using NPC.Behaviour;
using UnityEngine;


public class NPCLoockTarget :NPCRotate
{
    public NPCLoockTarget(NPC_AI_Base npc) : base(npc)
    {
    }
    private float time;
    public override void OnEnable()
    {
        npc.target.onRayHitEnemy += Target_onRayHitEnemy;  
    }
    private void Target_onRayHitEnemy()
    { 
        npc.target.onRayHitEnemy -= Target_onRayHitEnemy;
        npc.target.SearchBestTarget(npc.npcTr.position); 
        npc.SetFocus(npc.target.GetTarget);
        npc.data.SetIsFollowTarget(true);
        npc.data.SetIsLookTarget(false);
    }

    public override void FixedUpdate()
    {
        LoockTarget();
        SearchRaycastHitTargets();
    }
    public override void LoockTarget()
    {
        if (npc.currentTarget!= null)
        {
            Vector3 loockTarget = (npc.targetTr.position - npc.npcTr.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(loockTarget.x, 0, loockTarget.z), Vector3.up); 
            Rotating(targetRotation); 
        } 
    }
    private void SearchRaycastHitTargets()
    {
        if (time < Time.time)
        {
            npc.target.SearchRaycastHitTargets(); 
            time = Time.time + 1; 
        }
    }

}
