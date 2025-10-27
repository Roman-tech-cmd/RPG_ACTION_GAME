using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomGenerationArmorStats : MonoBehaviour
{
    public static RandomGenerationArmorStats Instance;
    [SerializeField] private int lvlPlayer;
    private float multipLVLPlayer;
    private int lvlItem;
    [SerializeField] private float multipLVLItem;
    private int numParams;

    private float baseArmor;
    [SerializeField] private float summaryArmor;
    [SerializeField] private float summaryAdditionalMana;
    [SerializeField] private float summaryAdditionalHp;
    [SerializeField] private float summaryAdditionalGemDMG;
    [SerializeField] private float summaryAdditionalCaneDMG;

    [SerializeField] private List<string> choosenParams = new List<string>();
    private Dictionary<string, float> paramsChanges = new Dictionary<string, float>
    {
        {"Health",0.8f },{"Mana",0.6f },{"GemDamage",0.3f },{"CaneDamage",0.3f },
    };

    private Dictionary<string, float> paramsRW = new Dictionary<string, float>
    {
        {"Health",5 },{"Mana",4 },{"GemDamage",1.5f },{"CaneDamage",1.5f },
    };


    private LootItem.Rare rareItem;
    private float multipRare;

    [SerializeField] private Button buttonActivate;
    [SerializeField] private TextMeshProUGUI textButton;

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
        buttonActivate.onClick.AddListener(OnClickButtonActivate);
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
                buttonActivate.GetComponentInChildren<TextMeshProUGUI>().text = "Активированно";
                buttonActivate.interactable = false;
            }
        }
    }

    public void OnClickButtonActivate()
    {
        if (InventoryManager.Instance.SelectedItem?.GetComponent<BaseItem>().IsActive == false)
        {
            GenerateRandomStats(InventoryManager.Instance.SelectedItem);
        }
    }

    public void GenerateRandomStats(GameObject item)
    {
        rareItem = item.GetComponent<LootItem>().RareItem;
        multipRare = rareItem switch
        {
            LootItem.Rare.common => 1.1f,
            LootItem.Rare.unusual => 1.3f,
            LootItem.Rare.rare => 1.5f,
            LootItem.Rare.epic => 1.7f,
            LootItem.Rare.legendary => 2.1f,
            _ => 1
        };
        GenerateMultiplayers();
        BaseItem itemType = item.GetComponent<BaseItem>();
        itemType.AdditionalProtection = Mathf.Round(summaryArmor * 10f) / 10f;
        itemType.AdditionalHp = Mathf.Round(summaryAdditionalHp * 10f) / 10f;
        itemType.AdditionalMana = Mathf.Round(summaryAdditionalMana * 10f) / 10f;
        itemType.AdditionalGemDMG = Mathf.Round(summaryAdditionalGemDMG * 10f) / 10f;
        itemType.AdditionalCaneDMG = Mathf.Round(summaryAdditionalCaneDMG * 10f) / 10f;
        itemType.IsActive = true;
    }

    public void GenerateMultiplayers()
    {
        lvlItem = lvlPlayer + Random.Range(0, 2);
        if (lvlItem == 0) lvlItem = 1;
        multipLVLPlayer = (float)lvlPlayer / 10;
        baseArmor = multipLVLItem * lvlItem;
        summaryArmor = baseArmor * multipRare + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(0, 0.7f)));
        ChooseRandomParams();
    }


    public void ChooseRandomParams()
    {
        choosenParams.Clear();
        numParams = rareItem switch
        {
            LootItem.Rare.common => 1,
            LootItem.Rare.unusual => 2,
            LootItem.Rare.rare => 3,
            LootItem.Rare.epic => 4,
            LootItem.Rare.legendary => 5,
            _ => 1
        };

        foreach (var param in paramsChanges)
        {
            float dropChange = param.Value;
            string nameParam = param.Key;

            float randomRoll = Random.Range(0f, 1f);
            if (randomRoll <= dropChange)
            {
                if (choosenParams.Count < numParams)
                {
                    choosenParams.Add(nameParam);
                }
            }
        }
        for (int i = 0; i < choosenParams.Count; i++)
        {
            switch (choosenParams[i])
            {
                case "Health":
                    paramsRW.TryGetValue("Health", out float RW_HealthStat);
                    print(RW_HealthStat);
                    summaryAdditionalHp = multipRare * Random.Range(RW_HealthStat - 2, RW_HealthStat + 1) + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(-0.1f, 0.9f)));
                    break;
                case "Mana":
                    paramsRW.TryGetValue("Mana", out float RW_ManaStat);
                    print(RW_ManaStat);
                    summaryAdditionalMana = multipRare * Random.Range(RW_ManaStat - 2, RW_ManaStat + 1) + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(-0.1f, 0.9f)));
                    break;
                case "GemDamage":
                    paramsRW.TryGetValue("GemDamage", out float RW_GemStat);
                    print(RW_GemStat);
                    summaryAdditionalGemDMG = multipRare * Random.Range(RW_GemStat - 2, RW_GemStat + 1) + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(-0.1f, 0.9f)));
                    break;
                case "CaneDamage":
                    paramsRW.TryGetValue("CaneDamage", out float RW_CaneStat);
                    print(RW_CaneStat);
                    summaryAdditionalCaneDMG = multipRare * Random.Range(RW_CaneStat - 2, RW_CaneStat + 1) + lvlPlayer * multipLVLPlayer + (multipRare * (1 + Random.Range(-0.1f, 0.9f)));
                    break;
            }
        }
    }

    public BaseItem TransferStats(BaseItem itemFrom, BaseItem itemTo)
    {
        itemTo.IsActive = itemFrom.IsActive;
        itemTo.AdditionalProtection = itemFrom.AdditionalProtection;
        itemTo.AttackItem = itemFrom.AttackItem;
        itemTo.Element = itemFrom.Element;
        itemTo.AdditionalHp = itemFrom.AdditionalHp;
        itemTo.AdditionalMana = itemFrom.AdditionalMana;
        itemTo.AdditionalGemDMG = itemFrom.AdditionalGemDMG;
        itemTo.AdditionalCaneDMG = itemFrom.AdditionalCaneDMG;
        return itemTo;
    }
}
