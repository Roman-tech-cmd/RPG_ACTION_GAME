using UnityEngine;

public class PlayerProcessor : MonoBehaviour
{
    public static PlayerProcessor Instant { get; private set; }
    private void Awake()
    {
        if (Instant == null)
        {
            Instant = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private PlayerData playerData;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) TakeDamage(5);
        if (Input.GetKeyDown(KeyCode.H)) ReduceMana(5);
        if (Input.GetKeyDown(KeyCode.K)) RestoreHp(2, false);
        if (Input.GetKeyDown(KeyCode.L)) RestoreMana(3, false);
        if (Input.GetKeyDown(KeyCode.N)) RestoreHp(0, true);
        if (Input.GetKeyDown(KeyCode.M)) RestoreMana(5, true);
        if (Input.GetKeyDown(KeyCode.L)) TakeDamage(5);
    }

    public void TakeDamage(float damage)
    {
        playerData.PlayerHealth -= damage;
    }
    public void ReduceMana(float amount)
    {
        playerData.PlayerMana -= amount;
    }

    public void RestoreHp(float amount, bool full)
    {
        if (full) playerData.PlayerHealth = playerData.MaxPlayerHealth;
        else playerData.PlayerHealth += amount;
    }
    public void RestoreMana(float amount, bool full)
    {
        if (full) playerData.PlayerMana = playerData.MaxPlayerMana;
        else playerData.PlayerMana += amount;
    }
    public bool EnoughMana(float costMana)
    {
        if (playerData.PlayerMana - costMana >= 0)
        {
            return true;
        }
        else return false;
    }
    public void AddGemDMG(float value)
    {
        playerData.GemDMG += value;
    }

    public void AddMaxHp(float AddHpMax)
    {
        playerData.MaxPlayerHealth += AddHpMax;
        playerData.PlayerHealth += AddHpMax;
    }
}
