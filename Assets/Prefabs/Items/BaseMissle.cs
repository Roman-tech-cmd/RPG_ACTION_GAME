using UnityEngine;

public abstract class BaseMissle : MonoBehaviour
{
    protected StaticItemCharacteristicClass.Element typeMissle;
    protected float speed;
    protected float lifeTime;
    protected IEnemyDebaf enemyDebaf;
    protected BaseEnemy enemy;
    protected float damageMissle;
    protected bool nastyHit;


    void Update()
    {
        transform.Translate(Vector2.up * -speed * Time.deltaTime);
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damageMissle, typeMissle);
            if (nastyHit == false) Destroy(gameObject);
        }
    }

    public virtual void GetDamage(float damage)
    {
        damageMissle = damage;
    }
    public virtual void GetSpeed(float speedMissle)
    {
        speed = speedMissle;
    }
    public virtual void GetLifeTime(float lifeTimeMissle)
    {
        lifeTime = lifeTimeMissle;
    }
    public virtual void GetElement(StaticItemCharacteristicClass.Element element)
    {
        typeMissle = element;
    }

}
