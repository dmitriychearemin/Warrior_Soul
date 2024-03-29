using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class EnemyAttack : MonoBehaviour
{
    public delegate void OnEnemyDamage(float damage);
    public static event OnEnemyDamage EnemyDamage;
    public MoveCamera moveCamera;
    Camera camera;
    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //EnemyDamage?.Invoke(25);
            if(moveCamera != null)
            moveCamera.ShakeCamera();
        }
    }
}
