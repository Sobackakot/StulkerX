 
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace Inventory_
{
    public class EquipmentSlot : InventorySlot
    {
        public EquipFieldScrObj equipFieldData;
        private IInventoryController inventory;
        private IInventoryController inventoryEquip;
        private IInventoryController inventoryBox;
        private RectTransform equipmentSlot;

        [Inject]
        private void Container([Inject(Id = "inventory")] IInventoryController inventory, [Inject(Id = "inventoryEquip")] IInventoryController inventoryEquip,
            [Inject(Id = "inventoryBox")] IInventoryController inventoryBox)
        {
            this.inventory = inventory;
            this.inventoryBox = inventoryBox;
            this.inventoryEquip = inventoryEquip;
        }
        private void Awake()
        {
            equipmentSlot = GetComponent<RectTransform>();
        }
        public override void AddItemInSlot(ItemInSlot item, ItemScrObj data)
        {
            base.AddItemInSlot(item, data);
        }
        public override void RemoveItemInSlot(ItemInSlot item)
        {
            base.RemoveItemInSlot(item);
        }
        public override void OnDrop(PointerEventData eventData)
        {
            ItemInSlot droppedItem = eventData.pointerDrag.GetComponent<ItemInSlot>();
            if (droppedItem.dataItem == null || !droppedItem.dataItem.IsCompatibleWithSlot(equipFieldData) || droppedItem == null) return;
            if (!CheckDropItemType(droppedItem)) return;
            base.OnDrop(eventData);
        }
        private bool CheckDropItemType(ItemInSlot dropItem)
        {
            Transform dropSlot = dropItem.originalSlot;
            if (dropSlot.gameObject.tag == "Slot" || dropSlot.gameObject.tag == "SlotBox" && equipmentSlot.gameObject.tag == "EquipSlot")
            {
                if (Equipping(dropItem, dropSlot.gameObject.tag)) return false;
                else return true;
            }
            else return true;
        }
        private bool Equipping(ItemInSlot dropItem, string slotType)
        {
            short index = inventoryEquip.GetIndexFreeSlot(dropItem.dataItem, slotType);

            if (slotType == "Slot" && index != -1)
            {
                ItemScrObj oldItem = inventoryEquip.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventory.RemoveItemFromInventory(dropItem.dataItem);
                if (oldItem != null) inventory.AddItemToInventory(oldItem);
                return true;
            }
            else if (slotType == "SlotBox" && index != -1)
            {
                ItemScrObj oldItem = inventoryEquip.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventoryBox.RemoveItemFromInventory(dropItem.dataItem);
                if (oldItem != null) inventoryBox.AddItemToInventory(oldItem);
                return true;
            }
            else return false;
        }
    }

}
