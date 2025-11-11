using UnityEngine;

public class GoblinEnemy : BaseEnemy, IEnemyDebaf
{

    [SerializeField] private EnemyData gobliData;
    private bool canTakeDebaff = true;
    [SerializeField, Range(0, 100)] private float changeRepel;

    [SerializeField] private CircleCollider2D areaDetecting;
    [SerializeField] private CircleCollider2D areaAttack;


    private bool isDie;

    public void Awake()
    {
        base.Inicialization(gobliData);
    }

    void Update()
    {
        if (isDie) canTakeDebaff = false;
        Move();
    }


    public override void TakeDamage(float playerDamage, StaticItemCharacteristicClass.Element element)
    {
        if (Random.value > ((100 - changeRepel) / 100))
        {
            Debug.Log(gobliData.Name + " отразил атаку.");
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
        isDie = true;
        Debug.Log(gobliData.Name + " идзаёт визг.");
        base.Die();
    }

    public override void EnemyBurning()
    {
        if (canTakeDebaff && !isDie)
        {
            base.EnemyBurning();
        }
    }

    public override void EnemyFreezing()
    {
        if (canTakeDebaff && !isDie)
        {
            base.EnemyFreezing();
        }
    }

    public override void EnemyBleeding()
    {
        if (canTakeDebaff && !isDie)
        {
            base.EnemyBleeding();
        }
    }

    public override void EnemyReducingProtection()
    {
        if (canTakeDebaff && !isDie)
        {
            base.EnemyReducingProtection();
        }
    }

    public override void EnemyDamageWithDelay()
    {
        if (canTakeDebaff && !isDie)
        {
            base.EnemyDamageWithDelay();
        }
    }


    public override void Move()
    {
        base.Move();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZoneDetection")) 
        { 

        }
    }

}
