
using Inventory.Handler;
using StateData.Character;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Inventory.UI
{
    public class ItemInSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public short slotIndex { get; set; }
        public ItemScrObj dataItem { get; private set; }

        private RectTransform pickItemTransform;
        private Transform originalParent;
        private CanvasGroup canvasGroup;
        private Canvas canvas;
        public Transform originalSlot { get; private set; }

        private Image itemIcon;
        private TextMeshProUGUI itemAmount;

        private IInventoryHandler inventory;
        private IInventoryEquipmentHandler inventoryEquip;
        private IInventoryLootBoxHandler inventoryBox;

        private protected CharacterStateContext stateData;

        [Inject]
        private void Container(
            
            IInventoryHandler inventory, 
            IInventoryEquipmentHandler inventoryEquip,
            IInventoryLootBoxHandler inventoryBox, 
            CharacterStateContext stateData)
        {
            this.inventory = inventory;
            this.inventoryEquip = inventoryEquip;
            this.inventoryBox = inventoryBox;
            this.stateData = stateData;
        }

        private void Awake()
        {
            originalParent = GetComponent<Transform>();  //transform parent object
            pickItemTransform = GetComponent<RectTransform>();//current position of the item
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = pickItemTransform.GetComponentInParent<Canvas>(); //UI canvasEq with inventoryController

            itemIcon = GetComponent<Image>(); //image of the current item 
            itemAmount = pickItemTransform.GetChild(0).GetComponent<TextMeshProUGUI>(); //amount of current item
        }
        public virtual void SetItem(ItemScrObj newItem) // coll from InventorySlot
        {
            if (newItem == null) return;
            dataItem = newItem;
            itemAmount.text = dataItem.amount.ToString();
            itemIcon.sprite = dataItem.IconItem;
            itemIcon.enabled = true;
        }
        public virtual void CleareItem() // coll from InventorySlot
        {
            dataItem = null;
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            itemAmount.text = " ";
        }
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
            originalParent = transform.parent; //save the parent object of the item 
            originalSlot = transform.parent;

            pickItemTransform.SetParent(canvas.transform); //changing the parent object of an item
            pickItemTransform.SetAsLastSibling(); //sets item display priority  
        }
        public virtual void OnDrag(PointerEventData eventData) //moves an item to the mouse cursor position
        {
            pickItemTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            pickItemTransform.SetParent(originalParent); //returns the item to the original position of the parent object   
            originalSlot = transform.parent;
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            originalSlot = transform.parent;
            if (eventData.button == PointerEventData.InputButton.Left)
            { 
                Equipping(originalSlot.gameObject.tag); 
            }
        }
        private void Equipping(string slotType)
        { 
            short index = inventoryBox.GetIndexFreeSlot(dataItem, slotType);
            short index2 = inventoryEquip.GetIndexFreeSlot(dataItem, slotType);
            if (index != -1 && stateData.isActiveInventory)
            {
                inventoryBox.UpdatePickItem(dataItem, index, slotType);
                inventory.RemoveItemFromInventory(dataItem);
            }
            else if (index2 != -1)
            {
                ItemScrObj oldItem = inventoryEquip.UpdatePickItem(dataItem, index2, slotType);
                inventory.RemoveItemFromInventory(dataItem);
                if (oldItem != null) inventory.AddItemToInventory(oldItem);
            }
        }
    }
}

