using UnityEngine;

public class Cane : BaseItem
{
    public override void AddItemToInventory(GameObject worldItem)
    {
        InventoryManager inventoryManager = InventoryManager.Instance;

        for (int i = 0; i < inventoryManager.gems.Count; i++)
        {
            if (inventoryManager.canes[i].GetComponent<ItemSlot>()._Item == null)
            {
                inventoryManager.canes[i].GetComponent<ItemSlot>()._Item = prefabUI;
                GameObject pikedItem = Instantiate(prefabUI, InventoryManager.Instance.canes[i].transform);
                RandomGenerationAttacksStats.Instance.TransferStats(worldItem.GetComponent<BaseItem>(), pikedItem.GetComponent<BaseItem>());
                break;
            }
        }
    }

    public override void AddItemToList()
    {
        InventoryManager.Instance.canesItemInventory.Add(gameObject);
    }


    public override bool CanPickUp()
    {
        if (InventoryManager.Instance.IsOpen == false)
        {
            if (InventoryManager.Instance.canesItemInventory.Count < InventoryManager.Instance.canes.Count)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
}
