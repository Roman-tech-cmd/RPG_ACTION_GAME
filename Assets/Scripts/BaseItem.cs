
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    public enum typeItem { Weapons, Armor }
    [SerializeField] protected typeItem _typeItem;
    public typeItem TypeItem
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
        set { attackItem = value; }
    }

    protected float _damage;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected StaticElementClass.Element _element;
    public StaticElementClass.Element Element
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


    public enum Rare { common, unusual, rare, epic, legendary }
    protected Rare rareItem;
    public Rare RareItem
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

    [SerializeField] protected float additionalProtection;
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
        rareItem = (Rare)dataItem.rareItem;
        GetComponent<LootItem>().RareItem = (LootItem.Rare)rareItem;
    }
    void Update()
    {
        switch (rareItem)
        {
            case Rare.common:
                ColorUtility.TryParseHtmlString("#ADADAD", out rareColor);
                break;
            case Rare.unusual:
                ColorUtility.TryParseHtmlString("#4E95FD", out rareColor);
                break;

            case Rare.rare:
                ColorUtility.TryParseHtmlString("#55FD4E", out rareColor);
                break;

            case Rare.epic:
                ColorUtility.TryParseHtmlString("#EA4EFD", out rareColor);
                break;

            case Rare.legendary:
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
    }

    public string TranslateElement(StaticElementClass.Element elem) => elem switch
    {
        StaticElementClass.Element.Earth => "Земля",
        StaticElementClass.Element.Fire => "Огонь",
        StaticElementClass.Element.Frost => "Мороз",
        StaticElementClass.Element.Water => "Вода",
        StaticElementClass.Element.Wind => "Ветер",
        _ => ""
    };

    public abstract void AddItemToInventory(GameObject wolrdItem);
    public abstract void AddItemToList();
    public abstract bool CanPickUp();

    //public string nameItem() => _name;
    //public string descriptionItem() => _description;
    //public float damageItem() => _damage;
    //public string elementItem() => TranslateElement(_element);
    //public float AddictionalHp() => addictionalHp;
    //public float AddictionalMana() => addictionalMana;
    //public float AddictionalProtection() => addictionalProtection;
    //public float AddicitonalGemDMG() => addicitonalGemDMG;
    //public float AddictionalCaneDMG() => addictionalCaneDMG;



    /* public GameObject GetUiItem() => prefabUI;
     public GameObject GetWorldItem() => prefabWorld;
     public abstract bool CanPickUp();
     public abstract void AddItemToInventory(GameObject worldItem);
     public abstract void AddItemToList();
     public bool ItemIsActive() => isActive;
     public void SetActiveItem(bool active) => isActive = active;
     public void SetDamage(float damage) => _damage = damage;
     public void SetElement(StaticElementClass.Element element)
     {
         _element = element;
         GetAttack()?.GetComponent<BaseAttack>().GetElement(_element);
     }

     public void SetAttack(GameObject attack)
     {
         attackItem = attack;
         _icon = attackItem?.GetComponent<BaseAttack>()?.GetIcon();
     }
     public GameObject GetAttack() => attackItem;

     IDescription.Rare IDescription.rareItem() => (IDescription.Rare)rareItem;

     public Sprite GetIconItemAttack(bool yes)
     {
         if (yes) return _icon;
         else return null;
     }

     public Color GetRareColor() => rareColor;
     public Rare GetRare() => rareItem;*/


}