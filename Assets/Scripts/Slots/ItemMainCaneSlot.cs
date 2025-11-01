using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMainCaneSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Cane Item;
    [SerializeField] private Image iconCanePlace;
    [SerializeField] private PlayerCaneAttack playerCaneAttack;
    public Cane _Item
    {
        get { return Item; }
        set
        {
            if (value != null)
            {
                if (value.GetComponent<Cane>()) Item = value;
                playerCaneAttack.SetNewItem(value.gameObject);
            }
            else
            {
                playerCaneAttack.SetNewItem(null);
            }
        }
    }
    private void Update()
    {
        if (transform.childCount > 0)
        {
            _Item = GetComponentInChildren<Cane>(includeInactive: true);
            iconCanePlace.sprite = _Item.Icon;
        }
        else
        {
            _Item = null;
            iconCanePlace.sprite = null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();
            if (draggableItem.GetComponent<Cane>())
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

            if (draggableItem.GetComponent<Cane>())
            {
                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}
