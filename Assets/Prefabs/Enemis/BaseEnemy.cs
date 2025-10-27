using UnityEngine;
using TMPro;

public abstract class BaseEnemy : MonoBehaviour
{
    private string nameEnemy;
    private int damageEnemy;
    private int MaxHealth;
    private float health;
    private LootItem[] itemDrop;

    private TextMeshProUGUI consoleTXT;

    public virtual void Inicialization(EnemyData data)
    {
        MaxHealth = data.MaxHealth;
        health = MaxHealth;
        nameEnemy = data.Name;
        damageEnemy = data.Damage;
        itemDrop = data.ItemDrop;
    }

    void Start()
    {
        gameObject.name = nameEnemy;
        consoleTXT = GameObject.FindGameObjectWithTag("Log").GetComponent<TextMeshProUGUI>();
    }

    public virtual void Die()
    {
        Debug.Log(nameEnemy + " умер.");
        DropItem();
        Destroy(gameObject);
    }
    public virtual void Move()
    {
        Debug.Log(nameEnemy + " ходит.");
    }

    public virtual void EnemyAttack()
    {
        Debug.Log(nameEnemy + " наносит " + damageEnemy + " урона.");
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
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и Нужно активировать предмет");
                NoneEffect();
                break;
            case StaticItemCharacteristicClass.Element.Fire:
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и горит.");
                EnemyBurning();
                break;

            case StaticItemCharacteristicClass.Element.Frost:
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и заморожен.");
                EnemyFreezing();
                break;

            case StaticItemCharacteristicClass.Element.Wind:
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и кровоточит.");
                EnemyBleeding();
                break;

            case StaticItemCharacteristicClass.Element.Water:
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и потерял немного брони.");
                EnemyReducingProtection();
                break;

            case StaticItemCharacteristicClass.Element.Earth:
                consoleTXT.SetText(nameEnemy + " получил " + playerDamage + " ед. урона и горит.");
                EnemyDamageWithDelay();
                break;
        }


    }
    public virtual void EnemyBurning()
    {
        Debug.Log(nameEnemy + " горит.");
    }

    public virtual void EnemyFreezing()
    {
        Debug.Log(nameEnemy + " заморожен.");
    }

    public virtual void EnemyBleeding()
    {
        Debug.Log(nameEnemy + " кровоточит.");
    }

    public virtual void EnemyReducingProtection()
    {
        Debug.Log(nameEnemy + " потерял немного брони.");
    }

    public virtual void EnemyDamageWithDelay()
    {
        Debug.Log(nameEnemy + " получил второй урон.");
    }

    public virtual void NoneEffect()
    {
        Debug.Log("Нужно активировать предмет.");
    }

    public void DropItem()
    {
        foreach (LootItem loot in itemDrop)
        {
            float randomValue = Random.Range(0, 100f);
            if (randomValue <= loot.ChangeDrop)
            {
                Instantiate(loot.PrefabLoot, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
