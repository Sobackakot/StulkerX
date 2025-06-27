using NPC.Behaviour;
using NPC.FSM;

public class ReadyStateNPC : NPCMoveFSMBase
{
    public ReadyStateNPC(
        NPCMoveStateHandler moveHandler,
        NPC.Behaviour.IBehaviourHandler cicleBeh) : 
        base(moveHandler, cicleBeh)
    { } 
    public override void EnterState()
    {
        AddTRansition();
        cicleBeh.OnEnable(); 
    } 
    public override void ExitState()
    {
        cicleBeh.OnDisable();
    } 
    public override void UpdateState()
    { 
        cicleBeh.FixedUpdate(); 
    }
    private void AddTRansition()
    {
        var type = NPCStateTypeMove.Ready;  
        moveHandler.AddTransition(type, () => moveHandler.currentNPC.data.isLookTarget ? NPCStateTypeMove.Aim : type);
    }
}