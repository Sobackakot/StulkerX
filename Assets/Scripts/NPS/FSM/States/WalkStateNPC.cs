using NPC.Behaviour;
using NPC.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStateNPC : NPCMoveFSMBase
{
    public WalkStateNPC(NPCMoveStateHandler moveHandler, NPC.Behaviour.IBehaviourHandler cicleBeh) : base(moveHandler, cicleBeh)
    {
    }

    public override void EnterState()
    {
        AddTransition(); 
    }

    public override void ExitState()
    { 
    }
    public override void UpdateState()
    { 
        cicleBeh.FixedUpdate();
    }
    private void AddTransition()
    {
        var type = NPCStateTypeMove.Walk;
        moveHandler.AddTransition(type, () => !moveHandler.currentNPC.data.isWalk ? NPCStateTypeMove.Idle : type);
    }
}
