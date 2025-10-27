using System.Collections;
using UnityEngine;

public class PlayerGemAttack : MonoBehaviour
{
    private bool canAttack = true;
    private GameObject attackGem;
    private BaseItem currentItem;
    private GameObject spawnedItemAttack;
    [SerializeField] private GameObject hand;
    int stageAttack = 0;
    float cooldownGemAttack;
    float manaCostGemAttack;
    bool canGemAttack = true;


    public GameObject AttackGem
    {
        get => attackGem;
        set => attackGem = value;
    }
    public BaseItem CurrentItem
    {
        get => currentItem;
        set
        {
            currentItem = value;
            _cachedGemItem = currentItem;
        }
    }

    private BaseItem _cachedGemItem;
    [SerializeField] private BaseAttack _cachedBaseAttack;
    private PlayerCaneAttack _cachedPlayerCaneAttack;

    void Start()
    {
        _cachedPlayerCaneAttack = GetComponent<PlayerCaneAttack>();
        PlayerMovement.InventoryOpenOrClosed += StopAttack;
        PlayerMovement.InventoryOpenOrClosed += InventoryOpen;
    }
    void Update()
    {
        if (canAttack == false) return;
        HandleRightClickInput();
        StageDetecter();
    }


    public void SetNewItem(GameObject newItem)
    {
        if (newItem != null)
        {
            currentItem = newItem.GetComponent<BaseItem>();
            AttackGem = currentItem.AttackItem;
            _cachedGemItem = currentItem;
        }
        else
        {
            currentItem = null;
            AttackGem = null;
            _cachedGemItem = null;
        }

    }

    private void HandleRightClickInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (attackGem != null && canGemAttack)
        {
            if (PlayerProcessor.Instant.EnoughMana(manaCostGemAttack))
            {
                _cachedPlayerCaneAttack.StopAttack();
                CurrentItem = _cachedGemItem;
                AttackItem();
            }
        }
        else
        {
            StopAttack();
        }
    }

    public void StopAttack()
    {
        stageAttack = 0;
        if (spawnedItemAttack != null)
        {
            Destroy(spawnedItemAttack);
            spawnedItemAttack = null;
        }
    }

    private int GetNextStage(int currentStage) => (currentStage + 1) % 3;
    public void AttackItem() => stageAttack = GetNextStage(stageAttack);
    public void StageDetecter()
    {
        if (stageAttack == 0) return;

        switch (stageAttack)
        {
            case 1:
                HandleStage1();
                break;
            case 2:
                HandleStage2();
                break;
        }
    }
    private void HandleStage1()
    {
        if (spawnedItemAttack == null)
        {
            spawnedItemAttack = Instantiate(AttackGem, hand.transform.position, Quaternion.identity);
        }
        _cachedBaseAttack = spawnedItemAttack.GetComponent<BaseAttack>();
        if (_cachedBaseAttack == null)
        {
            return;
        }

        _cachedBaseAttack.RareItem = currentItem.RareItem;
        _cachedBaseAttack.Damage = currentItem.Damage;
        _cachedBaseAttack.AttackElement = currentItem.Element;
        spawnedItemAttack.transform.SetParent(hand.transform);
        _cachedBaseAttack.CreateTrajectory();

        cooldownGemAttack = _cachedBaseAttack.Cooldown;
        manaCostGemAttack = _cachedBaseAttack.CostMana;
    }

    private void HandleStage2()
    {
        if (_cachedBaseAttack == null) return;

        _cachedBaseAttack.Attack();
        canGemAttack = false;
        if (_cachedBaseAttack.AttackIsHappend)
        {
            StartCoroutine(GemCooldown(cooldownGemAttack));
            _cachedBaseAttack.AttackIsHappend = false;
        }
        else
        {
            canGemAttack = true;
        }

        StopAttack();
    }

    public IEnumerator GemCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canGemAttack = true;
    }
    public void InventoryOpen()
    {
        canAttack = !canAttack;
    }

}