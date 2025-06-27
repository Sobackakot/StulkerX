
using System;
using UnityEngine;
 
public abstract class ItemScrObj : ScriptableObject
{ 
    public string Id { get; private set; }
    public int amount =1;
    public string NameItem;
    public Sprite IconItem;
    public float Weight;
    public bool isDefaultItem;
    public bool isStackable;

    private bool isInstaled;

    public abstract bool IsCompatibleWithSlot(EquipFieldScrObj equipData);
    public abstract bool IsEquipmentItem(); 
    public abstract bool IsRequiredItem(EquipItemTypes itemType);

    public void SetIdFromNewItem()
    {
        if (isInstaled)
        {
            Id = Guid.NewGuid().ToString();
            isInstaled = true;
        }
    } 
}

