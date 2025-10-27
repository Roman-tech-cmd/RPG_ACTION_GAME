using UnityEngine;

public class LineAttackGem : BaseAttack
{

    [Header("Настройки")]
    [SerializeField] private GameObject objTrajectory;
    [SerializeField] private GameObject missile;
    private GameObject hand;

    public void GetPointSpawn(GameObject point)
    {
        hand = point;
    }

    public override void Attack()
    {
        BaseMissle spawnMissle = Instantiate(missile, transform.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 90))).GetComponent<BaseMissle>();
        spawnMissle.GetSpeed(speedMissle);
        spawnMissle.GetLifeTime(lifeTimeMissle);
        spawnMissle.GetDamage(damage);
        spawnMissle.GetElement(attackElement);
        attackIsHappend = true;
    }

    public void RotateToMouse()
    {
        Vector2 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }

    public override void CreateTrajectory()
    {
        RotateToMouse();
    }
}
