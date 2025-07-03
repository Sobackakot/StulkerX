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

         
        public Vector2 inputAxisCamera;
        private float _currentAngle;
        public float currentAngle 
        { 
            get => _currentAngle;
            set { _currentAngle = value; }
        }
        private bool _isFirstCamera; 
        public bool isFirstCamera 
        {
            get => _isFirstCamera;
            set { _isFirstCamera = value; } 
        } 


        public Vector3 inputAxis { get; set; }

        private bool _isActiveInventory;
        public bool isActiveInventory 
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
        public bool isIdle 
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
        public bool isWalk 
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
        public bool isRun 
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
        public bool isSprint 
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
        public bool isCrouch 
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
        public bool isAim 
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
        public bool isReadyForBattle
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
        public bool isReloadingState 
        {
            get => _isReloadingState;
            set
            {
                if (_isReloadingState == value) return;
                _isReloadingState = value;
                onExecuteWeaponTransition?.Invoke();
            }
        }
        public bool isCollision { get; set; } 
        public bool isMove { get; set; } 
        public bool isLeanRight { get; set; } 
        public bool isLeanLeft { get; set; } 
        
        public bool isLeftTargerPoint { get; set; } 
         
        public bool isParkour { get; set; }  

        
        public bool isFire { get; set; } 
        
        public bool isHasWeapon { get; set; }
        public bool isEquippingState { get; set; }
        
         

        public bool isRayHitToInventoryLootBox { get; set; }
        public bool isRayHitToItem { get; set; }
        public bool isRayHitToObstacle { get; set; }

        
    }
}


