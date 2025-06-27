  
namespace NPC.Behaviour
{
    public class NPCBehaviourHandler
    {
        public IIdleNPC Idle { get; private set; }
        public IRotateNPC Rotate { get; private set; }
        public IMoveNPC Move { get; private set; }
        public IRandomRotateNPC RanRotate { get; private set; }
        public IRandomMoveNPC RanMove { get; private set; }
        public IFollowTargetNPC Follow { get; private set; }
        public ILoockTargetNPC Loock { get; private set; }

        public void InitIdleBehaviour(IIdleNPC idle) => Idle = idle;
        public void InitMoveBehaviour(IMoveNPC move) => Move = move;
        public void InitRotateBehaviour(IRotateNPC rotate) => Rotate = rotate;
        public void InitRandomMove(IRandomMoveNPC ranMove) => RanMove = ranMove;
        public void InitRandomRotate(IRandomRotateNPC ranRot) => RanRotate = ranRot;
        public void InitFollowTarget(IFollowTargetNPC followTar) => Follow = followTar;
        public void InitLoockTarget(ILoockTargetNPC loockTar) => Loock = loockTar;
    }
}