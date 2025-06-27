using State;
using State.CoreFSM; 

namespace NPC.FSM
{
    public class NPCMoveStateHandler : StateMachine<NPCStateTypeMove, IMoveFSM>
    {
        public NPCMoveStateHandler(NPC_AI_Base newNPC)
        {
            currentNPC = newNPC;
        }
        public readonly NPC_AI_Base currentNPC;
    }
    public enum NPCStateTypeMove
    {
        Idle,
        Walk,
        Run,
        Sprint, 
        Crouch, 
        Ready,
        Aim
    }
    public interface IMoveFSM : IState
    {
    }
}

