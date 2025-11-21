using Pathfinding;
using UnityEngine;
public class EnemyAIAdvanced : MonoBehaviour
{
    BaseEnemy baseEnemy;
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

    private void Awake()
    {
        baseEnemy = GetComponent<BaseEnemy>();
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
                    LayerMask barrierMask = 1 << LayerMask.NameToLayer("Barrier");
                    if (Physics2D.Raycast(transform.position, player.position - transform.position,detectionRange, barrierMask) ==false)
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
                RotateToPlayer();
                if (distanceToPlayer > attackRange)
                {
                    currentState = EnemyState.Chasing;
                    Debug.Log("Игрок вышел из зоны атаки");
                }
                else
                {
                    baseEnemy.EnemyAttack();
                }
                break;
        }
    }

    public void RotateToPlayer()
    {
        float rotationSpeed = 5;
        if (player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void ChasePlayer()
    {
        GetComponent<IAstarAI>().maxSpeed = baseEnemy.MoveSpeed;
        GetComponent<IAstarAI>().destination = player.transform.position;        
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