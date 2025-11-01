using UnityEngine;

public class Gem : BaseItem
{
    public override void AddItemToInventory(GameObject worldItem)
    {
        InventoryManager inventoryManager = InventoryManager.Instance;

        for (int i = 0; i < inventoryManager.gems.Count; i++)
        {
            if (inventoryManager.gems[i].GetComponent<ItemSlot>()._Item == null)
            {
                inventoryManager.gems[i].GetComponent<ItemSlot>()._Item = prefabUI;
                GameObject pikedItem = Instantiate(prefabUI, InventoryManager.Instance.gems[i].transform);
                RandomGenerationAttacksStats.Instance.TransferStats(worldItem.GetComponent<BaseItem>(), pikedItem.GetComponent<BaseItem>());
                break;
            }
        }
    }

    public override void AddItemToList()
    {
        InventoryManager.Instance.gemsItemInventory.Add(gameObject);
    }
    public override bool CanPickUp()
    {
        if (InventoryManager.Instance.IsOpen == false)
        {
            if (InventoryManager.Instance.gemsItemInventory.Count < InventoryManager.Instance.gems.Count)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
}
