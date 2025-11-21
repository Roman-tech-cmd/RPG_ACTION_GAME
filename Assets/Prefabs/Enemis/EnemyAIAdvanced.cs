using UnityEngine;
using Pathfinding;
public class EnemyAIAdvanced : BaseEnemy
{
    [Header("Настройки дистанций")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;

    private Transform player;
    private EnemyState currentState = EnemyState.Idle;

    private enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Логика перехода между состояниями
        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    print(Physics2D.LinecastAll(transform.position, player.position,LayerMask.NameToLayer("Barrier")).Length);
                    if (Physics2D.LinecastAll(transform.position,player.position).Length==0)
                    {

                        currentState = EnemyState.Chasing;
                        Debug.Log("Обнаружил игрока! Начинаю преследование");
                    }
                    
                }
                break;

            case EnemyState.Chasing:
                if (distanceToPlayer <= attackRange)
                {
                    
                    currentState = EnemyState.Attacking;
                    Debug.Log("Игрок в зоне атаки!");
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = EnemyState.Idle;
                    Debug.Log("Игрок потерян");
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case EnemyState.Attacking:

                float rotationSpeed = 5;
                if (player != null)
                {
                    Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Quaternion targetRotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
                }
                if (distanceToPlayer > attackRange)
                {
                    currentState = EnemyState.Chasing;
                    Debug.Log("Игрок вышел из зоны атаки");
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }
    }

    private void ChasePlayer()
    {
        //Vector2 direction = (player.position - transform.position).normalized;
        //transform.Translate(direction * moveSpeed * Time.deltaTime);

        GetComponent<IAstarAI>().destination = player.transform.position;
        GetComponent<IAstarAI>().maxSpeed = moveSpeed;
    }

    private void AttackPlayer()
    {
        // Останавливаемся для атаки
        PlayerProcessor.Instant.TakeDamage(damageEnemy);
        Debug.Log("Атакую игрока!");
    }

    // Визуализация зон в редакторе
    private void OnDrawGizmosSelected()
    {
        // Зона обнаружения (желтая)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Зона атаки (красная)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}