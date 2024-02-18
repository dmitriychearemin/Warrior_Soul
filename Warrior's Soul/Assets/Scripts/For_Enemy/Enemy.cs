using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Animator animatorControl;

    public float speed;
    public float Max_HitPoints = 100;
    float Current_HP;
    //public float Damage;
    public float Visibility_radius;

    public float Stopping_Distance;
    public float Distance_Retreat;

    // Start is called before the first frame update
    void Start()
    {
        animatorControl = GetComponent<Animator>();
        Current_HP = Max_HitPoints;
        PlayerAttack.PlayerDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        PlayerAttack.PlayerDamage -= TakeDamage;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Attack"))
    //        return;
    //}

    private void TakeDamage(float damage)
    {
        Current_HP -= damage;
        Debug.Log(Current_HP);
    }

    // Update is called once per frame
    void Update()
    {
        // Просто для примера
        if (Current_HP <= 0) 
        {
            animatorControl.Play("Die_Player");
        }
    }
}
