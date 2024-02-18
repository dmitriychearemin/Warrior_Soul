using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterStats playerStats;
    private float Speed_Walk=250f;
    [SerializeField]private float Default_Speed = 250f;

    private float horizontalInput = 0, verticalInput = 0;
    private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    private const int angleCoeff = 30;

    private static MoveState moveState = MoveState.Idle;
    private static ViewSide viewSide = ViewSide.Right;

    [SerializeField]private Transform attackCollider;

    private InputHandler input;
    private new Transform transform;
    Rigidbody2D rb;
    Animator animatorContoller;
    private float animationTime = 0, walkDuration = 0.08f; 
    private const float attackDuration = 1.2f;
    Vector3 Default_State;

    private void Awake()
    {
        Speed_Walk = Default_Speed;
        playerStats = GetComponent<CharacterStats>();
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
        if (input.AttackTriggered && playerStats.Stamina > 0 
            && !(horizontalInput != 0 || verticalInput != 0))
        {
            var x = input.MousePosInput.x;
            var y = input.MousePosInput.y;

            float angle = (float)Math.Atan2(y - halfScreenY, x - halfScreenX) 
                * (float)(180 / Math.PI);
            if (angle > 0)
            {
                if (angle <= 90)
                {
                    if (angle < 45 - angleCoeff)
                        FlipSprite(1f, 0f);
                    else if (angle > 45 + angleCoeff)
                        FlipSprite(0f, 1f);
                    else
                        FlipSprite(1f, 1f);
                }
                else
                {
                    if (angle > 135 + angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle < 135 - 10)
                        FlipSprite(0f, 1f);
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
                        FlipSprite(0f, -1f);
                    else
                        FlipSprite(1f, -1f);
                }
                else
                {
                    if (angle < -135 - angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle > -135 + angleCoeff)
                        FlipSprite(0f, -1f);
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
                * (input.RunTriggered && playerStats.Stamina > 0 ? 2 : 1);
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
                    attackCollider.SetLocalPositionAndRotation(new Vector3(-0.59f, 0.89f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                    viewSide = ViewSide.Up_Left;
                }
                else // Up_Right
                {
                    transform.localScale = Default_State;
                    transform.localScale = 
                        new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    attackCollider.SetLocalPositionAndRotation(new Vector3(-0.61f, 0.88f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
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
                    attackCollider.SetLocalPositionAndRotation(new Vector3(0.74f, -0.73f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                    //Debug.Log("Down_Left");
                    viewSide = ViewSide.Down_Left;
                }
                else // Down_right
                {
                    transform.localScale = Default_State;
                    attackCollider.SetLocalPositionAndRotation(new Vector3(0.986f, -0.65f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                    //Debug.Log("Down_right");
                    viewSide = ViewSide.Down_Right;
                }
            }
        }
        else if (verticalInput != 0)
        {
            if (verticalInput > 0) // OnScreen
            {
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, 0.93f),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
                //Debug.Log("Screen");
                viewSide = ViewSide.OnScreen;
            }
            else // OnMe
            {
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, -1.08f),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
               // Debug.Log("OnME");
                viewSide = ViewSide.OnMe;
            }
        }
        else
        {
            if (horizontalInput > 0) // Right
            {
                transform.localScale = Default_State;
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                        Quaternion.Euler(new Vector3(0, 0, 90)));
                //Debug.Log("Rigth");
                viewSide = ViewSide.Right;
            }
            else // Left
            {
                transform.localScale = Default_State;
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                        Quaternion.Euler(new Vector3(0, 0, 90)));
                //Debug.Log("Left");
                viewSide = ViewSide.Left;
            }
        }
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (input.RunTriggered && playerStats.Stamina > 0)
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
            animationTime = walkDuration;
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
                    animatorContoller.Play("Attack_Down");
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
                    animatorContoller.Play("Attack_Up");
                    break;
                default:
                    break;
            }
            animationTime = attackDuration;
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
            animationTime = walkDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = input.MoveInput.x;
        verticalInput = input.MoveInput.y;

        animationTime -= Time.deltaTime;

        if (animationTime <= 0)
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
        Attack,
        Menu_Open
    }

    public void Set_Move_State(MoveState state)
    {
        moveState = state;
    }

    public static MoveState GetMoveState() => moveState;
    public static ViewSide GetViewSide() => viewSide;

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
