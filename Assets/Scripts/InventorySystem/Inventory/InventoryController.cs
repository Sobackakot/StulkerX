using Inventory.UI;
using System.Collections.Generic;

namespace Inventory.Handler
{ 
    public class InventoryController : IInventoryHandler
    {
        public InventoryController(IInventoryUI inventoryUI)
        {
            this.inventoryUI = inventoryUI;

            itemsInventory = new List<ItemScrObj>(space);
            for (int i = 0; i < space; i++)
            {
                itemsInventory.Add(null); // InitializeMoveState the list with null values 
            }
            this.inventoryUI.onSetNewItem += GetCurrentItems;
        }
        ~InventoryController()// не работает bind ---------------------------------------------------------------------------------
        {
            inventoryUI.onSetNewItem -= GetCurrentItems;
        }

        private IInventoryUI inventoryUI;

        public readonly List<ItemScrObj> itemsInventory;
        private int space = 48;
         
        bool IInventoryHandlerBase.AddItemToInventory(ItemScrObj newItem) //coll from EquipmentController,CharacterState_GetItemFromHitRay
        {
            for (byte i = 0; i < itemsInventory.Count; i++)
            {
                if (itemsInventory[i] == null)
                {
                    // update inventoryController equipmentSlots 
                    itemsInventory[i] = newItem;
                    inventoryUI.SetNewItemByInventoryCell(newItem, i);
                    return true;
                }
            }
            return false; // InventoryPerson is full 
        }
        void IInventoryHandlerBase.RemoveItemFromInventory(ItemScrObj item) // coll from ItemInSlot
        { 
            for (byte i = 0; i < itemsInventory.Count; i++)
            {
                if (itemsInventory[i] == item)
                {
                    itemsInventory[i] = null;
                    inventoryUI.ResetItemByInventoryCell(i);// update inventoryController equipmentSlots  
                    return;
                }
            }
        }
        public ItemScrObj SwapItemFromInventory(ItemScrObj item, short index)
        {
            if (index >= 0 && index < itemsInventory.Count)
            {
                FreeUpOldSlot(item);
                ItemScrObj oldItem = null;
                if (itemsInventory[index] != null)
                {
                    oldItem = itemsInventory[index];
                }
                itemsInventory[index] = item;
                inventoryUI.SetNewItemByInventoryCell(item, index);
                return oldItem;
            }
            else return null;
        }

        private void FreeUpOldSlot(ItemScrObj item)
        {
            for (short i = 0; i < itemsInventory.Count; i++)
            {
                if (itemsInventory[i] == item)
                {
                    inventoryUI.ResetItemByInventoryCell(i);
                    itemsInventory[i] = null;
                }
            }
        }
        ItemScrObj IInventoryHandlerBase.UpdatePickItem(ItemScrObj pickItem, short index, string slotType)
        {
            if (slotType == "EquipSlot" && pickItem != null && pickItem.IsEquipmentItem())
            {
                return SwapItemFromInventory(pickItem, index);
            }
            else if (slotType == "SlotBox" && pickItem != null)
            {
                return SwapItemFromInventory(pickItem, index);
            }
            return null;
        }

        public List<ItemScrObj> GetCurrentItems() //get a list of items from a charInspector's inventoryController
        {
            return itemsInventory;
        }
        short IInventoryHandlerBase.GetIndexFreeSlot(ItemScrObj item, string slotType)
        {
            return inventoryUI.GetIndexFreeSlot(item, slotType);
        }

        void IInventoryHandlerBase.SetBoxByInventory(InventoryBoxScrObj box)
        { 
        }
    }
}

