using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class NPC : Character
{
    private Animator animatorContoller;
    private Rigidbody2D rb;

    public float speed;
    //private CharacterStats stats;

    private float animationTime;
    private const float walkDuration = 0.08f;
    private const float attackDuration = 1.2f;

    public float Visibility_radius;
    public float Stopping_Distance;
    public float Distance_Retreat;
    // Заглушки
    private bool runTriggered;
    private bool attackTriggered;

    [SerializeField]public AI_Type Type { get; private set; }

    public enum AI_Type
    {
        Friendly,
        Enemy
    }

    private void Awake()
    {
        animatorContoller = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        AnimateMovement();
    }

    // Update is called once per frame
    void Update()
    {
        animationTime -= Time.deltaTime;

        if (animationTime <= 0)
        {
            rb.velocity = Vector2.zero;
            MoveState = MoveState.Idle;
            animatorContoller.Play("Idol_Animation");
        }
    }

    protected override void AnimateMovement()
    {
        //if (horizontalInput != 0 || verticalInput != 0)
        //{
        //    FlipSprite(horizontalInput, verticalInput);
        //    float speed = Speed_Walk * (runTriggered ? 2 : 1);
        //    rb.velocity = speed * Time.deltaTime * new Vector2(horizontalInput, verticalInput);
        //}
    }

    protected override void ChangeAnimation()
    {
        if (runTriggered)
        {
            MoveState = MoveState.Run;
            switch (ViewSide)
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Run_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Run_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Run_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Run_Up");
                    break;
                default:
                    break;
            }
            animationTime = walkDuration;
        }
        else if (attackTriggered)
        {
            MoveState = MoveState.Attack;
            switch (ViewSide)
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Attack_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Attack_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Attack_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Attack_Up");
                    break;
                default:
                    break;
            }
            animationTime = attackDuration;
        }
        else
        {
            MoveState = MoveState.Walk;
            switch (ViewSide)
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Move_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Walk_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Walk_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Move_Up");
                    break;
                default:
                    break;
            }
            animationTime = walkDuration;
        }
    }
}
