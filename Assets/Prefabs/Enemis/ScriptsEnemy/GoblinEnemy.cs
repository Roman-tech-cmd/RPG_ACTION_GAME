using System;
using System.Collections;
using UnityEngine;

public class GoblinEnemy : BaseEnemy, IEnemyDebaf
{
    [SerializeField, Range(0, 100)] private float changeRepel;

    public override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(float playerDamage, StaticItemCharacteristicClass.Element element)
    {
        if (UnityEngine.Random.value > ((100 - changeRepel) / 100))
        {
            Debug.Log(nameEnemy + " отразил атаку.");
            canTakeDebaff = false;
            return;
        }
        else
        {
            canTakeDebaff = true;
            base.TakeDamage(playerDamage, element);
        }
    }

    public override void Die()
    {
        Debug.Log(nameEnemy + " идзаёт визг.");
        base.Die();
    }

    public override void EnemyAttack()
    {
        base.UpdateCooldown();
        base.EnemyAttack();
    }
}
