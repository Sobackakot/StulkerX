using StateData.Character;
using System;
using UnityEngine;

namespace Character.InputEvents
{
    public class CharacterInputEventHandler : IInputEvents
    {
        public CharacterInputEventHandler(CharacterStateContext stateData)
        {
            this.stateData = stateData; 
        }
        private CharacterStateContext stateData;

        public event Action OnJump;
        public event Action OnParkour;
        public event Action OnPickUpItem;
        public event Action OnSwichCamera;



        public event Action<bool> OnActiveInventoryLootBox;
        public event Action<bool> OnActiveInventory;
        public event Action OnExitInventory;

        public event Func<bool> OnHasWeapon;
        public event Action OnReloadWeapon;
        public event Action OnSetParentWeapon;
        public event Action OnResetParenWeapon;

        public void Initialize()
        {
            EventBus.Subscribe<SwitchEventCamera>(InputCamera_OnSwitchCamera);
            EventBus.Subscribe<InputEventCamera>(InputCamera_OnInputAxisCamera);

            EventBus.Subscribe<ReloadWeaponEvent>(ReloadWeaponBehaviour);
            EventBus.Subscribe<AimInputEvent>(AimWeaponBehaviour);
            EventBus.Subscribe<EquipWeaponToggleEvent>(EquipWeaponBehaviour);
            EventBus.Subscribe<FireInputEvent>(FireWeaponInput);

            EventBus.Subscribe<ToggleEventCrouch>(CrouchingBehaviour);
            EventBus.Subscribe<InputEventJump>(JumpingBehaviour);
            EventBus.Subscribe<InputEventLeanRight>(LeaningRightBehaviour);
            EventBus.Subscribe<InputEventLeanLeft>(LeaningLeftBehaviour);
            EventBus.Subscribe<InputEventMove>(MovingBehaviour);
            EventBus.Subscribe<InputEventJump>(ParkouringBehaviour);
            EventBus.Subscribe<PickUpItemEvent>(PickUpItemBehaviour);
            EventBus.Subscribe<InputEventSprint>(SprintingBehaviour);
            EventBus.Subscribe<InputEventWalk>(WalkingBehaviour);

            EventBus.Subscribe<InventoryExitEvent>(ExitInventory);
            EventBus.Subscribe<InventoryLootBoxActiveEvent>(InventoryLootBoxActive);
            EventBus.Subscribe<InventoryActiveEvent>(InventoryActive);
        }
        public void Dispose()
        {
            EventBus.Unsubscribe<SwitchEventCamera>(InputCamera_OnSwitchCamera);
            EventBus.Unsubscribe<InputEventCamera>(InputCamera_OnInputAxisCamera);

            EventBus.Unsubscribe<ReloadWeaponEvent>(ReloadWeaponBehaviour);
            EventBus.Unsubscribe<AimInputEvent>(AimWeaponBehaviour);
            EventBus.Unsubscribe<EquipWeaponToggleEvent>(EquipWeaponBehaviour);
            EventBus.Unsubscribe<FireInputEvent>(FireWeaponInput);

            EventBus.Unsubscribe<ToggleEventCrouch>(CrouchingBehaviour);
            EventBus.Unsubscribe<InputEventJump>(JumpingBehaviour);
            EventBus.Unsubscribe<InputEventLeanRight>(LeaningRightBehaviour);
            EventBus.Unsubscribe<InputEventLeanLeft>(LeaningLeftBehaviour);
            EventBus.Unsubscribe<InputEventMove>(MovingBehaviour);
            EventBus.Unsubscribe<InputEventJump>(ParkouringBehaviour);
            EventBus.Unsubscribe<PickUpItemEvent>(PickUpItemBehaviour);
            EventBus.Unsubscribe<InputEventSprint>(SprintingBehaviour);
            EventBus.Unsubscribe<InputEventWalk>(WalkingBehaviour);

            EventBus.Unsubscribe<InventoryExitEvent>(ExitInventory);
            EventBus.Unsubscribe<InventoryLootBoxActiveEvent>(InventoryLootBoxActive);
            EventBus.Unsubscribe<InventoryActiveEvent>(InventoryActive);
        }

