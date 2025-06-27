 
using System.Collections.Generic;

namespace Inventory_
{
    public interface IInventoryController
    {
        bool AddItemToInventory(ItemScrObj newItem);
        void RemoveItemFromInventory(ItemScrObj item);
        ItemScrObj SwapItemFromInventory(ItemScrObj item, short index);
        ItemScrObj UpdatePickItem(ItemScrObj pickItem, short index, string slotType);
        List<ItemScrObj> GetCurrentItems();
        short GetIndexFreeSlot(ItemScrObj item, string slotType);
        void SetBoxByInventory(InventoryBoxScrObj box);

    }
}

