using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Inventory.Handler;

namespace Inventory.UI
{
    public class EquipmentItemInSlot : ItemInSlot
    {
        private IInventoryHandler inventory;
        private IInventoryEquipmentHandler inventoryEquip;
        private IInventoryLootBoxHandler inventoryBox; 

        private Transform originEquipSlot;
        public int equipSlotIndex { get; set; }

        [Inject]
        private void Container(
            
            IInventoryHandler inventory, 
            IInventoryEquipmentHandler inventoryEquip,
            IInventoryLootBoxHandler inventoryBox)
        {
            this.inventory = inventory;
            this.inventoryEquip = inventoryEquip;
            this.inventoryBox = inventoryBox; 
        }
        public override void SetItem(ItemScrObj newItem)
        {
            base.SetItem(newItem);
        }
        public override void CleareItem()
        {
            base.CleareItem();
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            originEquipSlot = transform.parent;
            base.OnBeginDrag(eventData);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            originEquipSlot = transform.parent;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            originEquipSlot = transform.parent;
            if (eventData.button == PointerEventData.InputButton.Left && dataItem != null)
            {
                Equipping(originEquipSlot.gameObject.tag);
            }
        }
        private void Equipping(string slotType)
        {
            short index = inventory.GetIndexFreeSlot(dataItem, slotType);
            short index1 = inventoryBox.GetIndexFreeSlot(dataItem, slotType);
            if (index != -1)
            {
                inventory.UpdatePickItem(dataItem, index, slotType);
                inventoryEquip.RemoveItemFromInventory(dataItem);
            }
            else if (index1 != -1 && stateData.isActiveInventory)
            {
                inventoryBox.UpdatePickItem(dataItem, index, slotType);
                inventoryEquip.RemoveItemFromInventory(dataItem);
            }
        }
    }

}
