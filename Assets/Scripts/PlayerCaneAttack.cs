using System.Collections;
using UnityEngine;

public class PlayerCaneAttack : MonoBehaviour
{
    private bool canAttack = true;
    private GameObject attackCane;
    private BaseItem currentItem;
    private GameObject spawnedItemAttack;
    [SerializeField] private GameObject hand;
    int stageAttack = 0;
    float cooldownCaneAttack;
    float manaCostCaneAttack;
    bool canCaneAttack = true;


    public GameObject AttackCane
    {
        get => attackCane;
        set => attackCane = value;
    }
    public BaseItem CurrentItem
    {
        get => currentItem;
        set
        {
            currentItem = value;
            _cachedCaneItem = currentItem;
        }
    }

    private BaseItem _cachedCaneItem;
    [SerializeField] private BaseAttack _cachedBaseAttack;
    private PlayerGemAttack _cachedPlayerGemAttack;

    void Start()
    {
        _cachedPlayerGemAttack = GetComponent<PlayerGemAttack>();
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
            AttackCane = currentItem.AttackItem;
            _cachedCaneItem = currentItem;
        }
        else
        {
            currentItem = null;
            AttackCane = null;
            _cachedCaneItem = null;
        }

    }

    private void HandleRightClickInput()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        if (attackCane != null && canCaneAttack)
        {
            if (PlayerProcessor.Instant.EnoughMana(manaCostCaneAttack))
            {
                _cachedPlayerGemAttack.StopAttack();
                CurrentItem = _cachedCaneItem;
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
            spawnedItemAttack = Instantiate(AttackCane, hand.transform.position, Quaternion.identity);
        }
        _cachedBaseAttack = spawnedItemAttack.GetComponent<BaseAttack>();
        if (_cachedBaseAttack == null)
        {
            return;
        }

        _cachedBaseAttack.GetRareItem((BaseAttack.Rare)currentItem.RareItem);
        _cachedBaseAttack.GetDamage(currentItem.Damage);
        _cachedBaseAttack.GetElement(currentItem.Element);
        spawnedItemAttack.transform.SetParent(hand.transform);
        _cachedBaseAttack.CreateTrajectory();

        cooldownCaneAttack = _cachedBaseAttack.Cooldown;
        manaCostCaneAttack = _cachedBaseAttack.CostMana;
    }

    private void HandleStage2()
    {
        if (_cachedBaseAttack == null) return;

        _cachedBaseAttack.Attack();
        canCaneAttack = false;
        if (_cachedBaseAttack.AttackIsHappend)
        {
            StartCoroutine(CaneCooldown(cooldownCaneAttack));
            _cachedBaseAttack.AttackIsHappend = false;
        }
        else
        {
            canCaneAttack = true;
        }

        StopAttack();
    }

    public IEnumerator CaneCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canCaneAttack = true;
    }
    public void InventoryOpen()
    {
        canAttack = !canAttack;
    }

}