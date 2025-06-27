
using UnityEngine; 
using Zenject;
using Inventory_;

public class PickUpItems : Interactable 
{   
    [field : SerializeField] public ItemScrObj item {  get; private set; }
    private IInventoryController inventory;

    [Inject]
    private void Container([Inject(Id = "inventory")] IInventoryController inventory)
    {
        this.inventory = inventory;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public override void Interaction()
    {   
        base.Interaction(); //interaction with default item
        PickUpItem(); //pick up item in inventoryController
    }
    private void PickUpItem()
    {   
        if(!item.isDefaultItem)
        {
            bool isPickUp = inventory.AddItemToInventory(item);
            if (isPickUp)
            {
                Destroy(gameObject);
            }
        } 
    }
    public bool IsWeapon()
    {
        if (item.IsRequiredItem(EquipItemTypes.Weapon_1))
            return true;
        else return false;
    }
}
      
