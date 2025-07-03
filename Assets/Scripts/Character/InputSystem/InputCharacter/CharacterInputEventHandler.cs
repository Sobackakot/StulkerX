using Character.Context;
using System;
using UnityEngine;

namespace Character.InputEvents
{
    public class CharacterInputEventHandler : IInputEvents
    {
        public CharacterInputEventHandler(
            IContextCommands contextCommands,
            IContextStates contextStates)
        {
            this.contextCommands = contextCommands;
            this.contextStates = contextStates;
        }
        private IContextCommands contextCommands;
        private IContextStates contextStates;

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
            contextCommands.SetInputAxisCamera(inputAxis.InputAxis); 
        }
        public void InputCamera_OnSwitchCamera(SwitchEventCamera a)
        { 
            contextCommands.SetIsFirstCamera(!contextStates.IsFirstCamera);
            OnSwichCamera?.Invoke();
        }

        public void ExitInventory(InventoryExitEvent inventory)
        {
            contextCommands.SetIsActiveInventory(false);
            OnExitInventory?.Invoke();
        }
        public void InventoryLootBoxActive(InventoryLootBoxActiveEvent inventory)
        {
            if (contextStates.IsRayHitToInventoryLootBox)
            {
                contextCommands.SetIsActiveInventory(!contextStates.IsActiveInventory);
                OnActiveInventoryLootBox?.Invoke(contextStates.IsActiveInventory);
            }
        }
        public void InventoryActive(InventoryActiveEvent inventory)
        {
            contextCommands.SetIsActiveInventory(!contextStates.IsActiveInventory);
            OnActiveInventory?.Invoke(contextStates.IsActiveInventory);
        }
        public void JumpingBehaviour(InputEventJump jump)
        {
            if (contextStates.IsCollision && !contextStates.IsRayHitToObstacle)
            {
                OnJump?.Invoke();
            }
        }
        public void PickUpItemBehaviour(PickUpItemEvent pickUp)
        {
            if (contextStates.IsRayHitToItem)
            {
                if (OnHasWeapon.Invoke()) contextCommands.SetIsHasWeapon(true);
                OnPickUpItem?.Invoke();
            }
        }
        public void ParkouringBehaviour(InputEventJump jump)
        {
            if (contextStates.IsRayHitToObstacle)
                OnParkour?.Invoke();
        }

        public void MovingBehaviour(InputEventMove move)
        {
            contextCommands.SetInputAxis(new Vector3(move.inputValue.x, 0, move.inputValue.y));
            contextCommands.SetIsRun(contextStates.InputAxis.sqrMagnitude > 0.2f 
                && !contextStates.IsWalk && !contextStates.IsCrouch);
        }
        public void CrouchingBehaviour(ToggleEventCrouch crouch)
        {
            contextCommands.SetIsCrouch(!contextStates.IsCrouch);
        }
        public void WalkingBehaviour(InputEventWalk walk)
        {
            contextCommands.SetIsWalk(walk.isWalking);
        }
        public void SprintingBehaviour(InputEventSprint sprint)
        {
            if (!contextStates.IsCrouch)
                contextCommands.SetIsSprint(sprint.isSprinting);
        }

        public void LeaningLeftBehaviour(InputEventLeanLeft lean)
        {
            contextCommands.SetIsLeanLeft(lean.isLeanLeft);
            contextCommands.SetIsLeftTargerPoint(true);
        }
        public void LeaningRightBehaviour(InputEventLeanRight lean)
        {
            contextCommands.SetIsLeanRight (lean.isLeanRight);
            contextCommands.SetIsLeftTargerPoint(false);
        }


        public void EquipWeaponBehaviour(EquipWeaponToggleEvent equip)
        {
            if (!contextStates.IsAim && contextStates.IsHasWeapon && !contextStates.IsReloadingState)
            {
                contextCommands.SetIsReadyForBattle(!contextStates.IsReadyForBattle);
            }
        }
        public void FireWeaponInput(FireInputEvent fire)
        {
            if (contextStates.IsAim && contextStates.IsHasWeapon && contextStates.IsReadyForBattle && !contextStates.IsReloadingState)
                contextCommands.SetIsFire(fire.isFire);
        }
        public void ReloadWeaponBehaviour(ReloadWeaponEvent reload)
        {
            if (!contextStates.IsAim && contextStates.IsHasWeapon && contextStates.IsReadyForBattle && !contextStates.IsReloadingState)
                OnReloadWeapon?.Invoke();
        }
        public void AimWeaponBehaviour(AimInputEvent aim)
        {
            if (contextStates.IsHasWeapon && contextStates.IsReadyForBattle && !contextStates.IsReloadingState)
            {
                contextCommands.SetIsAim (aim.isAiming);
            }
        }

        public void SetReloadWeaponAnimationState(bool isReload) // call from StateMachineAnimator
        {
            contextCommands.SetIsReloadingState(isReload);
            if (isReload)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }

        public void SetEquippWeaponAnimationState(bool isEquipping) // call from StateMachineAnimator
        {
            contextCommands.SetIsEquippingState(isEquipping);
            if (isEquipping)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }
    }

}
