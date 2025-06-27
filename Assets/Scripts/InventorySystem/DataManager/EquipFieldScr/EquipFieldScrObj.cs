using UnityEngine;

[CreateAssetMenu(fileName = "EquipFieldScrObj", menuName = "InventoryFields/EquipFieldScrObj")]
public class EquipFieldScrObj : ScriptableObject
{
    public EquipField equipField;
    public EquipFieldTypes fieldType;
}
public enum EquipFieldTypes : short
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
