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



        public event Action<bool> OnActiveInventoryLootBox;
        public event Action<bool> OnActiveInventory;
        public event Action OnExitInventory;

        public event Func<bool> OnHasWeapon;
        public event Action OnReloadWeapon;
        public event Action OnSetParentWeapon;
        public event Action OnResetParenWeapon;

        public void Enable()
        {
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
        public void Disable()
        {
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

        public void ExitInventory(InventoryExitEvent inventory)
        {
            stateData.isActiveInventory = false;
            OnExitInventory?.Invoke();
        }
        public void InventoryLootBoxActive(InventoryLootBoxActiveEvent inventory)
        {
            if (stateData.isRayHitToInventoryLootBox)
            {
                stateData.isActiveInventory = !stateData.isActiveInventory;
                OnActiveInventoryLootBox?.Invoke(stateData.isActiveInventory);
            }
        }
        public void InventoryActive(InventoryActiveEvent inventory)
        {
            stateData.isActiveInventory = !stateData.isActiveInventory;
            OnActiveInventory?.Invoke(stateData.isActiveInventory);
        }
        public void JumpingBehaviour(InputEventJump jump)
        {
            if (stateData.isCollision && !stateData.isRayHitToObstacle)
            {
                OnJump?.Invoke();
            }
        }
        public void PickUpItemBehaviour(PickUpItemEvent pickUp)
        {
            if (stateData.isRayHitToItem)
            {
                if (OnHasWeapon.Invoke()) stateData.isHasWeapon = true;
                OnPickUpItem?.Invoke();
            }
        }
        public void ParkouringBehaviour(InputEventJump jump)
        {
            if (stateData.isRayHitToObstacle)
                OnParkour?.Invoke();
        }

        public void MovingBehaviour(InputEventMove move)
        {
            stateData.inputAxis = new Vector3(move.inputValue.x, 0, move.inputValue.y);
            stateData.isRun = stateData.inputAxis.sqrMagnitude > 0.2f && !stateData.isWalk && !stateData.isCrouch;
        }
        public void CrouchingBehaviour(ToggleEventCrouch crouch)
        {
            stateData.isCrouch = !stateData.isCrouch;
        }
        public void WalkingBehaviour(InputEventWalk walk)
        {
            stateData.isWalk = walk.inputValue;
        }
        public void SprintingBehaviour(InputEventSprint sprint)
        {
            if (!stateData.isCrouch)
                stateData.isSprint = sprint.inputValue;
        }

        public void LeaningLeftBehaviour(InputEventLeanLeft lean)
        {
            stateData.isLeanLeft = lean.inputValue;
            stateData.isLeftTargerPoint = true;
        }
        public void LeaningRightBehaviour(InputEventLeanRight lean)
        {
            stateData.isLeanRight = lean.inputValue;
            stateData.isLeftTargerPoint = false;
        }


        public void EquipWeaponBehaviour(EquipWeaponToggleEvent equip)
        {
            if (!stateData.isAim && stateData.isHasWeapon && !stateData.isReloadingState)
            {
                stateData.isReadyForBattle = !stateData.isReadyForBattle;
            }
        }
        public void FireWeaponInput(FireInputEvent fire)
        {
            if (stateData.isAim && stateData.isHasWeapon && stateData.isReadyForBattle && !stateData.isReloadingState)
                stateData.isFire = fire.inputValue;
        }
        public void ReloadWeaponBehaviour(ReloadWeaponEvent reload)
        {
            if (!stateData.isAim && stateData.isHasWeapon && stateData.isReadyForBattle && !stateData.isReloadingState)
                OnReloadWeapon?.Invoke();
        }
        public void AimWeaponBehaviour(AimInputEvent aim)
        {
            if (stateData.isHasWeapon && stateData.isReadyForBattle && !stateData.isReloadingState)
            {
                stateData.isAim = aim.inputValue;
            }
        }

        public void SetReloadWeaponAnimationState(bool isReload) // call from StateMachineAnimator
        {
            stateData.isReloadingState = isReload;
            if (isReload)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }

        public void SetEquippWeaponAnimationState(bool isEquipping) // call from StateMachineAnimator
        {
            stateData.isEquippingState = isEquipping;
            if (isEquipping)
                OnSetParentWeapon?.Invoke(); // subscribe from WeaponFreeParent
            else
                OnResetParenWeapon?.Invoke();
        }
    }

}
