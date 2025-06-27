 
using UnityEngine;
using Inventory_;

[CreateAssetMenu(fileName = "InventoryBox" , menuName = "Inventory/inventoryBox")]
public class InventoryBoxScrObj : ScriptableObject
{
    public InventoryBoxData inventoryBoxData;
    public InventoryBox inventoryBox;
    private void OnEnable()
    {
        if (inventoryBox == null)
        {
            inventoryBox = new InventoryBox();
        }
    }
}
