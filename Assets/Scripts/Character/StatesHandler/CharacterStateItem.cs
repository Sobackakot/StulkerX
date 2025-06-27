using State.Character;
using System;

public class CharacterStateItem
{

    
    public bool isRayHitToItem { get; private set; }
     
    public void SetStateHitToItem(bool isHit)
    {
        isRayHitToItem = isHit;
    }
    public void InputCharacter_OnPickUpItem(PickUpItemEvent pickEvent)
    {
    //    if (isRayHitToItem && !stateGame.Weapon.isReload)
    //    {
    //        OnPickUpItemAnim?.Invoke();
    //        if (OnGetItemFromHitRay != null && OnGetItemFromHitRay.Invoke())
    //        {
    //            stateGame.Weapon.SetAvailableWeapon(true);
    //        }
    //    }
    }
}
