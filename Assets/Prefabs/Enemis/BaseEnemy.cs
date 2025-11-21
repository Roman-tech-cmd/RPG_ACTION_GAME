using System;
using TMPro;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected string nameEnemy;
    [SerializeField] protected int damageEnemy;
    protected int MaxHealth;
    protected float health;
    protected LootItem[] itemDrop;
    [SerializeField] protected float moveSpeed;

    public virtual void Inicialization(EnemyData data)
    {
        MaxHealth = data.MaxHealth;
        health = MaxHealth;
        nameEnemy = data.Name;
        damageEnemy = data.Damage;
        itemDrop = data.ItemDrop;
        moveSpeed = data.MoveSpeed;
    }

    void Start()
    {
        gameObject.name = nameEnemy;
    }

    public virtual void Die()
    {
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
            float randomValue = UnityEngine.Random.Range(0, 100f);
            if (randomValue <= loot.ChangeDrop)
            {
                Instantiate(loot.PrefabLoot, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
