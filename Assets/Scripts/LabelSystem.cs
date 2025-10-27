using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabelSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] labels;

    [SerializeField] private GameObject[] inventories;

    [SerializeField] private DescriptionSystem descriptionSystem;

    public ItemSlot[] allSlots;

    GameObject ActiveLabel;
    int numLabel;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CountItemsWithDelay());
    }


    private System.Collections.IEnumerator CountItemsWithDelay()
    {
        foreach (GameObject inv in inventories)
        {
            bool wasActive = inv.activeSelf;

            inv.SetActive(true);
            yield return null;

            inv.SetActive(wasActive);
        }
        yield return null;
        yield return null;
    }

    void Start()
    {
        ActiveLabel = labels[0];
        inventories[0].SetActive(true);
        ActiveLabel.GetComponent<Image>().color = Color.red;
    }
    private void Update()
    {

    }

    public void ActivateLabel(GameObject Label)
    {
        descriptionSystem.ClearInfo();
        ActiveLabel = Label;

        for (int i = 0; i < labels.Length; i++)
        {
            if (labels[i] == ActiveLabel)
            {
                numLabel = i;
                labels[i].GetComponent<Image>().color = Color.red;

            }
            else
            {
                labels[i].GetComponent<Image>().color = Color.white;
            }
        }

        for (int i = 0; i < inventories.Length; i++)
        {
            if (i != numLabel)
            {
                inventories[i].SetActive(false);
            }
            else
            {
                inventories[i].SetActive(true);
            }
        }
    }

    public void CheckInventory()
    {
        InventoryManager.Instance.canesItemInventory.Clear();
        InventoryManager.Instance.gemsItemInventory.Clear();
        InventoryManager.Instance.mantleItemInventory.Clear();

        foreach (GameObject Label in inventories)
        {
            allSlots = Label.GetComponentsInChildren<ItemSlot>(includeInactive: true);
            foreach (ItemSlot slot in allSlots)
            {
                if (slot.HasItem())
                {
                    slot.AddSlotItemToList();
                }
            }
        }
    }

}
