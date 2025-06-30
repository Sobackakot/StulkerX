using System;

namespace Character.InputEvents
{
    public interface IInputEvents  
    {
        event Action OnJump;
        event Action OnParkour;
        event Action OnPickUpItem;
         
        event Action<bool> OnActiveInventoryLootBox;
        event Action<bool> OnActiveInventory;
        event Action OnExitInventory;

        event Func<bool> OnHasWeapon;
        event Action OnReloadWeapon;
        event Action OnSetParentWeapon;
        event Action OnResetParenWeapon;
        void Initialize();
        void Dispose();
        void SetReloadWeaponAnimationState(bool isReload);
        void SetEquippWeaponAnimationState(bool isEquipping);
    }
}

