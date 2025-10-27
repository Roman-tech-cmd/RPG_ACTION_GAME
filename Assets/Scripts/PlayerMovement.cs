using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashTime;
    float defaultTime;
    [SerializeField] private float dashSpeed = 10f;
    private bool canDash = true;

    bool canMove;

    [SerializeField] private GameObject Inventory;
    bool inventoryIsActive;

    public static event Action InventoryOpenOrClosed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        canMove = !inventoryIsActive;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryManager.Instance.canClose == true)
            {
                InventoryManager.Instance.UnselectedItems();
                inventoryIsActive = !inventoryIsActive;
                InventoryOpenOrClosed?.Invoke();
            }

        }


        if (inventoryIsActive == true)
        {
            Inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            Inventory.GetComponent<RectTransform>().anchoredPosition = new Vector2(5000, 0);

        }
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) & canDash)
        {
            StartCoroutine(DashCoroutine());
        }

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator DashCoroutine()
    {
        float speed = moveSpeed;
        canDash = false;
        moveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = speed;
        yield return new WaitForSeconds(dashTime / 2f);
        canDash = true;
    }

    public void Dash()
    {
        defaultTime = dashTime;
        if (defaultTime <= 0)
        {
            Debug.Log("Dashing");
        }
        else
        {
            defaultTime -= Time.deltaTime;
        }
    }
}
