using NPC.Behaviour;
using NPC.FSM;

public class RunStateNPC : NPCMoveFSMBase
{
    public RunStateNPC( 
        NPCMoveStateHandler moveHandler,
        NPC.Behaviour.IBehaviourHandler cicleBeh) :
        base(moveHandler, cicleBeh)
    {
    } 
    public override void EnterState()
    {
        AddTransition();
        cicleBeh.OnEnable(); 
    } 
    public override void ExitState()
    {
        cicleBeh.OnDisable();
    }

    public override void UpdateState()
    { 
        cicleBeh.Update(); 
    }
    private void AddTransition()
    {
        var type = NPCStateTypeMove.Run; 
        moveHandler.AddTransition(type, () => moveHandler.currentNPC.data.isIdle ? NPCStateTypeMove.Idle : type); 
    }
}