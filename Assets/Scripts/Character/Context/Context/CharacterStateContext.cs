using Character.Context;
using System;  
using UnityEngine; 

namespace StateData.Character 
{
    public class CharacterStateContext : CharacterContextBase
    {
        public override event Action onExecuteMoveTransition;
        public override event Action onExecuteReadyTransition;
        public override event Action onExecuteWeaponTransition; 

        Vector3 _inputAxis;
        public override Vector3 InputAxis => _inputAxis;

        Vector2 _inputAxisCamera;
        public override Vector2 InputAxisCamera => _inputAxisCamera;

        float _currentAngle;
        public override float CurrentAngle => _currentAngle;

        bool _isFirstCamera;
        public override bool IsFirstCamera => _isFirstCamera;

        bool _isActiveInventory;
        public override bool IsActiveInventory => _isActiveInventory;

        bool _isIdle;
        public override bool IsIdle => _isIdle;
        bool _isWalk;
        public override bool IsWalk => _isWalk;
        bool _isRun;
        public override bool IsRun => _isRun;
        bool _isSprint;
        public override bool IsSprint => _isSprint;
        bool _isCrouch;
        public override bool IsCrouch => _isCrouch;
        bool _isAim;
        public override bool IsAim => _isAim;
        bool _isReadyForBattle;
        public override bool IsReadyForBattle => _isReadyForBattle;
        bool _isReloadingState;
        public override bool IsReloadingState => _isReloadingState;
        bool _isCollision;
        public override bool IsCollision => _isCollision;
        bool _isLeanRight;
        public override bool IsLeanRight => _isLeanRight;
        bool _isLeanLeft;
        public override bool IsLeanLeft => _isLeanLeft;
        bool _isLeftTargetPoint;
        public override bool IsLeftTargerPoint => _isLeftTargetPoint;
        bool _isParkour;
        public override bool IsParkour => _isParkour;
        bool _isFire;
        public override bool IsFire => _isFire;
        bool _isHasWeapon;
        public override bool IsHasWeapon => _isHasWeapon;
        bool _isEquippingState;
        public override bool IsEquippingState => _isEquippingState;
        bool _isRayHitToInventoryLootBox;
        public override bool IsRayHitToInventoryLootBox => _isRayHitToInventoryLootBox;
        bool _isRayHitToItem;
        public override bool IsRayHitToItem => _isRayHitToItem;
        bool _isRayHitToObstacle;
        public override bool IsRayHitToObstacle => _isRayHitToObstacle;
         

        public override void SetInputAxis(Vector3 axis)
        {
            _inputAxis = axis;
        }

        public override void SetInputAxisCamera(Vector3 axis)
        {
            _inputAxisCamera = axis;
        }

        public override void SetCurrentAngle(float angle)
        {
            _currentAngle = angle;
        }

        public override void SetIsFirstCamera(bool isFirstCamera)
        {
            _isFirstCamera = isFirstCamera;
        }

        public override void SetIsActiveInventory(bool isActive)
        { 
            _isActiveInventory = isActive;
        }

        public override void SetIsIdle(bool isIdle)
        {
            if (_isIdle == isIdle) return;
            _isIdle = isIdle;
            onExecuteMoveTransition?.Invoke();
        }

        public override void SetIsWalk(bool isWalk)
        {
            if (_isWalk == isWalk) return;
            _isWalk = isWalk;
            onExecuteMoveTransition?.Invoke();
        }

        public override void SetIsRun(bool isRun)
        {
            if (_isRun == isRun) return;
            _isRun = isRun;
            onExecuteMoveTransition?.Invoke();
        }

        public override void SetIsSprint(bool isSprint)
        {
            if (_isSprint == isSprint) return;
            _isSprint = isSprint;
            onExecuteMoveTransition?.Invoke();
        }

        public override void SetIsCrouch(bool isCrouch)
        {
            if (_isCrouch == isCrouch) return;
            _isCrouch = isCrouch;
            onExecuteMoveTransition?.Invoke();
        }

        public override void SetIsAim(bool isAim)
        {
            if (_isAim == isAim) return;
            _isAim = isAim;
            onExecuteMoveTransition?.Invoke();
            onExecuteWeaponTransition?.Invoke();
        }

        public override void SetIsReadyForBattle(bool isReady)
        {
            if (_isReadyForBattle == isReady) return;
            _isReadyForBattle = isReady;
            onExecuteReadyTransition?.Invoke();
        }

        public override void SetIsReloadingState(bool isReload)
        {
            if (_isReloadingState == isReload) return;
            _isReloadingState = isReload;
            onExecuteWeaponTransition?.Invoke();
        }

        public override void SetIsCollision(bool isCollision)
        {
            _isCollision = isCollision;
        }

        public override void SetIsLeanRight(bool isRight)
        {
            _isLeanRight = isRight;
        }

        public override void SetIsLeanLeft(bool isLeft)
        {
            _isLeanLeft = isLeft;
        }

        public override void SetIsLeftTargerPoint(bool isLeftPoint)
        {
            _isLeftTargetPoint = isLeftPoint;
        }

        public override void SetIsParkour(bool isParkour)
        {
            _isParkour = isParkour;
        }

        public override void SetIsFire(bool isFire)
        {
            _isFire = isFire;
        }

        public override void SetIsHasWeapon(bool isHasWeapon)
        {
            _isHasWeapon = isHasWeapon;
        }

        public override void SetIsEquippingState(bool isEquipping)
        {
            _isEquippingState = isEquipping;
        }

        public override void SetIsRayHitToInventoryLootBox(bool isHitLootBox)
        {
            _isRayHitToInventoryLootBox = isHitLootBox;
        }

        public override void SetIsRayHitToItem(bool isHitItem)
        {
            _isRayHitToItem = isHitItem;
        }

        public override void SetIsRayHitToObstacle(bool isHitObstacle)
        {
            _isRayHitToObstacle = isHitObstacle;
        }
    }
}


