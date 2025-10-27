using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private bool isOpen;

    public LabelSystem LabelSystem;

    public bool canClose;

    GameObject selectedItem;
    public GameObject SelectedItem
    {
        get { return selectedItem; }
    }

    public static InventoryManager Instance { get; private set; }

    [SerializeField] private List<GameObject> gemsSlotsInventory = new List<GameObject>();
    public List<GameObject> gemsItemInventory = new List<GameObject>();

    [SerializeField] private List<GameObject> canesSlotsInventory = new List<GameObject>();
    public List<GameObject> canesItemInventory = new List<GameObject>();

    [SerializeField] private List<GameObject> mantleSlotsInventory = new List<GameObject>();
    public List<GameObject> mantleItemInventory = new List<GameObject>();

    public List<GameObject> gems
    {
        get { return gemsSlotsInventory; }
    }
    public List<GameObject> canes
    {
        get { return canesSlotsInventory; }
    }
    public List<GameObject> mantle
    {
        get { return mantleSlotsInventory; }

    }

    public bool IsOpen
    {
        get { return isOpen; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        canClose = true;
        PlayerMovement.InventoryOpenOrClosed += OpenInventory;
    }

    private void Update()
    {
        if (isOpen == false)
        {
            selectedItem = null;
        }
    }

    public void ItemIsSelected(GameObject item)
    {
        selectedItem = item;
    }
    public void OpenInventory()
    {
        isOpen = !isOpen;
    }
}
