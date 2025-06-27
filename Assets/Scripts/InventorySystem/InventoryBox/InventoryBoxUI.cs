using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory_
{
    public class InventoryBoxUI : MonoBehaviour, IInventoryUI
    {
        private List<ItemInSlotBox> itemsInSlotsBox = new List<ItemInSlotBox>();
        private List<InventorySlotBox> inventorySlotsBox = new List<InventorySlotBox>();

        public event Func<List<ItemScrObj>> onSetNewItem;
 
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None; 
        }
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked; 
        }
        private void Awake()
        {
            itemsInSlotsBox.AddRange(GetComponentsInChildren<ItemInSlotBox>(false));
            inventorySlotsBox.AddRange(GetComponentsInChildren<InventorySlotBox>(false));
        } 
        private void Start()
        {
            for (short i = 0; i < inventorySlotsBox.Count; i++)
            {
                itemsInSlotsBox[i].slotIndexBox = i;
            }
        }
        void IInventoryUI.SetNewItemByInventoryCell(ItemScrObj newItem, short slotIndex) //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            if (slotIndex < items.Count && items[slotIndex] != null) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                inventorySlotsBox[slotIndex].AddItemInSlot(itemsInSlotsBox[slotIndex], newItem);
            }
        }
        void IInventoryUI.ResetItemByInventoryCell(short slot) //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            if (slot < items.Count) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                inventorySlotsBox[slot].RemoveItemInSlot(itemsInSlotsBox[slot]);
            }
        }
        void IInventoryUI.UpdateInventorySlots() //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            for (short i = 0; i < inventorySlotsBox.Count; i++) //Updates the inventoryController UI completely when changing characters
            {
                if (itemsInSlotsBox[i].dataItem != null)
                {
                    inventorySlotsBox[i].RemoveItemInSlot(itemsInSlotsBox[i]);
                }
                if (i < items.Count && items[i] != null)
                {
                    inventorySlotsBox[i].AddItemInSlot(itemsInSlotsBox[i], items[i]);
                }
            }
        }

        short IInventoryUI.GetIndexFreeSlot(ItemScrObj item, string slotType)
        {
            if (slotType == "EquipSlot" && CheckFreeSlot(item, out short index))
            {
                return index;
            }
            else if (slotType == "Slot" && CheckFreeSlot(item, out short index2))
            {
                return index2;
            }
            return -1;
        }
        private bool CheckFreeSlot(ItemScrObj item, out short index)
        {
            for (short i = 0; i < inventorySlotsBox.Count; i++)
            {
                if (itemsInSlotsBox[i].dataItem == null && item != null)
                {
                    index = i; 
                    return true;
                }
            }
            index = -1; 
            return false;
        }
    }
}

