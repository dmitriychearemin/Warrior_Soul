using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Надо подумать над названием
public class PlayerAttack : MonoBehaviour
{
    public delegate void OnPlayerDamage(float damage);
    public static event OnPlayerDamage PlayerDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            PlayerDamage?.Invoke(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
