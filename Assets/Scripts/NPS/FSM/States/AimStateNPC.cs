using NPC.Behaviour;
using NPC.FSM;

public class AimStateNPC : NPCMoveFSMBase
{ 
    public AimStateNPC(
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
        cicleBeh.FixedUpdate();
    }
    private void AddTransition()
    {
        var type = NPCStateTypeMove.Aim; 
        moveHandler.AddTransition(type, () => moveHandler.currentNPC.data.isFollowTarget ? NPCStateTypeMove.Run : type);
    }
}
