using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    [SerializeField] protected BasePlayerData data;
    [SerializeField] protected float maxPlayerHealth;
    [SerializeField] protected float maxPlayerMana;
    [SerializeField] protected float maxPlayerProtection;
    [SerializeField] protected float playerXPtoLVL;

    protected float playerHealth;
    protected float playerMana;
    protected float playerProtection;
    protected int playerLVL;
    protected float playerXP;
    [SerializeField] protected float gemDMG;
    [SerializeField] protected float caneDMG;

    public event Action<float> OnMaxHpChanged;
    public event Action<float> OnMaxManaChanged;
    public event Action<float> OnManaChanged;
    public event Action<float> OnHpChanged;

    void Start()
    {
        Inicialization();
    }

    public void Inicialization()
    {
        maxPlayerHealth = data.MaxPlayerHP;
        maxPlayerMana = data.MaxPlayerMana;
        maxPlayerProtection = data.MaxPlayerProtection;
        playerHealth = maxPlayerHealth;
        playerMana = maxPlayerMana;
        playerProtection = maxPlayerProtection;
    }


    public float GemDMG
    {
        get { return gemDMG; }
        set { gemDMG = value; }
    }
    public float CaneDMG
    {
        get { return caneDMG; }
        set { caneDMG = value; }
    }

    public float MaxPlayerHealth
    {
        get { return maxPlayerHealth; }
        set
        {
            maxPlayerHealth = value;
            OnMaxHpChanged?.Invoke(maxPlayerHealth);
        }
    }
    public float MaxPlayerMana
    {
        get { return maxPlayerMana; }
        set
        {
            maxPlayerMana = value;
            OnMaxManaChanged?.Invoke(maxPlayerMana);
        }
    }
    public float MaxPlayerProtection
    {
        get { return maxPlayerProtection; }
        set
        {
            maxPlayerProtection = value;
        }
    }
    public float PlayerXPtoLVL
    {
        get { return playerXPtoLVL; }
        set { playerXPtoLVL = value; }
    }
    public float PlayerHealth
    {
        get { return playerHealth; }
        set
        {
            playerHealth = value;
            OnHpChanged?.Invoke(playerHealth);
        }
    }
    public float PlayerMana
    {
        get { return playerMana; }
        set
        {
            playerMana = value;
            OnManaChanged?.Invoke(playerMana);
        }
    }
    public float PlayerProtection
    {
        get { return playerProtection; }
        set { playerProtection = value; }
    }
    public int PlayerLVL
    {
        get { return playerLVL; }
        set { playerLVL = value; }
    }
}
