using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragebleItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] private Image itemImage;

    public bool isSelected;

    private StaticItemCharacteristicClass.CategoryItem categoryItem;
    public StaticItemCharacteristicClass.CategoryItem CategoryItem
    {
        get { return categoryItem; }
        set { categoryItem = value; }
    }

    private void Start()
    {
    }

    public void SelectItem()
    {
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
