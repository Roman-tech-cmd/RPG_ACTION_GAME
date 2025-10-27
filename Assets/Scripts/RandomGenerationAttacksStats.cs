using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomGenerationAttacksStats : MonoBehaviour
{
    public static RandomGenerationAttacksStats Instance;
    [SerializeField] private int lvlPlayer;
    private float multipLVLPlayer;
    private int lvlItem;
    [SerializeField] private float multipLVLItem;
    private float baseDamage;
    private float summaryDamage;
    private LootItem.Rare rareItem;
    private float multipRare;

    [SerializeField] private Button buttonActivate;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private GameObject[] AttaksCanes;
    [SerializeField] private GameObject[] AttaksGems;

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

    void Start()
    {
        buttonActivate.onClick.AddListener(ActivatingAnItem);
    }
    void Update()
    {
        if (InventoryManager.Instance.SelectedItem != null)
        {
            if (InventoryManager.Instance.SelectedItem.GetComponent<BaseItem>().IsActive == false)
            {
                buttonActivate.interactable = true;
                buttonActivate.GetComponentInChildren<TextMeshProUGUI>().text = "Активировать";
            }
            else
            {
                buttonActivate.GetComponentInChildren<TextMeshProUGUI>().text = "Активировано";
                buttonActivate.interactable = false;
            }
        }
    }
    public void GenerateMultiplayers()
    {
        lvlItem = lvlPlayer + Random.Range(0, 2);
        if (lvlItem == 0) lvlItem = 1;
        multipLVLPlayer = (float)lvlPlayer / 10;
        baseDamage = multipLVLItem * lvlItem;
        multipRare = rareItem switch
        {
            LootItem.Rare.common => 1.1f,
            LootItem.Rare.unusual => 1.3f,
            LootItem.Rare.rare => 1.5f,
            LootItem.Rare.epic => 1.7f,
            LootItem.Rare.legendary => 2.1f,
            _ => 1
        };
    }
    public GameObject RandomAttack(GameObject[] attacksItem)
    {
        int randomAttack = Random.Range(0, attacksItem.Length);
        return attacksItem[randomAttack];
    }
    public void GenerateRandomStats(GameObject item)
    {
        rareItem = item.GetComponent<LootItem>().RareItem;

        GenerateMultiplayers();

        summaryDamage = baseDamage * multipRare + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(0.1f, 0.9f)));
        BaseItem itemType = item.GetComponent<BaseItem>();

        GenerateRandomElement(itemType);

        if (item.GetComponent<Gem>()) itemType.AttackItem = RandomAttack(AttaksGems);
        if (item.GetComponent<Cane>()) itemType.AttackItem = RandomAttack(AttaksCanes);

        if (itemType.IsActive == false)
        {
            itemType.Damage = Mathf.Round(summaryDamage * 10f) / 10f;
            itemType.IsActive = true;
        }
    }
    public void ActivatingAnItem()
    {
        if (InventoryManager.Instance.SelectedItem?.GetComponent<BaseItem>().IsActive == false)
        {
            GenerateRandomStats(InventoryManager.Instance.SelectedItem);
        }
    }

    public BaseItem TransferStats(BaseItem itemFrom, BaseItem itemTo)
    {
        itemTo.IsActive = itemFrom.IsActive;
        itemTo.Damage = itemFrom.Damage;
        itemTo.AttackItem = itemFrom.AttackItem;
        itemTo.Element = itemFrom.Element;
        return itemTo;
    }

    public void GenerateRandomElement(BaseItem itemType)
    {
        StaticItemCharacteristicClass.Element randomElement = (StaticItemCharacteristicClass.Element)Random.Range(1, 6);
        itemType.Element = randomElement;
    }
}
