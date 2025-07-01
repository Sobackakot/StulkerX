 
using UnityEngine;
using Zenject;
using Inventory.Handler;


namespace Inventory
{
    public class InventoryLootBoxGameObject : MonoBehaviour
    {
        private InventoryLootBoxUI boxInventoryPanel;
        private InventoryCharUI playerInventoryPanel;

        public InventoryBoxScrObj inventoryBoxScrObj;
        private IInventoryLootBoxHandler inventoryBox;

        [SerializeField] private Material materialBox;
        [Inject]
        private void Container(IInventoryLootBoxHandler inventoryBox)
        {
            this.inventoryBox = inventoryBox;
        }

        private void Awake()
        {
            boxInventoryPanel = FindAnyObjectByType<InventoryLootBoxUI>();
            playerInventoryPanel = FindAnyObjectByType<InventoryCharUI>();
            inventoryBoxScrObj.inventoryBoxData.SetNewInventoryBoxId();
        }
        private void Start()
        {
            materialBox.color = Color.red;
        }
        public void ActiveInventoryUIPanel(bool isActive) // coll from raycast
        {
            boxInventoryPanel.gameObject.SetActive(isActive);
            playerInventoryPanel.gameObject.SetActive(isActive);
            inventoryBox.SetBoxByInventory(inventoryBoxScrObj);
            materialBox.color = Color.green;
        }
    }
}

