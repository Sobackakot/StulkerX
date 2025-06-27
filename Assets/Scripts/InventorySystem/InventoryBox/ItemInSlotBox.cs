
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace Inventory_
{
    public class ItemInSlotBox : ItemInSlot
    {
        public short slotIndexBox { get; set; }

        private IInventoryController inventory;
        private IInventoryController inventoryEquip;
        private IInventoryController inventoryBox;

        private Transform originSlotBox;

        [Inject]
        private void Container([Inject(Id = "inventory")] IInventoryController inventory, [Inject(Id = "inventoryEquip")] IInventoryController inventoryEquip,
            [Inject(Id = "inventoryBox")] IInventoryController inventoryBox)
        {
            this.inventory = inventory;
            this.inventoryEquip = inventoryEquip;   
            this.inventoryBox = inventoryBox;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            originSlotBox = transform.parent;
            base.OnBeginDrag(eventData);
        }
        public override void OnDrag(PointerEventData eventData) //moves an item to the mouse cursor position
        {
            base.OnDrag(eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            originSlotBox = transform.parent;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            originSlotBox = transform.parent;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Equipping(originSlotBox.gameObject.tag);
            }
        }
        private void Equipping(string slotType)
        {
            short index = inventory.GetIndexFreeSlot(dataItem, slotType);
            short index2 = inventoryEquip.GetIndexFreeSlot(dataItem, slotType);

            if (index != -1)
            {
                inventory.UpdatePickItem(dataItem, index, slotType);
                inventoryBox.RemoveItemFromInventory(dataItem);
            }
            else if (index2 != -1)
            {
                ItemScrObj oldItem = inventoryEquip.UpdatePickItem(dataItem, index, slotType);
                inventoryBox.RemoveItemFromInventory(dataItem);
                if (oldItem != null) inventoryBox.AddItemToInventory(oldItem);
            }
        }
    }
}

