using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    protected StaticItemCharacteristicClass.typeItem _typeItem;
    public StaticItemCharacteristicClass.typeItem TypeItem
    {
        get { return _typeItem; }
    }
    [SerializeField] protected DataItem dataItem;

    [SerializeField] protected GameObject prefabWorld;
    public GameObject PrefabWorld
    {
        get { return prefabWorld; }
        set { prefabWorld = value; }
    }

    [SerializeField] protected GameObject prefabUI;
    public GameObject PrefabUI
    {
        get { return prefabUI; }
        set { prefabUI = value; }
    }

    protected string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    protected string _description;
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    protected GameObject attackItem;
    public GameObject AttackItem
    {
        get { return attackItem; }
        set 
        { 
            attackItem = value;
        }
    }

    protected float _damage;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected StaticItemCharacteristicClass.Element _element;
    public StaticItemCharacteristicClass.Element Element
    {
        get { return _element; }
        set { _element = value; }
    }

    protected string elementText;
    public string ElementText
    {
        get { return elementText; }
        set { elementText = value; }
    }

    protected StaticItemCharacteristicClass.Rare rareItem;
    public StaticItemCharacteristicClass.Rare RareItem
    {
        get { return rareItem; }
        set { rareItem = value; }
    }

    protected Color rareColor;
    public Color RareColor
    {
        get { return rareColor; }
        set { rareColor = value; }
    }

    protected bool isActive = false;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    protected Sprite _icon;
    public Sprite Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }
    protected float additionalHp;
    public float AdditionalHp
    {
        get { return additionalHp; }
        set { additionalHp = value; }
    }

    protected float additionalProtection;
    public float AdditionalProtection
    {
        get { return additionalProtection; }
        set { additionalProtection = value; }
    }

    protected float additionalMana;
    public float AdditionalMana
    {
        get { return additionalMana; }
        set { additionalMana = value; }
    }

    protected float additionalGemDMG;
    public float AdditionalGemDMG
    {
        get { return additionalGemDMG; }
        set { additionalGemDMG = value; }
    }

    protected float additionalCaneDMG;
    public float AdditionalCaneDMG
    {
        get { return additionalCaneDMG; }
        set { additionalCaneDMG = value; }
    }


    private void Start() => InitializeItem();

    protected virtual void InitializeItem()
    {
        _name = dataItem.ItemName;
        _description = dataItem.ItemDiscription;
        rareItem = dataItem.rareItem;
        _typeItem = dataItem.ItemType;
        GetComponent<LootItem>().RareItem = (LootItem.Rare)rareItem;
    }
    void Update()
    {
        switch (rareItem)
        {
            case StaticItemCharacteristicClass.Rare.common:
                ColorUtility.TryParseHtmlString("#ADADAD", out rareColor);
                break;
            case StaticItemCharacteristicClass.Rare.unusual:
                ColorUtility.TryParseHtmlString("#4E95FD", out rareColor);
                break;

            case StaticItemCharacteristicClass.Rare.rare:
                ColorUtility.TryParseHtmlString("#55FD4E", out rareColor);
                break;

            case StaticItemCharacteristicClass.Rare.epic:
                ColorUtility.TryParseHtmlString("#EA4EFD", out rareColor);
                break;

            case StaticItemCharacteristicClass.Rare.legendary:
                ColorUtility.TryParseHtmlString("#FDDF4E", out rareColor);
                break;
            default:
                break;
        }
        if (GetComponent<DragebleItem>() == true)
        {
            GetComponentInParent<Image>().color = rareColor;
        }

        elementText = TranslateElement(_element);

        if (attackItem!=null)
        {
            Icon = attackItem.GetComponent<BaseAttack>().IconAttack;
        }
    }

    public string TranslateElement(StaticItemCharacteristicClass.Element elem) => elem switch
    {
        StaticItemCharacteristicClass.Element.Earth => "Земля",
        StaticItemCharacteristicClass.Element.Fire => "Огонь",
        StaticItemCharacteristicClass.Element.Frost => "Мороз",
        StaticItemCharacteristicClass.Element.Water => "Вода",
        StaticItemCharacteristicClass.Element.Wind => "Ветер",
        _ => ""
    };

    public abstract void AddItemToInventory(GameObject wolrdItem);
    public abstract void AddItemToList();
    public abstract bool CanPickUp();
}