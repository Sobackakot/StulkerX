
using State.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Inventory_
{
    public class EquipmentUI : MonoBehaviour, IInventoryUI
    {
        private List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
        private List<EquipmentItemInSlot> equipItemInSlots = new List<EquipmentItemInSlot>();
         

        public event Func<List<ItemScrObj>> onSetNewItem;
        
        private void Awake()
        {
            equipmentSlots.AddRange(GetComponentsInChildren<EquipmentSlot>(false));
            equipItemInSlots.AddRange(GetComponentsInChildren<EquipmentItemInSlot>(false));
        } 
        private void Start()
        {
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                equipItemInSlots[i].equipSlotIndex = i;
            }
        }
        void IInventoryUI.SetNewItemByInventoryCell(ItemScrObj newItem, short slotIndex)
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            if (slotIndex < items.Count && items[slotIndex] != null) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                equipmentSlots[slotIndex].AddItemInSlot(equipItemInSlots[slotIndex], newItem);
            }
        }
        void IInventoryUI.ResetItemByInventoryCell(short slot)
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            if (slot < items.Count) //updates the inventoryController user interface, those equipmentSlots that have been changed
            {
                equipmentSlots[slot].RemoveItemInSlot(equipItemInSlots[slot]);
            }
        }
        void IInventoryUI.UpdateInventorySlots()
        {
            List<ItemScrObj> items = onSetNewItem?.Invoke();
            for (short i = 0; i < equipmentSlots.Count; i++) //Updates the inventoryController UI completely when changing characters
            {
                if (equipItemInSlots[i].dataItem != null)
                {
                    equipmentSlots[i].RemoveItemInSlot(equipItemInSlots[i]);
                }
                if (i < items.Count && items[i] != null)
                {
                    equipmentSlots[i].AddItemInSlot(equipItemInSlots[i], items[i]);
                }
            }
        }
        short IInventoryUI.GetIndexFreeSlot(ItemScrObj item, string slotType)
        {
            if (slotType == "Slot" && CheckFreeSlot(item, out short index))
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
            for (short i = 0; i < equipmentSlots.Count; i++)
            {
                if (item != null && item.IsCompatibleWithSlot(equipmentSlots[i].equipFieldData))
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

