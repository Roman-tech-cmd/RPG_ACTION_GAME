using UnityEngine;

public class PointAttackCane : BaseAttack
{

    [Header("Настройки")]

    [SerializeField] private LayerMask layerEnemies;
    [SerializeField] private LayerMask layerBarrier;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Collider2D rangeObj;

    RaycastHit2D hit;
    private Color colorEnemy;
    private float maxDistance;
    Vector2 mousePos;
    GameObject currentEnemy;
    GameObject previousEnemy;
    bool startAiming = false;

    void Start()
    {
        maxDistance = rangeObj.bounds.extents.x;
    }
    public override void Attack()
    {
        if (hit.collider != null)
        {
            if (Vector2.Distance(transform.position, mousePos) <= maxDistance && !Physics2D.Linecast(transform.position, mousePos, layerBarrier))
            {
                BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
                enemy.TakeDamage(damage, attackElement);
                attackIsHappend = true;
                startAiming = false;
                if (previousEnemy != null) previousEnemy.GetComponent<SpriteRenderer>().color = colorEnemy;
            }
        }
    }

    public override void CreateTrajectory()
    {
        startAiming = true;
        FindTheAim();
    }

    public override void Update()
    {
        base.Update();
        if (startAiming)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointer.transform.position = mousePos;

            hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerEnemies);
        }
    }

    private void FindTheAim()
    {
        if (Vector2.Distance(transform.position, mousePos) <= maxDistance && !Physics2D.Linecast(transform.position, mousePos, layerBarrier))
        {
            ProcessEnemySelection();
        }
        else ResetEnemyHighlight();

    }
    private void ResetEnemyHighlight()
    {
        if (currentEnemy != null)
        {
            currentEnemy.GetComponent<SpriteRenderer>().color = colorEnemy;
            currentEnemy = null;
        }
        previousEnemy = null;
    }

    private void ProcessEnemySelection()
    {
        previousEnemy = currentEnemy;

        if (hit.collider != null) currentEnemy = hit.collider.gameObject;
        else currentEnemy = null;

        if (currentEnemy != previousEnemy)
        {
            if (previousEnemy != null) previousEnemy.GetComponent<SpriteRenderer>().color = colorEnemy;
            if (currentEnemy != null)
            {
                SpriteRenderer renderer = currentEnemy.GetComponent<SpriteRenderer>();
                colorEnemy = renderer.color;
                renderer.color = Color.green;
            }
        }
    }

    void OnDestroy()
    {
        if (previousEnemy != null) previousEnemy.GetComponent<SpriteRenderer>().color = colorEnemy;
        if (currentEnemy != null) currentEnemy.GetComponent<SpriteRenderer>().color = colorEnemy;
    }



}
