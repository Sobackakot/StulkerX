using NPC.Behaviour;
using NPC.FSM;

public class IdleStateNPC : NPCMoveFSMBase
{
    public IdleStateNPC(
        NPCMoveStateHandler moveHandler,
        NPC.Behaviour.IBehaviourHandler cicleBeh) : 
        base(moveHandler,  cicleBeh)
    {
    } 
    public override void EnterState()
    {
        moveHandler.currentNPC.data.SetIsIdle(true);
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
        var type = NPCStateTypeMove.Idle;   
        moveHandler.AddTransition(type, () => moveHandler.currentNPC.data.isReadyForBattle ? NPCStateTypeMove.Ready : type); 
    }
}