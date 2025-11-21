using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyData gobliData;
    protected string nameEnemy;
    protected int damageEnemy;
    protected int MaxHealth;
    protected float health;
    protected LootItem[] itemDrop;
    protected float moveSpeed;
    

    [Header("Настройки атаки")]
    protected float speedAttack;
    protected float lastAttackTime = 0f;
    protected bool canAttack = true;

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }
    protected bool isDie;
    protected bool canTakeDebaff = true;

    public virtual void Inicialization()
    {
        MaxHealth = gobliData.MaxHealth;
        health = MaxHealth;
        nameEnemy = gobliData.Name;
        damageEnemy = gobliData.Damage;
        itemDrop = gobliData.ItemDrop;
        moveSpeed = gobliData.MoveSpeed;
        speedAttack = gobliData.SpeedAttack;
    }

    public virtual void Awake()
    {
        Inicialization();
    }

    void Start()
    {
        //Inicialization(gobliData);
        gameObject.name = nameEnemy;
    }

    private void Update()
    {
        if (isDie) canTakeDebaff = false;
    }

    public virtual void Die()
    {
        isDie = true;
        Debug.Log(nameEnemy + " умер.");
        DropItem();
        Destroy(gameObject);
    }
    public virtual void Move()
    {
        //Debug.Log(nameEnemy + " ходит.");
    }
    public virtual void EnemyAttack()
    {
        if (!canAttack) return;

        Debug.Log(nameEnemy + " атакует!");
        PlayerProcessor.Instant.TakeDamage(damageEnemy);

        // Запускаем кулдаун
        StartCooldown();
    }

    protected virtual void StartCooldown()
    {
        canAttack = false;
        lastAttackTime = Time.time;
    }

    protected virtual void UpdateCooldown()
    {
        if (!canAttack && Time.time >= lastAttackTime + speedAttack)
        {
            canAttack = true;
        }
    }

    public virtual void ResetAttackCooldown()
    {
        lastAttackTime = 0f;
        canAttack = true;
    }

    public virtual void TakeDamage(float playerDamage, StaticItemCharacteristicClass.Element element)
    {
        if (health > 0)
        {
            health -= playerDamage;
            Debug.Log(nameEnemy + " получил " + playerDamage + " ед. урона");
        }
        if (health <= 0)
        {
            Die();
        }
        switch (element)
        {
            case StaticItemCharacteristicClass.Element.None:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и Нужно активировать предмет");
                NoneEffect();
                break;
            case StaticItemCharacteristicClass.Element.Fire:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и горит.");
                EnemyBurning();
                break;

            case StaticItemCharacteristicClass.Element.Frost:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и заморожен.");
                EnemyFreezing();
                break;

            case StaticItemCharacteristicClass.Element.Wind:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и кровоточит.");
                EnemyBleeding();
                break;

            case StaticItemCharacteristicClass.Element.Water:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и потерял немного брони.");
                EnemyReducingProtection();
                break;

            case StaticItemCharacteristicClass.Element.Earth:
                print(nameEnemy + " получил " + playerDamage + " ед. урона и горит.");
                EnemyDamageWithDelay();
                break;
        }


    }
    public virtual void EnemyBurning()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log(nameEnemy + " горит.");
        }
    }

    public virtual void EnemyFreezing()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log(nameEnemy + " заморожен.");
        }
    }

    public virtual void EnemyBleeding()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log(nameEnemy + " кровоточит.");
        }
    }

    public virtual void EnemyReducingProtection()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log(nameEnemy + " потерял немного брони.");
        }
    }

    public virtual void EnemyDamageWithDelay()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log(nameEnemy + " получил второй урон.");
        }
    }

    public virtual void NoneEffect()
    {
        if (canTakeDebaff && !isDie)
        {
            Debug.Log("Нужно активировать предмет.");
        }
    }

    public void DropItem()
    {
        foreach (LootItem loot in itemDrop)
        {
            float randomValue = Random.Range(0, 100);
            if (randomValue <= loot.ChangeDrop)
            {
                Instantiate(loot.PrefabLoot, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
