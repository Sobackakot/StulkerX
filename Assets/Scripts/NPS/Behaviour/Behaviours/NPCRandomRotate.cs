using NPC;
using UnityEngine;

public class NPCRandomRotate : NPCRotate
{
    public NPCRandomRotate(NPC_AI_Base npc) : base(npc)
    {
    }
    private float time;
    public override void OnEnable()
    {
        npc.target.onDetectedEnemy += Target_onDetectedEnemy;  
    } 
    private void Target_onDetectedEnemy()
    { 
        npc.target.onDetectedEnemy -= Target_onDetectedEnemy; 
        npc.data.SetIsLookTarget(true);
        npc.data.SetIsReadyForBattle(false);
    } 
    public override void FixedUpdate()
    { 
        SearchDetectedTargets();
        RandomRotate();
    } 
    public override void RandomRotate()
    {
        float turnAmount = 0;
        float currentY = npc.npcTr.eulerAngles.y;
        if (time < Time.time)
        {  
            turnAmount = Random.Range(npc.minAngleRotate, npc.maxAngleRotate);
            if (Random.value > 0.5f) turnAmount *= -1; 
        }
        float newY = currentY + turnAmount;
        Rotating(Quaternion.Euler(0, newY, 0)); 
    }
    private void SearchDetectedTargets()
    { 
        if (time < Time.time)
        { 
            time = Time.time + Random.Range(1f, 1.5f);
            npc.target.SearchDetectedTargets(npc.npcTr.position, npc.npcTr.forward, npc.viewAngle); 
        }   
    }
}
