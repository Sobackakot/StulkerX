 
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace Inventory_
{
    public class InventorySlotBox : InventorySlot
    {
        private IInventoryController inventory;
        private IInventoryController inventoryEquip;
        private IInventoryController inventoryBox;

        private RectTransform transformBoxSlot;

        [Inject]
        private void Container([Inject(Id = "inventory")] IInventoryController inventory, [Inject(Id = "inventoryEquip")] IInventoryController inventoryEquip,
            [Inject(Id = "inventoryBox")] IInventoryController inventoryBox)
        {
           this.inventory = inventory;
            this.inventoryEquip = inventoryEquip;
            this.inventoryBox = inventoryBox;
        }
        private void Awake()
        {
            transformBoxSlot = GetComponent<RectTransform>();
        }

        public override void AddItemInSlot(ItemInSlot item, ItemScrObj data) //coll from class InventoryUI
        {
            base.AddItemInSlot(item, data);
        }
        public override void RemoveItemInSlot(ItemInSlot item)//coll from class InventoryUI
        {
            base.RemoveItemInSlot(item);
        }
        public override void OnDrop(PointerEventData eventData)
        {
            ItemInSlot dropItem = eventData.pointerDrag.GetComponent<ItemInSlot>();
            ItemScrObj itemData = dropItem.dataItem;
            if (transformBoxSlot?.childCount > 0 && itemData != null)
            {
                ItemInSlot pickItem = transformBoxSlot.GetChild(0).GetComponent<ItemInSlot>();
                if (!CheckDropItemType(dropItem, pickItem)) return;
            }
            base.OnDrop(eventData);
        }

        public override void DropItemInventory(ItemScrObj itemData, ItemInSlot dropItem)
        {
            base.DropItemInventory(itemData, dropItem);
        }
        public override bool CheckDropItemType(ItemInSlot dropItem, ItemInSlot pickItem)
        {
            Transform dropSlot = dropItem.originalSlot;
            if (dropSlot.gameObject.tag == "FastSlot") return false;
            if (dropSlot.gameObject.tag == "Slot" | dropSlot.gameObject.tag == "EquipSlot" && pickItem.dataItem != null) return false;
            if (UnEquip(dropItem, dropSlot.gameObject.tag)) return false;
            else return true;
        }
        private bool UnEquip(ItemInSlot dropItem, string slotType)
        {
            short index = inventoryBox.GetIndexFreeSlot(dropItem.dataItem, slotType);

            if (slotType == "EquipSlot" && index != -1)
            {
                inventoryBox.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventoryEquip.RemoveItemFromInventory(dropItem.dataItem);
                return true;
            }
            else if (slotType == "Slot" && index != -1)
            {
                inventoryBox.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventory.RemoveItemFromInventory(dropItem.dataItem);
                return true;
            }
            else return false;
        }
    }
}

