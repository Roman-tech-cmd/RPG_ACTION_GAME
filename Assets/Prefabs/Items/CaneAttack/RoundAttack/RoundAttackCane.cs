using UnityEngine;

public class RoundAttackCane : BaseAttack
{

    [Header("Настройки")]
    [SerializeField] private GameObject objTrajectory;
    [SerializeField] private GameObject objRange;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask layerBarrier;
    private float maxRadius;
    Vector2 posMouse;
    [SerializeField] private float smoothSpeed = 10f;
    private Vector2 targetDirection;
    private float targetDistance;
    [SerializeField] bool canAttack;


    void Start()
    {
        maxRadius = objRange.GetComponent<CircleCollider2D>().bounds.extents.x;
    }

    public override void Attack()
    {
        if (canAttack)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(objTrajectory.transform.position, objTrajectory.GetComponent<CircleCollider2D>().bounds.extents.x, enemyLayerMask);
            Debug.Log(enemies.Length);
            if (enemies.Length > 0)
            {
                foreach (Collider2D enemy in enemies)
                {
                    enemy.gameObject.GetComponent<BaseEnemy>()?.TakeDamage(damage, attackElement);
                    enemy.GetComponent<IEnemyDebaf>();
                }
                PlayerProcessor.Instant.ReduceMana(costMana);
                attackIsHappend = true;
            }
        }
    }



    public override void Update()
    {
        if (canAttack == false) objTrajectory.GetComponent<SpriteRenderer>().color = Color.red;
        else objTrajectory.GetComponent<SpriteRenderer>().color = Color.white;
        CreateTrajectory();
        base.Update();

    }

    public override void CreateTrajectory()
    {
        posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        canAttack = !Physics2D.Linecast(transform.position, posMouse, layerBarrier);

        Vector2 center = transform.position;
        Vector2 direction = posMouse - center;

        if (direction.magnitude > 0.1f)
        {
            targetDirection = direction.normalized;
            targetDistance = Mathf.Clamp(direction.magnitude, 0f, maxRadius);
        }

        Vector2 currentPos = objTrajectory.transform.position;
        Vector2 targetPos = center + targetDirection * targetDistance;

        objTrajectory.transform.position = Vector2.Lerp(
            currentPos,
            targetPos,
            Time.deltaTime * smoothSpeed
        );

    }
}
