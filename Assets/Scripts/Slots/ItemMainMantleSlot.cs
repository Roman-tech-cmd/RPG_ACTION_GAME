using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMainMantleSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Mantle Item;
    [SerializeField] private Image iconMantlePlace;
    [SerializeField] private PlayerGemAttack playerMantleAttack;
    public Mantle _Item
    {
        get { return Item; }
        set
        {
            if (value != null)
            {
                if (value.GetComponent<Mantle>()) Item = value;
                playerMantleAttack.SetNewItem(value.gameObject);
            }
            else
            {
                playerMantleAttack.SetNewItem(null);
            }
        }
    }
    private void Update()
    {
        if (transform.childCount > 0)
        {
            _Item = GetComponentInChildren<Mantle>(includeInactive: true);
            iconMantlePlace.sprite = _Item.Icon;
        }
        else
        {
            _Item = null;
            iconMantlePlace.sprite = null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();
            if (draggableItem.GetComponent<Mantle>())
            {
                draggableItem.parentAfterDrag = transform;
            }
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();

            GameObject current = transform.GetChild(0).gameObject;
            DragebleItem currentDraggable = current.GetComponent<DragebleItem>();

            if (draggableItem.GetComponent<Mantle>())
            {
                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}
