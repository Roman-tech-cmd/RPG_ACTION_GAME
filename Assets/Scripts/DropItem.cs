using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private LayerMask layerBarrier;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && inventoryManager.SelectedItem != null)
        {
            GameObject selectedItem = inventoryManager.SelectedItem;
            GameObject worldItem = selectedItem.GetComponent<BaseItem>().PrefabWorld;
            Drop(selectedItem, worldItem, 0);
        }
    }

    private void Drop(GameObject UIitem, GameObject worldItem, int numTries)
    {
        int count = numTries;
        Vector2 dropPosition = DropItemInDonut(0.8f, 1.3f);
        if (!Physics2D.OverlapCircle(dropPosition, worldItem.GetComponent<Collider2D>().bounds.extents.x, layerBarrier))
        {
            GameObject droppedItem = Instantiate(worldItem, dropPosition, Quaternion.identity);
            if (droppedItem.GetComponent<BaseItem>().TypeItem == StaticItemCharacteristicClass.typeItem.Weapons)
            {
                RandomGenerationAttacksStats.Instance.TransferStats(UIitem.GetComponent<BaseItem>(), droppedItem.GetComponent<BaseItem>());
            }
            else
            {
                RandomGenerationArmorStats.Instance.TransferStats(UIitem.GetComponent<BaseItem>(), droppedItem.GetComponent<BaseItem>());

            }

            Destroy(UIitem);
        }
        else if (count < 100)
        {
            count += 1;
            Debug.Log("Препятствие");
            Drop(UIitem, worldItem, count);
        }
        else
        {
            Debug.Log("Предмет не может выпасть, так как нет места рядом с игроком");
        }
    }
    public Vector2 DropItemInDonut(float minRadius, float maxRadius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);

        float radius = Random.Range(minRadius, maxRadius);

        Vector2 dropPos = (Vector2)transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        return dropPos;
    }


}
