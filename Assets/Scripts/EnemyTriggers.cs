using System;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{

    public event Action<bool> PlayerDetected;
    public event Action<bool> PlayerLost;
    public event Action<bool> EnemyStartAttack;
    public event Action<bool> EnemyStopAttack;

    [SerializeField] private bool isAttackTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&isAttackTrigger==false)
        {
            PlayerDetected?.Invoke(true);
        }

        if (collision.CompareTag("Player") && isAttackTrigger == true)
        {
            EnemyStartAttack?.Invoke(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isAttackTrigger == false)
        {
            PlayerDetected?.Invoke(false);
        }

        if (collision.CompareTag("Player") && isAttackTrigger == true)
        {
            EnemyStartAttack?.Invoke(false);
        }
    }
}
