using UnityEngine;

namespace Character.Context
{
    public interface IContextCommands
    {
        void SetInputAxis(Vector3 axis);
        void SetInputAxisCamera(Vector3 axis);
        void SetCurrentAngle(float angle);
        void SetIsFirstCamera(bool isFirstCamera); 
        void SetIsActiveInventory(bool isActive);
        void SetIsIdle(bool isIdle);
        void SetIsWalk(bool isWalk);
        void SetIsRun(bool isRun);
        void SetIsSprint(bool isSprint);
        void SetIsCrouch(bool isCrouch);
        void SetIsAim(bool isAim);
        void SetIsReadyForBattle(bool isReady);
        void SetIsReloadingState(bool isReload);
        void SetIsCollision(bool isCollision);
        void SetIsLeanRight(bool isRight);
        void SetIsLeanLeft(bool isLeft);
        void SetIsLeftTargerPoint(bool isLeftPoint);
        void SetIsParkour(bool isParkour);
        void SetIsFire(bool isFire);
        void SetIsHasWeapon(bool isHasWeapon);
        void SetIsEquippingState(bool isEquipping);
        void SetIsRayHitToInventoryLootBox(bool isHitLootBox);
        void SetIsRayHitToItem(bool isHitItem);
        void SetIsRayHitToObstacle(bool isHitObstacle);
    }
}

