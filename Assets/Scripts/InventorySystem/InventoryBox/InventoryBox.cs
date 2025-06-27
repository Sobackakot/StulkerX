 
using System.Collections.Generic;
using UnityEngine;

namespace Inventory_
{
    public class InventoryBox
    {
        public readonly List<ItemScrObj> itemsInventoryBox;
        private short space = 48;
        public InventoryBox()
        {
            itemsInventoryBox = new List<ItemScrObj>(space);
            for (short i = 0; i < space; i++)
            {
                itemsInventoryBox.Add(null); // InitializeMoveState the list with null values  
            }
        }

        public bool AddItemToInventory(ItemScrObj newItem, out short indexSlot) //coll from InventoryController
        {
            for (short i = 0; i < itemsInventoryBox.Count; i++)
            {
                if (itemsInventoryBox[i] == null)
                {
                    itemsInventoryBox[i] = newItem;
                    indexSlot = i;
                    return true;
                }
            }
            indexSlot = -1;
            return false; // InventoryPerson is full
        }

        public void RemoveItemFromInventory(ItemScrObj newItem, out short slotIndex) // coll from class InventoryController
        {
            for (short i = 0; i < itemsInventoryBox.Count; i++)
            {
                if (itemsInventoryBox[i] == newItem)
                {
                    itemsInventoryBox[i] = null;
                    slotIndex = i;
                    return;
                }
            }
            slotIndex = -1;
        }
        public void SwapItemFromInventory(ItemScrObj item, short index)
        {
            itemsInventoryBox[index] = item;
        }
        public void FreeUpOldSlot(short index)
        {
            itemsInventoryBox[index] = null;
        }
        public List<ItemScrObj> GetCurrentItems() //get a list of items from a charInspector's inventoryController
        {
            return itemsInventoryBox;
        }

    }
}

