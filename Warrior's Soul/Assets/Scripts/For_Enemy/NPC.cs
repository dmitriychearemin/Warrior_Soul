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

    [field: SerializeField] public AI_Type Type { get; private set; }

    public float speed;
    //private CharacterStats stats;

    private float animationTime;
    private const float walkDuration = 0.08f;
    private const float attackDuration = 1.2f;

    [field: SerializeField]public float Visibility_radius { get; private set; }
    [field: SerializeField]public float Stopping_Distance { get; private set; }
    [field: SerializeField]public float Distance_Retreat { get; private set; }
    // Заглушки
    private bool runTriggered;
    private bool attackTriggered;

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
        transform = GetComponent<Transform>();
        Default_State =
            new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    public void Animate(Vector2 origin, Vector2 target)
    {
        DefineAngle(origin, target, 20);
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
