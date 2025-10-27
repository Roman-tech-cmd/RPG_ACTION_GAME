using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    [Header("Описание атаки")]
    [SerializeField] protected string nameAttack;
    public string NameAttack => nameAttack;

    [SerializeField] protected string descriptionAttack;
    public string DescriptionAttack => descriptionAttack;

    [SerializeField] protected Sprite iconAttack;
    public Sprite IconAttack => iconAttack;


    [Header("Параметры атаки")]
    protected float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    [SerializeField] private float baseCooldown;

    [SerializeField] protected float cooldown;
    public float Cooldown => cooldown;

    [SerializeField] protected float costMana;
    public float CostMana => costMana;

    protected bool attackIsHappend;
    public bool AttackIsHappend
    {
        get => attackIsHappend;
        set => attackIsHappend = value;
    }

    protected StaticItemCharacteristicClass.Element attackElement;
    public StaticItemCharacteristicClass.Element AttackElement
    {
        get => attackElement;
        set => attackElement = value;
    }

    protected StaticItemCharacteristicClass.Rare rareItem;
    public StaticItemCharacteristicClass.Rare RareItem
    {
        get => rareItem;
        set => rareItem = value;
    }

    [SerializeField] protected float speedMissle;
    [SerializeField] protected float lifeTimeMissle;

    public virtual GameObject GetPrefab()
    {
        return gameObject;
    }

    public virtual void Update()
    {
        cooldown = rareItem switch
        {
            StaticItemCharacteristicClass.Rare.common => baseCooldown / 1.0f,
            StaticItemCharacteristicClass.Rare.unusual => baseCooldown / 1.1f,
            StaticItemCharacteristicClass.Rare.rare => baseCooldown / 1.2f,
            StaticItemCharacteristicClass.Rare.epic => baseCooldown / 1.3f,
            StaticItemCharacteristicClass.Rare.legendary => baseCooldown / 1.4f,
            _ => baseCooldown
        };
    }

    public abstract void CreateTrajectory();

    public abstract void Attack();    
}
