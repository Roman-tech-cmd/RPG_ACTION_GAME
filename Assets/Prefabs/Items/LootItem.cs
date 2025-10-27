using UnityEngine;

public class LootItem : MonoBehaviour
{
    public enum Rare { common, unusual, rare, epic, legendary };
    private float changeDrop;
    [SerializeField] private GameObject prefabLoot;
    private Rare rareItem;

    void Start()
    {
        changeDrop = rareItem switch
        {
            Rare.common => 40f,
            Rare.unusual => 20f,
            Rare.rare => 10f,
            Rare.epic => 3f,
            Rare.legendary => 1f,
            _ => 0,
        };
    }
    public Rare RareItem
    {
        set
        {
            rareItem = value;
            changeDrop = rareItem switch
            {
                Rare.common => 40f,
                Rare.unusual => 20f,
                Rare.rare => 10f,
                Rare.epic => 3f,
                Rare.legendary => 1f,
                _ => 0,
            };
        }
        get { return rareItem; }
    }
    void Update()
    {
        if (prefabLoot == null) prefabLoot = GetComponent<BaseItem>().PrefabWorld;
    }

    public float ChangeDrop
    {
        get { return changeDrop; }
    }
    public GameObject PrefabLoot
    {
        get { return prefabLoot; }
    }
}
