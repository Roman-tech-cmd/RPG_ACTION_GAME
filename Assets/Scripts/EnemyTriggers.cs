using System;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{

    public static event Action<bool> PlayerDetected;
    public static event Action<bool> PlayerLost;
    public static event Action<bool> EnemyStartAttack;
    public static event Action<bool> EnemyStopAttack;

    [SerializeField] private bool isAttackTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&isAttackTrigger==false)
        {
            PlayerDetected?.Invoke(true);
        }
        if (collision.CompareTag("Player") && isAttackTrigger == true)
        {
            EnemyStartAttack?.Invoke(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttackTrigger == false)
        {
            PlayerLost?.Invoke(true);
        }
        if (collision.CompareTag("Player") && isAttackTrigger == true)
        {
            EnemyStopAttack?.Invoke(false);
        }
    }
}
