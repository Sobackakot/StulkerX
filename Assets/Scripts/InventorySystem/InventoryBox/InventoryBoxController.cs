using Inventory.UI;
using System.Collections.Generic;

namespace Inventory.Handler
{
    public class InventoryBoxController : IInventoryLootBoxHandler
    {
        public InventoryBoxController(IInvetnoryLootBoxUI inventoryBoxUI)
        {
            this.inventoryBoxUI = inventoryBoxUI;
            this.inventoryBoxUI.onSetNewItem += GetCurrentItems;
        }
        ~InventoryBoxController()
        {
            inventoryBoxUI.onSetNewItem -= GetCurrentItems;
        }

        private IInvetnoryLootBoxUI inventoryBoxUI;
        public InventoryBox inventoryBox;
         
        
        void IInventoryHandlerBase.SetBoxByInventory(InventoryBoxScrObj box) // coll from class CharacterSwitchSystem
        {
            inventoryBox = box.inventoryBox; // get pick Box for inventory
            inventoryBoxUI.UpdateInventorySlots();
        }
        bool IInventoryHandlerBase.AddItemToInventory(ItemScrObj newItem) //coll from EquipmentController,CharacterState_GetItemFromHitRay
        {
            bool isHas = inventoryBox.AddItemToInventory(newItem, out short index);
            inventoryBoxUI.SetNewItemByInventoryCell(newItem, index);
            return isHas;
        }
        void IInventoryHandlerBase.RemoveItemFromInventory(ItemScrObj item) // coll from ItemInSlot
        {
            inventoryBox.RemoveItemFromInventory(item, out short index);
            inventoryBoxUI.ResetItemByInventoryCell(index);// update inventoryController equipmentSlots 
        }
        public ItemScrObj SwapItemFromInventory(ItemScrObj item, short index)
        {
            List<ItemScrObj> items = GetCurrentItems();
            if (index >= 0 && index < items.Count)
            {
                FreeUpOldSlot(item);
                ItemScrObj oldItem = null;
                if (items[index] != null)
                {
                    oldItem = items[index];
                }
                inventoryBox.SwapItemFromInventory(item, index);
                inventoryBoxUI.SetNewItemByInventoryCell(item, index);
                return oldItem;
            }
            else return null;
        }

        private void FreeUpOldSlot(ItemScrObj item)
        {
            List<ItemScrObj> items = GetCurrentItems();
            for (short i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    inventoryBoxUI.ResetItemByInventoryCell(i);
                    inventoryBox.FreeUpOldSlot(i);
                }
            }
        }
        ItemScrObj IInventoryHandlerBase.UpdatePickItem(ItemScrObj pickItem, short index, string slotType)
        {
            if (slotType == "EquipSlot" && pickItem != null && pickItem.IsEquipmentItem())
            {
                return SwapItemFromInventory(pickItem, index);
            }
            else if (slotType == "Slot" && pickItem != null)
            {
                return SwapItemFromInventory(pickItem, index);
            }
            return null;
        }
        public List<ItemScrObj> GetCurrentItems() //get a list of items from a charInspector's inventoryController
        {
            return inventoryBox.GetCurrentItems();//
        }
        short IInventoryHandlerBase.GetIndexFreeSlot(ItemScrObj item, string slotType)
        {
            return inventoryBoxUI.GetIndexFreeSlot(item, slotType);
        }
    }

}
