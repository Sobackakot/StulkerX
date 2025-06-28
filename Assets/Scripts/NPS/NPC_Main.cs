
using NPC.Behaviour;
using NPC.FSM; 
using System;
using System.Collections.Generic;
using Zenject; 

namespace NPC.Main
{
    public class NPC_Main : IInitializable, IDisposable
    {
        public NPC_Main(IRegistryNPC npcRegistr)
        {
            this.npcRegistr = npcRegistr;
        }
        private readonly IRegistryNPC npcRegistr;
        private List<NPC_AI_Base> enemys = new();
        private Dictionary<NPC_AI_Base, NPCMoveStateHandler> states = new();
        private Dictionary<NPC_AI_Base, NPCBehaviourHandler> behaviours = new();

        public void Initialize()
        {
            enemys = npcRegistr?.GetListNPC();
              
            foreach (var npc in enemys)
            { 
                var moveFSM = new NPCMoveStateHandler(npc);
                var behaviour = new NPCBehaviourHandler();
                InitializeBehaviours(behaviour, npc); 
                moveFSM?.RegisterFSM(NPCStateTypeMove.Idle, new IdleStateNPC(moveFSM, behaviour.Idle));
                moveFSM?.RegisterFSM(NPCStateTypeMove.Ready, new ReadyStateNPC(moveFSM, behaviour.RanRotate));
                moveFSM?.RegisterFSM(NPCStateTypeMove.Aim, new AimStateNPC(moveFSM, behaviour.Loock));
                moveFSM?.RegisterFSM(NPCStateTypeMove.Run, new RunStateNPC(moveFSM, behaviour.Follow));
                moveFSM?.RegisterFSM(NPCStateTypeMove.Walk, new WalkStateNPC(moveFSM, behaviour.Move));

                moveFSM.SetFSM(NPCStateTypeMove.Idle);
                states[npc] = moveFSM;
                behaviours[npc] = behaviour;
            }
        }
        private void InitializeBehaviours(NPCBehaviourHandler behaviour, NPC_AI_Base npc)
        {
            behaviour?.InitIdleBehaviour(new NPCIdle(npc));
            behaviour?.InitMoveBehaviour(new NPCMove(npc));
            behaviour?.InitRotateBehaviour(new NPCRotate(npc));
            behaviour?.InitRandomMove(new NPCRandomMove(npc));
            behaviour?.InitRandomRotate(new NPCRandomRotate(npc));
            behaviour?.InitFollowTarget(new NPCFollowTarget(npc));
            behaviour?.InitLoockTarget(new NPCLoockTarget(npc));
        }
        public void Dispose() { }
        public void Tick()
        {

        }
        public void LateTick() { }
        public void FixedTick()
        {
            foreach (var state in states.Values)
            {
                state?.UpdateFSM();
            }
        }
    }

}
