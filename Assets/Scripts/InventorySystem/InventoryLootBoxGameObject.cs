 
using UnityEngine;
using Zenject; 


namespace Inventory_
{
    public class InventoryLootBoxGameObject : MonoBehaviour
    {
        private InventoryLootBoxUI boxInventoryPanel;
        private InventoryCharUI playerInventoryPanel;

        public InventoryBoxScrObj inventoryBoxScrObj;
        private IInventoryController inventoryBox;

        [SerializeField] private Material materialBox;
        [Inject]
        private void Container([Inject(Id = "inventoryBox")] IInventoryController inventoryBox)
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
        public void OnActiveInventoryLootBox(bool isActive) // coll from raycast
        {
            boxInventoryPanel.gameObject.SetActive(isActive);
            playerInventoryPanel.gameObject.SetActive(isActive);
            inventoryBox.SetBoxByInventory(inventoryBoxScrObj);
            materialBox.color = Color.green;
        }
    }
}

