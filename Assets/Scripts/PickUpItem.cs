using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    private Queue<GameObject> pickUpQueue = new Queue<GameObject>();
    private bool isPickingUp = false;

    [SerializeField] private LabelSystem labelSystem;

    private void Update()
    {
        if (!isPickingUp && pickUpQueue.Count > 0)
        {
            StartCoroutine(ProcessPickUpCoroutine(pickUpQueue.Dequeue()));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (collision.gameObject.GetComponent<BaseItem>().CanPickUp())
            {
                pickUpQueue.Enqueue(collision.gameObject);
            }
        }
    }

    private System.Collections.IEnumerator ProcessPickUpCoroutine(GameObject item)
    {
        isPickingUp = true;

        item.GetComponent<BaseItem>().AddItemToInventory(item);

        Destroy(item);

        float waitTime = 0.06f;
        yield return new WaitForSeconds(waitTime);

        isPickingUp = false;
    }
}

