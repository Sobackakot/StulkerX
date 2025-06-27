

using State.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Inventory_
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        private List<ItemInSlot> itemsInSlots = new List<ItemInSlot>();
        private List<InventorySlot> inventorySlots = new List<InventorySlot>();

        public event Func<List<ItemScrObj>> onSetNewItem;
         
        private void Awake()
        {
            itemsInSlots.AddRange(GetComponentsInChildren<ItemInSlot>(false));
            inventorySlots.AddRange(GetComponentsInChildren<InventorySlot>(false));
        }
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None; 
        }
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked; 

        }
        private void Start()
        {
            for (short i = 0; i < inventorySlots.Count; i++)
            {
                itemsInSlots[i].slotIndex = i;
            }
        }
        void IInventoryUI.SetNewItemByInventoryCell(ItemScrObj newItem, short slotIndex) //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            if (slotIndex < items.Count && items[slotIndex] != null) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                inventorySlots[slotIndex].AddItemInSlot(itemsInSlots[slotIndex], newItem);
            }
        }
        void IInventoryUI.ResetItemByInventoryCell(short slot) //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke(); 
            if (slot < items.Count) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                inventorySlots[slot].RemoveItemInSlot(itemsInSlots[slot]);
            }
        }
        void IInventoryUI.UpdateInventorySlots() //coll from InventoryController
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            for (short i = 0; i < inventorySlots.Count; i++) //Updates the inventoryController UI completely when changing characters
            {
                if (itemsInSlots[i].dataItem != null)
                {
                    inventorySlots[i].RemoveItemInSlot(itemsInSlots[i]);
                }
                if (i < items.Count && items[i] != null)
                {
                    inventorySlots[i].AddItemInSlot(itemsInSlots[i], items[i]);
                }
            }
        }

        short IInventoryUI.GetIndexFreeSlot(ItemScrObj item, string slotType)
        {
            if (slotType == "EquipSlot" && CheckFreeSlot(item, out short index))
            {
                return index;
            }
            else if (slotType == "SlotBox" && CheckFreeSlot(item, out short index2))
            {
                return index2;
            }
            return -1;
        }
        private bool CheckFreeSlot(ItemScrObj item, out short index)
        {
            for (short i = 0; i < inventorySlots.Count; i++)
            {
                if (itemsInSlots[i].dataItem == null && item != null)
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

