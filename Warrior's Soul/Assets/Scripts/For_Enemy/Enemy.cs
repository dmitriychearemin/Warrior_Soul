using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Animator animatorControl;

    public float speed;
    private CharacterStats stats;

    public float Visibility_radius;
    public float Stopping_Distance;
    public float Distance_Retreat;

    private void Awake()
    {
        animatorControl = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        stats.TakeDamage(-damage);
    }

    // Update is called once per frame
    void Update()
    {
        //// Просто для примера
        //if (Current_HP <= 0) 
        //{
        //    animatorControl.Play("Die_Player");
        //}
    }
}
