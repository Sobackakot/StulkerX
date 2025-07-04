using Character.Context;
using StateData.Character;
using System;
using UnityEngine;
using State.Character.Move;

namespace Character.InputEvents
{
    public class CharacterInputEventHandler : IInputEvents
    {
        public CharacterInputEventHandler(
            IContextCommands contextCommands,
            IContextStates contextStates)
        {
            this.ctxCommands = contextCommands;
            this.ctxStates = contextStates;
        } 
        private IContextCommands ctxCommands;
        private IContextStates ctxStates;

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
            ctxCommands.SetInputAxisCamera(inputAxis.InputAxis); 
        }
        public void InputCamera_OnSwitchCamera(SwitchEventCamera a)
        { 
            ctxCommands.SetIsFirstCamera(!ctxStates.IsFirstCamera);
            OnSwichCamera?.Invoke();
        }

        public void ExitInventory(InventoryExitEvent inventory)
        {
            ctxCommands.SetIsActiveInventory(false);
            OnExitInventory?.Invoke();
        }
        public void InventoryLootBoxActive(InventoryLootBoxActiveEvent inventory)
        {
            if (ctxStates.IsRayHitToInventoryLootBox)
            {
                ctxCommands.SetIsActiveInventory(!ctxStates.IsActiveInventory);
                OnActiveInventoryLootBox?.Invoke(ctxStates.IsActiveInventory);
            }
        }
        public void InventoryActive(InventoryActiveEvent inventory)
        {
            ctxCommands.SetIsActiveInventory(!ctxStates.IsActiveInventory);
            OnActiveInventory?.Invoke(ctxStates.IsActiveInventory);
        }
        public void JumpingBehaviour(InputEventJump jump)
        {
            if (ctxStates.IsCollision && !ctxStates.IsRayHitToObstacle)
            {
                OnJump?.Invoke();
            }
        }
        public void PickUpItemBehaviour(PickUpItemEvent pickUp)
        {
            if (ctxStates.IsRayHitToItem)
            {
                if (OnHasWeapon.Invoke()) ctxCommands.SetIsHasWeapon(true);
                OnPickUpItem?.Invoke();
            }
        }
        public void ParkouringBehaviour(InputEventJump jump)
        {
            if (ctxStates.IsRayHitToObstacle)
                OnParkour?.Invoke();
        }

        public void MovingBehaviour(InputEventMove move)
        {
            ctxCommands.SetInputAxis(new Vector3(move.inputValue.x, 0, move.inputValue.y));
            if(MoveStateType.Run != ctxStates.MoveStateType && ctxStates.InputAxis.sqrMagnitude > 0.2f)
                ctxCommands.SetMoveStateType(MoveStateType.Run);
            ctxCommands.SetIsRun(ctxStates.InputAxis.sqrMagnitude > 0.2f 
                && !ctxStates.IsWalk && !ctxStates.IsCrouch);
        }
        public void CrouchingBehaviour(ToggleEventCrouch crouch)
        {
            ctxCommands.SetIsCrouch(!ctxStates.IsCrouch);
            ctxCommands.SetMoveStateType(MoveStateType.Crouch);
        }
        public void WalkingBehaviour(InputEventWalk walk)
        {
            ctxCommands.SetIsWalk(walk.isWalking);
            ctxCommands.SetMoveStateType(MoveStateType.Walk);   
        }
        public void SprintingBehaviour(InputEventSprint sprint)
        {
            if (ctxStates.MoveStateType != MoveStateType.Crouch)
            {
                ctxCommands.SetIsSprint(sprint.isSprinting);
                ctxCommands.SetMoveStateType(MoveStateType.Sprint);
            }    
        }

        public void LeaningLeftBehaviour(InputEventLeanLeft lean)
        {
            ctxCommands.SetIsLeanLeft(lean.isLeanLeft);
            ctxCommands.SetIsLeftTargerPoint(true);
        }
        public void LeaningRightBehaviour(InputEventLeanRight lean)
        {
            ctxCommands.SetIsLeanRight (lean.isLeanRight);
            ctxCommands.SetIsLeftTargerPoint(false);
        }


        public void EquipWeaponBehaviour(EquipWeaponToggleEvent equip)
        {
            if (!ctxStates.IsAim && ctxStates.IsHasWeapon && !ctxStates.IsReloadingState)
            {
                ctxCommands.SetIsReadyForBattle(!ctxStates.IsReadyForBattle);
            }
        }
        public void FireWeaponInput(FireInputEvent fire)
        {
            if (ctxStates.IsAim && ctxStates.IsHasWeapon && ctxStates.IsReadyForBattle && !ctxStates.IsReloadingState)
                ctxCommands.SetIsFire(fire.isFire);
        }
        public void ReloadWeaponBehaviour(ReloadWeaponEvent reload)
        {
            if (!ctxStates.IsAim && ctxStates.IsHasWeapon && ctxStates.IsReadyForBattle && !ctxStates.IsReloadingState)
                OnReloadWeapon?.Invoke();
        }
        public void AimWeaponBehaviour(AimInputEvent aim)
        {
            if (ctxStates.IsHasWeapon && ctxStates.IsReadyForBattle && !ctxStates.IsReloadingState)
            {
                ctxCommands.SetIsAim (aim.isAiming);
            }
        }

        public void SetReloadWeaponAnimationState(bool isReload) // call from StateMachineAnimator
        {
            ctxCommands.SetIsReloadingState(isReload);
            if (isReload)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }

        public void SetEquippWeaponAnimationState(bool isEquipping) // call from StateMachineAnimator
        {
            ctxCommands.SetIsEquippingState(isEquipping);
            if (isEquipping)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }
    }

}
