 
using State.Character;
using System;
using UnityEngine;

public class CharacterStateInventory  
{ 
      
    public bool isActiveInvent { get; private set; }
    public bool isActiveInventBox { get; private set; }
    public bool isRayHitToInventoryBox { get; private set; }
   
    public void SetStateHitToInventory(bool isHit)
    {
        isRayHitToInventoryBox = isHit;
    }
    public void InputCharacter_OnExitInventory(InventoryExitEvent exitEvent)
    {
        isActiveInvent = false;
        //OnExitInventory?.Invoke();
        //OnActiveInventory?.Invoke(isActiveInvent);
    }
    public void InputCharacter_OnSearcheInventoryBox(InventoryLootBoxActiveEvent activeEvent)
    {
        if (isRayHitToInventoryBox)
        {
            isActiveInvent = !isActiveInvent;
            activeEvent.inputValue = isActiveInvent;
            //OnSearcheInventoryBox?.Invoke(activeEvent.inputValue);
        }
    }
    public void InputCharacter_OnOppenInventory(InventoryActiveEvent oppenEvent)
    {
        isActiveInvent = !isActiveInvent; 
        //OnActiveInventory?.Invoke(isActiveInvent);
        Debug.Log("inventory isActive state " + isActiveInvent);
    }
   
}
