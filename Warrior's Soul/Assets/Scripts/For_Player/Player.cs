using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Speed_Walk=250f;
    [SerializeField]private float Default_Speed = 250f;

    private float horizontalInput = 0, verticalInput = 0;
    private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    private const int angleCoeff = 30;

    [HideInInspector]
    public MoveState moveState = MoveState.Idle;
    [HideInInspector]
    public ViewSide viewSide = ViewSide.Right;

    private InputHandler input;
    private new Transform transform;
    Rigidbody2D rb;
    Animator animatorContoller;
    float timeWalk = 0, walkKooldown = 0.08f; 
    private const float attackCooldown = 1.4f;
    Vector3 Default_State;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorContoller = GetComponent<Animator>();
        Default_State =
            new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Start()
    {
        input = InputHandler.Instance;
    }

    private void FixedUpdate()
    {
        AnimateMovement();
        AnimateAttack();
    }

    private void AnimateAttack() 
    {
        if (input.AttackTriggered && HitPoint.GetStamina() > 0 
            && !(horizontalInput != 0 || verticalInput != 0))
        {
            float x = input.MousePosInput.x;
            float y = input.MousePosInput.y;

            float angle = (float)Math.Atan2(y - halfScreenY, x - halfScreenX) 
                * (float)(180 / Math.PI);
            if (angle > 0)
            {
                if (angle <= 90)
                {
                    if (angle < 45 - angleCoeff)
                        FlipSprite(1f, 0f);
                    else if (angle > 45 + angleCoeff)
                        FlipSprite(0f, -1f);
                    else
                        FlipSprite(1f, 1f);
                }
                else
                {
                    if (angle > 135 + angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle < 135 - 10)
                        FlipSprite(0f, -1f);
                    else
                        FlipSprite(-1f, 1f);
                }
            }
            else
            {
                if (angle > -90)
                {
                    if (angle > -45 + angleCoeff)
                        FlipSprite(1f, 0f);
                    else if (angle < -45 - angleCoeff)
                        FlipSprite(0f, 1f);
                    else
                        FlipSprite(1f, -1f);
                }
                else
                {
                    if (angle < -135 - angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle > -135 + angleCoeff)
                        FlipSprite(0f, 1f);
                    else
                        FlipSprite(-1f, -1f);
                }
            }
        }
    }

    private void AnimateMovement()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            FlipSprite(horizontalInput, verticalInput);
            float speed = Speed_Walk 
                * (input.RunTriggered && HitPoint.GetStamina() > 0 ? 2 : 1);
            rb.velocity = speed * Time.deltaTime * new Vector2(horizontalInput, verticalInput);
        }
    }

    private void FlipSprite(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 && verticalInput != 0)
        {
            if (verticalInput > 0)
            {
                if (horizontalInput < 0) // Up_Left
                {
                    transform.localScale = Default_State;
                    viewSide = ViewSide.Up_Left;
                }
                else // Up_Right
                {
                    transform.localScale = Default_State;
                    transform.localScale = 
                        new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    viewSide = ViewSide.Up_Right;
                }
            }
            else
            {
                if (horizontalInput < 0) // Down_Left
                {
                    transform.localScale = Default_State;
                    transform.localScale = 
                        new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    viewSide = ViewSide.Down_Left;
                }
                else // Down_right
                {
                    transform.localScale = Default_State;
                    viewSide = ViewSide.Down_Right;
                }
            }
        }
        else if (verticalInput != 0)
        {
            if (verticalInput > 0) // OnScreen
            {
                viewSide = ViewSide.OnScreen;
            }
            else // OnMe
            {
                viewSide = ViewSide.OnMe;
            }
        }
        else
        {
            if (horizontalInput > 0) // Right
            {
                transform.localScale = Default_State;
                viewSide = ViewSide.Right;
            }
            else // Left
            {
                transform.localScale = Default_State;
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                viewSide = ViewSide.Left;
            }
        }
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (input.RunTriggered && HitPoint.GetStamina() > 0)
        {
            moveState = MoveState.Run;
            switch (viewSide) 
            {
                case Player.ViewSide.Up_Left:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case Player.ViewSide.Up_Right:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case Player.ViewSide.OnMe:
                    animatorContoller.Play("Run_Down");
                    break;
                case Player.ViewSide.Left:
                    animatorContoller.Play("Run_Right");
                    break;
                case Player.ViewSide.Right:
                    animatorContoller.Play("Run_Right");
                    break;
                case Player.ViewSide.Down_Left:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case Player.ViewSide.Down_Right:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case Player.ViewSide.OnScreen:
                    animatorContoller.Play("Run_Up");
                    break;
                default:
                    break;
            }
            timeWalk = walkKooldown;
        }
        else if (input.AttackTriggered && !(horizontalInput != 0 || verticalInput != 0))
        {
            moveState = MoveState.Attack;
            switch (viewSide)
            {
                case Player.ViewSide.Up_Left:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case Player.ViewSide.Up_Right:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case Player.ViewSide.OnMe:
                    animatorContoller.Play("Attack_Up");
                    break;
                case Player.ViewSide.Left:
                    animatorContoller.Play("Attack_Right");
                    break;
                case Player.ViewSide.Right:
                    animatorContoller.Play("Attack_Right");
                    break;
                case Player.ViewSide.Down_Left:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case Player.ViewSide.Down_Right:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case Player.ViewSide.OnScreen:
                    animatorContoller.Play("Attack_Down");
                    break;
                default:
                    break;
            }
            timeWalk = attackCooldown;
        }
        else
        {
            moveState = MoveState.Walk;
            switch (viewSide)
            {
                case Player.ViewSide.Up_Left:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case Player.ViewSide.Up_Right:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case Player.ViewSide.OnMe:
                    animatorContoller.Play("Move_Down");
                    break;
                case Player.ViewSide.Left:
                    animatorContoller.Play("Walk_Right");
                    break;
                case Player.ViewSide.Right:
                    animatorContoller.Play("Walk_Right");
                    break;
                case Player.ViewSide.Down_Left:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case Player.ViewSide.Down_Right:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case Player.ViewSide.OnScreen:
                    animatorContoller.Play("Move_Up");
                    break;
                default:
                    break;
            }
            timeWalk = walkKooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = input.MoveInput.x;
        verticalInput = input.MoveInput.y;

        timeWalk -= Time.deltaTime;

        if (timeWalk <= 0)
        {
            rb.velocity = Vector2.zero;
            moveState = MoveState.Idle;
            animatorContoller.Play("Idol_Animation");
        }
    }

    public enum MoveState
    {
        Idle,
        Walk,
        Run,
        Attack
    }

    public enum ViewSide
    {
        Left,
        Right,
        OnScreen,
        OnMe,
        Up_Right,
        Up_Left,
        Down_Left,
        Down_Right
    }
    public void DeadAnimation()
    {
        transform.localScale = Default_State;
        animatorContoller.Play("Die_Player");
    }
}
