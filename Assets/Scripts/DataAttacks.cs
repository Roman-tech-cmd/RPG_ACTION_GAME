using UnityEngine;

[CreateAssetMenu(fileName = "Stats Attack", menuName = "Attack/Create Attack Stats")]
public class DataAttacks : ScriptableObject
{
    public int Damage;
    public int Cooldown;
    public float SpeedMissle;
    public float LifeTimeMissle;

}
