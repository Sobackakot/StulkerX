using Character.Context;
using System;  
using UnityEngine;

namespace StateData.Character 
{
    public class CharacterStateContext : CharacterContext
    {
        public override event Action onExecuteMoveTransition;
        public override event Action onExecuteReadyTransition;
        public override event Action onExecuteWeaponTransition;
        public override event Action<bool> onStoppingExecute;

        public Vector3 inputAxis { get; set; }
        public Vector2 inputAxisCamera;
        private float _currentAngle;
        public float currentAngle 
        { 
            get => _currentAngle;
            set { _currentAngle = value; }
        }
        private bool _isFirstCamera; 
        public bool IsFirstCamera 
        {
            get => _isFirstCamera;
            set { _isFirstCamera = value; } 
        } 


       

        private bool _isActiveInventory;
        public bool IsActiveInventory 
        {
            get => _isActiveInventory;
            set
            {
                if (_isActiveInventory == value) return;
                _isActiveInventory = value;
                onStoppingExecute?.Invoke(_isActiveInventory);
            }
        }


        private bool _isIdle;
        public bool IsIdle 
        {
            get => _isIdle;
            set
            {
                if (_isIdle == value) return;
                _isIdle = value;
                onExecuteMoveTransition?.Invoke();
            }
        }
        private bool _isWalk;
        public bool IsWalk 
        { 
            get => _isWalk;
            set
            {
                if (_isWalk == value) return;
                _isWalk = value; 
                onExecuteMoveTransition?.Invoke();
            }
        }
        private bool _isRun;
        public bool IsRun 
        { 
            get => _isRun;
            set
            {
                if (_isRun == value) return;
                _isRun = value; 
                onExecuteMoveTransition?.Invoke();
            }
        }
        private bool _isSprint;
        public bool IsSprint 
        {
            get => _isSprint;
            set
            {
                if (_isSprint == value) return;
                _isSprint = value;
                onExecuteMoveTransition?.Invoke();
            }
        }
        private bool _isCrouch;
        public bool IsCrouch 
        {
            get => _isCrouch;
            set
            {
                if (_isCrouch == value) return;
                _isCrouch = value; 
                onExecuteMoveTransition?.Invoke();
            }
        }
        private bool _isAim;
        public bool IsAim 
        {
            get => _isAim;
            set
            {
                if (_isAim == value) return; 
                _isAim = value;
                onExecuteMoveTransition?.Invoke();
                onExecuteWeaponTransition?.Invoke();
            }
        }
        private bool _isReadyForBattle;
        public bool IsReadyForBattle
        {
            get => _isReadyForBattle;
            set
            {
                if (_isReadyForBattle == value) return;
                _isReadyForBattle = value;
                onExecuteReadyTransition?.Invoke();
            }
        }
        private bool _isReloadingState;
        public bool IsReloadingState 
        {
            get => _isReloadingState;
            set
            {
                if (_isReloadingState == value) return;
                _isReloadingState = value;
                onExecuteWeaponTransition?.Invoke();
            }
        }
        public bool IsCollision { get; set; }  
        public bool IsLeanRight { get; set; } 
        public bool IsLeanLeft { get; set; } 
        
        public bool IsLeftTargerPoint { get; set; } 
         
        public bool IsParkour { get; set; }  

        
        public bool IsFire { get; set; } 
        
        public bool IsHasWeapon { get; set; }
        public bool IsEquippingState { get; set; }
        
         

        public bool IsRayHitToInventoryLootBox { get; set; }
        public bool IsRayHitToItem { get; set; }
        public bool IsRayHitToObstacle { get; set; }

        
    }
}


