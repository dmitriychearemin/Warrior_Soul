using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public delegate void OnEnemyDamage(float damage);
    public static event OnEnemyDamage EnemyDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            EnemyDamage?.Invoke(25);
    }
}
