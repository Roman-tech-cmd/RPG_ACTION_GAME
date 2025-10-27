using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "PlayerStat/Stats")]
public class BasePlayerData : ScriptableObject
{
    public float MaxPlayerHP;
    public float MaxPlayerMana;
    public float MaxPlayerProtection;

}
