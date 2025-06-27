
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "InventoryItems/EquipItem")]
public class EquipmentScrObj : ItemScrObj
{
    public EquipItemTypes itemType;
    public override bool IsCompatibleWithSlot(EquipFieldScrObj equipData)
    { 
        return (short)itemType == (short)equipData.fieldType;
    }
    public override bool IsEquipmentItem()
    {
        return (short)itemType != (short)EquipFieldTypes.None;
    }
    public override bool IsRequiredItem(EquipItemTypes itemType)
    {
        return (short)this.itemType == (short)itemType;
    }
}
public enum EquipItemTypes : short
{
    Helmet,
    ArmorVest,
    Backpack,
    Weapon_1, 
    Flashlight,
    Binoculars,
    Knife,
    Bolts,
    Grenades,
    PDA,
    Devices,
    Upgrade,
    None
}