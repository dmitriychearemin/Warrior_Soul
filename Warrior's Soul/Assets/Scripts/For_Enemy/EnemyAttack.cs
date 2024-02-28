using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public delegate void OnEnemyDamage(float damage);
    public static event OnEnemyDamage EnemyDamage;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //        EnemyDamage?.Invoke(25);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            EnemyDamage?.Invoke(100);
    }
}
