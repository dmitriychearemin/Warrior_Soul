using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Надо подумать над названием
public class PlayerAttack : MonoBehaviour
{
    public delegate void OnPlayerDamage(float damage);
    public static event OnPlayerDamage PlayerDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            PlayerDamage?.Invoke(50);
    }
}
