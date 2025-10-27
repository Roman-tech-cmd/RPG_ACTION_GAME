using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMainGemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Gem Item;
    [SerializeField] private Image iconGemPlace;
    [SerializeField] private PlayerGemAttack playerGemAttack;
    public Gem _Item
    {
        get { return Item; }
        set
        {
            if (value != null)
            {
                if (value.GetComponent<Gem>()) Item = value;
                playerGemAttack.SetNewItem(value.gameObject);
            }
            else
            {
                playerGemAttack.SetNewItem(null);
            }
        }
    }
    private void Update()
    {
        if (transform.childCount > 0)
        {
            _Item = GetComponentInChildren<Gem>(includeInactive: true);
            iconGemPlace.sprite = _Item.Icon;
        }
        else
        {
            _Item = null;
            iconGemPlace.sprite = null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragebleItem draggableItem = dropped.GetComponent<DragebleItem>();
            if (draggableItem.GetComponent<Gem>())
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

            if (draggableItem.GetComponent<Gem>())
            {
                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}
