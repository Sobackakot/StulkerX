 
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Inventory_
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        private IInventoryController inventory;
        private IInventoryController inventoryEquip;
        private IInventoryController inventoryBox;
        private RectTransform transformSlot;

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
            transformSlot = GetComponent<RectTransform>();
        }

        public virtual void AddItemInSlot(ItemInSlot item, ItemScrObj data) //coll from class InventoryUI
        {
            item.SetItem(data);
        }
        public virtual void RemoveItemInSlot(ItemInSlot item)//coll from class InventoryUI
        {
            item.CleareItem();
        }
        public virtual void OnDrop(PointerEventData eventData)
        {
            ItemInSlot dropItem = eventData.pointerDrag.GetComponent<ItemInSlot>();
            ItemScrObj itemData = dropItem.dataItem;
            if (transformSlot?.childCount > 0 && itemData != null) DropItemInventory(itemData, dropItem);
        }
        public virtual void DropItemInventory(ItemScrObj itemData, ItemInSlot dropItem)
        {
            ItemInSlot pickItem = transformSlot.GetChild(0).GetComponent<ItemInSlot>();
            if (!CheckDropItemType(dropItem, pickItem)) return;
            ItemScrObj oldItemData = inventory.SwapItemFromInventory(itemData, pickItem.slotIndex);
            if (oldItemData != null) inventory.SwapItemFromInventory(oldItemData, dropItem.slotIndex);
        }
        public virtual bool CheckDropItemType(ItemInSlot dropItem, ItemInSlot pickItem)
        {
            Transform dropSlot = dropItem.originalSlot;
            if (dropSlot.gameObject.tag == "FastSlot") return false;
            if (dropSlot.gameObject.tag == "SlotBox" | dropSlot.gameObject.tag == "EquipSlot" && pickItem.dataItem != null) return false;
            if (UnEquip(dropItem, dropSlot.gameObject.tag)) return false;
            else return true;
        }
        private bool UnEquip(ItemInSlot dropItem, string slotType)
        {
            short index = inventory.GetIndexFreeSlot(dropItem.dataItem, slotType);

            if (slotType == "EquipSlot" && index != -1)
            {
                inventory.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventoryEquip.RemoveItemFromInventory(dropItem.dataItem);
                return true;
            }
            else if (slotType == "SlotBox" && index != -1)
            {
                inventory.UpdatePickItem(dropItem.dataItem, index, slotType);
                inventoryBox.RemoveItemFromInventory(dropItem.dataItem);
                return true;
            }
            else return false;
        }
    }
}