        public void InputCamera_OnInputAxisCamera(InputEventCamera inputAxis)
        { 
            stateData.inputAxisCamera = inputAxis.InputAxis; 
        }
        public void InputCamera_OnSwitchCamera(SwitchEventCamera a)
        {
            stateData.IsFirstCamera = !stateData.IsFirstCamera;
            OnSwichCamera?.Invoke();
        }

        public void ExitInventory(InventoryExitEvent inventory)
        {
            stateData.IsActiveInventory = false;
            OnExitInventory?.Invoke();
        }
        public void InventoryLootBoxActive(InventoryLootBoxActiveEvent inventory)
        {
            if (stateData.IsRayHitToInventoryLootBox)
            {
                stateData.IsActiveInventory = !stateData.IsActiveInventory;
                OnActiveInventoryLootBox?.Invoke(stateData.IsActiveInventory);
            }
        }
        public void InventoryActive(InventoryActiveEvent inventory)
        {
            stateData.IsActiveInventory = !stateData.IsActiveInventory;
            OnActiveInventory?.Invoke(stateData.IsActiveInventory);
        }
        public void JumpingBehaviour(InputEventJump jump)
        {
            if (stateData.IsCollision && !stateData.IsRayHitToObstacle)
            {
                OnJump?.Invoke();
            }
        }
        public void PickUpItemBehaviour(PickUpItemEvent pickUp)
        {
            if (stateData.IsRayHitToItem)
            {
                if (OnHasWeapon.Invoke()) stateData.IsHasWeapon = true;
                OnPickUpItem?.Invoke();
            }
        }
        public void ParkouringBehaviour(InputEventJump jump)
        {
            if (stateData.IsRayHitToObstacle)
                OnParkour?.Invoke();
        }

        public void MovingBehaviour(InputEventMove move)
        {
            stateData.inputAxis = new Vector3(move.inputValue.x, 0, move.inputValue.y);
            stateData.IsRun = stateData.inputAxis.sqrMagnitude > 0.2f && !stateData.IsWalk && !stateData.IsCrouch;
        }
        public void CrouchingBehaviour(ToggleEventCrouch crouch)
        {
            stateData.IsCrouch = !stateData.IsCrouch;
        }
        public void WalkingBehaviour(InputEventWalk walk)
        {
            stateData.IsWalk = walk.inputValue;
        }
        public void SprintingBehaviour(InputEventSprint sprint)
        {
            if (!stateData.IsCrouch)
                stateData.IsSprint = sprint.inputValue;
        }

        public void LeaningLeftBehaviour(InputEventLeanLeft lean)
        {
            stateData.IsLeanLeft = lean.inputValue;
            stateData.IsLeftTargerPoint = true;
        }
        public void LeaningRightBehaviour(InputEventLeanRight lean)
        {
            stateData.IsLeanRight = lean.inputValue;
            stateData.IsLeftTargerPoint = false;
        }


        public void EquipWeaponBehaviour(EquipWeaponToggleEvent equip)
        {
            if (!stateData.IsAim && stateData.IsHasWeapon && !stateData.IsReloadingState)
            {
                stateData.IsReadyForBattle = !stateData.IsReadyForBattle;
            }
        }
        public void FireWeaponInput(FireInputEvent fire)
        {
            if (stateData.IsAim && stateData.IsHasWeapon && stateData.IsReadyForBattle && !stateData.IsReloadingState)
                stateData.IsFire = fire.inputValue;
        }
        public void ReloadWeaponBehaviour(ReloadWeaponEvent reload)
        {
            if (!stateData.IsAim && stateData.IsHasWeapon && stateData.IsReadyForBattle && !stateData.IsReloadingState)
                OnReloadWeapon?.Invoke();
        }
        public void AimWeaponBehaviour(AimInputEvent aim)
        {
            if (stateData.IsHasWeapon && stateData.IsReadyForBattle && !stateData.IsReloadingState)
            {
                stateData.IsAim = aim.inputValue;
            }
        }

        public void SetReloadWeaponAnimationState(bool isReload) // call from StateMachineAnimator
        {
            stateData.IsReloadingState = isReload;
            if (isReload)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }

        public void SetEquippWeaponAnimationState(bool isEquipping) // call from StateMachineAnimator
        {
            stateData.IsEquippingState = isEquipping;
            if (isEquipping)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }
    }

}
