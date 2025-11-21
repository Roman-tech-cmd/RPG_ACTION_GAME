using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject Item;

    public GameObject _Item
    {
        get { return Item; }
        set
        {
            Item = value;
            InventoryManager.Instance.LabelSystem.CheckInventory();
        }
    }

    [SerializeField] private StaticItemCharacteristicClass.CategoryItem typeSlot;

    public StaticItemCharacteristicClass.CategoryItem TypeSlot
    {
        get { return typeSlot; }
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            _Item = GetComponentInChildren<DragebleItem>(includeInactive: true).gameObject;
        }
        else
        {
            _Item = null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            if (transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();
                if (typeSlot == StaticItemCharacteristicClass.CategoryItem.None)
                {
                    Debug.Log("PoFIk");
                    draggableItem.parentAfterDrag = transform;
                }
                else if ((int)typeSlot == (int)draggableItem.CategoryItem)
                {
                    Debug.Log("GOOD");
                    draggableItem.parentAfterDrag = transform;
                }
                else
                {
                    Debug.Log("BAD");
                }

            }
            else
            {
                GameObject dropped = eventData.pointerDrag;
                DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();

                GameObject current = transform.GetChild(0).gameObject;
                DragebleItem currentDraggable = current.GetComponent<DragebleItem>();

                if (typeSlot == StaticItemCharacteristicClass.CategoryItem.None)
                {
                    Debug.Log("PoFIk");
                    currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                    draggableItem.parentAfterDrag = transform;
                }
                else if ((int)typeSlot == (int)draggableItem.CategoryItem)
                {
                    Debug.Log("GOOD");
                    currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                    draggableItem.parentAfterDrag = transform;
                }
                else
                {
                    Debug.Log("BAD");
                }
            }
        }
    }

    public bool HasItem()
    {
        return Item != null;
    }

    public void AddSlotItemToList()
    {
        if (_Item != null)
        {
            _Item.GetComponent<BaseItem>().AddItemToList();
        }
    }

}
