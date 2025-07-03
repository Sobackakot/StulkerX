using System;
using UnityEngine;

namespace Character.Context
{
    public abstract class CharacterContextBase : IContextEvents, IContextStates, IContextCommands
    {
        public abstract event Action onExecuteMoveTransition;
        public abstract event Action onExecuteReadyTransition;
        public abstract event Action onExecuteWeaponTransition; 
        public abstract Vector3 InputAxis { get; }

        public abstract Vector2 InputAxisCamera { get; }
         
        public abstract float CurrentAngle { get; }

        public abstract bool IsFirstCamera { get; }

        public abstract bool IsIdle { get; }

        public abstract bool IsCrouch { get; }

        public abstract bool IsWalk { get; } 
        public abstract bool IsRun { get; }

        public abstract bool IsSprint { get; }

        public abstract bool IsHasWeapon { get; }

        public abstract bool IsAim { get; }

        public abstract bool IsReloadingState { get; }

        public abstract bool IsReadyForBattle { get; }

        public abstract bool IsEquippingState { get; }

        public abstract bool IsRayHitToItem { get; }

        public abstract bool IsActiveInventory { get; }

        public abstract bool IsRayHitToInventoryLootBox { get; }

        public abstract bool IsCollision { get; }

        public abstract bool IsLeanRight { get; }

        public abstract bool IsLeanLeft { get; }

        public abstract bool IsLeftTargerPoint { get; }

        public abstract bool IsParkour { get; }

        public abstract bool IsFire { get; }

        public abstract bool IsRayHitToObstacle { get; }

        public abstract void SetInputAxis(Vector3 axis);

        public abstract void SetInputAxisCamera(Vector3 axis);

        public abstract void SetCurrentAngle(float angle);

        public abstract void SetIsFirstCamera(bool isFirstCamera);

        public abstract void SetIsActiveInventory(bool isActive);

        public abstract void SetIsIdle(bool isIdle);
        public abstract void SetIsWalk(bool isWalk);

        public abstract void SetIsRun(bool isRun);

        public abstract void SetIsSprint(bool isSprint);

        public abstract void SetIsCrouch(bool isCrouch);

        public abstract void SetIsAim(bool isAim);

        public abstract void SetIsReadyForBattle(bool isReady);

        public abstract void SetIsReloadingState(bool isReload);

        public abstract void SetIsCollision(bool isCollision);

        public abstract void SetIsLeanRight(bool isRight);

        public abstract void SetIsLeanLeft(bool isLeft);

        public abstract void SetIsLeftTargerPoint(bool isLeftPoint);

        public abstract void SetIsParkour(bool isParkour);

        public abstract void SetIsFire(bool isFire);

        public abstract void SetIsHasWeapon(bool isHasWeapon);

        public abstract void SetIsEquippingState(bool isEquipping);

        public abstract void SetIsRayHitToInventoryLootBox(bool isHitLootBox);

        public abstract void SetIsRayHitToItem(bool isHitItem);

        public abstract void SetIsRayHitToObstacle(bool isHitObstacle);
    }
}