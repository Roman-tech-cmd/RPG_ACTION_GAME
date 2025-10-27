using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Data")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public int Damage;
    public int MaxHealth;

    public LootItem[] ItemDrop;
}
