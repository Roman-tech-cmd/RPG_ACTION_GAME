using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{

    [Header("Описание атаки")]
    [SerializeField] protected string nameAttack;
    [SerializeField] protected string descriptionAttack;
    [SerializeField] protected Sprite iconAttack;

    [Header("Параметры атаки")]
    protected float damage;
    [SerializeField] private float baseCooldown;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float costMana;
    protected bool attackIsHappend;
    public bool AttackIsHappend
    {
        get { return attackIsHappend; }
        set
        {
            attackIsHappend = value;
        }
    }
    public float CostMana
    {
        get { return costMana; }
    }
    protected StaticElementClass.Element attackElement;
    public float Cooldown
    {
        get
        {
            return cooldown;
        }
    }
    public enum Rare { common, unusual, rare, epic, legendary }
    [SerializeField] protected Rare rareItem;

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
            Rare.common => baseCooldown / 1.0f,
            Rare.unusual => baseCooldown / 1.1f,
            Rare.rare => baseCooldown / 1.2f,
            Rare.epic => baseCooldown / 1.3f,
            Rare.legendary => baseCooldown / 1.4f,
            _ => baseCooldown
        };
    }

    public abstract void CreateTrajectory();

    public abstract void Attack();

    public virtual void GetElement(StaticElementClass.Element element)
    {
        attackElement = element;
    }

    public virtual Sprite GetIcon()
    {
        return iconAttack;
    }
    public virtual void GetDamage(float damageFromGem)
    {
        damage = damageFromGem;
    }
    public virtual string GetNameAttack()
    {
        return nameAttack;
    }
    public virtual string GetDescriptionAttack()
    {
        return descriptionAttack;
    }
    public virtual void GetRareItem(Rare rare)
    {
        rareItem = rare;
    }
}
