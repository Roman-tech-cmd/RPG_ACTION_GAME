using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragebleItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image itemImage;

    public bool isSelected;

    public enum TypeItemSlot
    {
        None,
        Gem,
        Cane,
        Mantle,
        Belt
    }
    [SerializeField] private TypeItemSlot typeItem;

    public TypeItemSlot TypeItem
    {
        get { return typeItem; }
    }

    public void SelectItem()
    {
        InventoryManager.Instance.UnselectedItems();
        isSelected = !isSelected;
        InventoryManager.Instance.ItemIsSelected(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            InventoryManager.Instance.canClose = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            GetComponent<Image>().raycastTarget = false;
            itemImage.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            InventoryManager.Instance.canClose = false;
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            InventoryManager.Instance.canClose = true;
            ItemToBack();
        }
    }

    private void ItemToBack()
    {
        transform.SetParent(parentAfterDrag);
        GetComponent<Image>().raycastTarget = true;
        itemImage.raycastTarget = true;
    }
}
