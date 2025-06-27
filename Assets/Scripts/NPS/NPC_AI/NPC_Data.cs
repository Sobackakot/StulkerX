
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NPC.Data
{
    [Serializable]
    public class NPC_Data
    { 
        public bool isAim { get; private set; } 
        public bool isFight { get; private set; }
        public bool isReadyForBattle { get; private set; } 
        public bool isIdle { get; private set; }
        public bool isWalk { get; private set; }
        public bool isRun { get; private set; }
        public bool isSprint { get; private set; }
        public bool isCrouch { get; private set; }
        public bool isRotate { get; private set; }

        public bool isPatrule { get; private set; }
        public bool isFollowTarget { get; private set; }
        public bool isLookTarget { get; private set; }
        public bool isAttack { get; private set; }
        public bool isRetreat { get; private set; }
        public bool isInteract { get; private set; }

        public void SetIsIdle(bool idle) => isIdle = idle;
        public void SetIsReadyForBattle(bool isReady) => isReadyForBattle = isReady;
        public void SetIsLookTarget(bool isLook) => isLookTarget = isLook;
        public void SetIsFollowTarget(bool isFollow) => isFollowTarget = isFollow;
    }

}

