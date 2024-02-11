using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Speed_Walk=250f;
    public float Default_Speed = 250f;

    private float horizontalInput = 0, verticalInput = 0;
    private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    private int angleCoeff = 30;

    [HideInInspector]
    public MoveState moveState = MoveState.Idle;
    [HideInInspector]
    public ViewSide viewSide = ViewSide.Right;

    private InputHandler input;

    Transform transform;
    Rigidbody2D rb;
    Animator animatorContoller;
    float timeWalk = 0, walkKooldown = 0.08f; 
    private const float attackCooldown = 1.4f;
    Vector3 Default_State;

    void Start()
    {
        input = InputHandler.Instance;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorContoller = GetComponent<Animator>();
        Default_State = 
            new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        AnimateMovement();
        AnimateAttack();
    }

    private void AnimateAttack() 
    {
        if (input.AttackTriggered)
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
            FlipSprite(horizontalInput, verticalInput);
        float speed = Speed_Walk * (input.RunTriggered ? 2 : 1) 
            * (input.AttackTriggered ? 0 : 1);
        rb.velocity = speed * Time.deltaTime * new Vector2(horizontalInput, verticalInput);
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
        if (input.RunTriggered)
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
        else if (input.AttackTriggered)
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
        //if (moveState == MoveState.Walk)
        //{
        //    if(viewSide == ViewSide.Up_Right)
        //    {
        //        rb.velocity = new Vector2(1,1) * Time.deltaTime * Speed_Walk;
        //    }

        //    if (viewSide == ViewSide.Up_Left)
        //    {
        //        rb.velocity = new Vector2(-1, 1) * Time.deltaTime * Speed_Walk;
        //    }

        //    if (viewSide == ViewSide.Down_Left)
        //    {
        //        rb.velocity = new Vector2(-1, -1) * Time.deltaTime * Speed_Walk;
        //    }

        //    if (viewSide == ViewSide.Down_Right)
        //    {
        //        rb.velocity = new Vector2(1, -1) * Time.deltaTime * Speed_Walk;
        //    }

        //    if (viewSide == ViewSide.Right)
        //    {
        //        rb.velocity = Vector2.right * Time.deltaTime * Speed_Walk;
        //    }
        //    if (viewSide == ViewSide.Left)
        //    {
        //        rb.velocity =  Vector2.left * Time.deltaTime * Speed_Walk;
        //    }
        //    if(viewSide == ViewSide.OnMe)
        //    {
        //        rb.velocity = Vector2.down * Time.deltaTime * Speed_Walk;
        //    }
        //    if (viewSide == ViewSide.OnScreen)
        //    {
        //        rb.velocity = Vector2.up * Time.deltaTime * Speed_Walk;
        //    }

        //    timeWalk -= Time.deltaTime;

        //    if (timeWalk <= 0)
        //    {
        //        rb.velocity = new Vector2(0,0);
        //        moveState = MoveState.Idle;
        //        animatorContoller.Play("Idol_Animation");
        //    }

        //}
        //else if (moveState == MoveState.Run)
        //{
        //    if (viewSide == ViewSide.Up_Right)
        //    {
        //        rb.velocity = new Vector2(1, 1) * Time.deltaTime * Speed_Walk*2;
        //    }

        //    if (viewSide == ViewSide.Up_Left)
        //    {
        //        rb.velocity = new Vector2(-1, 1) * Time.deltaTime * Speed_Walk*2;
        //    }

        //    if (viewSide == ViewSide.Down_Left)
        //    {
        //        rb.velocity = new Vector2(-1, -1) * Time.deltaTime * Speed_Walk*2;
        //    }

        //    if (viewSide == ViewSide.Down_Right)
        //    {
        //        rb.velocity = new Vector2(1, -1) * Time.deltaTime * Speed_Walk*2;
        //    }

        //    if (viewSide == ViewSide.Right)
        //    {
        //        rb.velocity = Vector2.right * Time.deltaTime * Speed_Walk * 2;
        //    }
        //    if (viewSide == ViewSide.Left)
        //    {
        //        rb.velocity = Vector2.left * Time.deltaTime * Speed_Walk * 2;
        //    }
        //    if (viewSide == ViewSide.OnMe)
        //    {
        //        rb.velocity = Vector2.down * Time.deltaTime * Speed_Walk * 2;
        //    }
        //    if (viewSide == ViewSide.OnScreen)
        //    {
        //        rb.velocity = Vector2.up * Time.deltaTime * Speed_Walk * 2;
        //    }

        //    timeWalk -= Time.deltaTime;

        //    if (timeWalk <= 0)
        //    {
        //        rb.velocity = new Vector2(0, 0);
        //        moveState = MoveState.Idle;
        //        animatorContoller.Play("Idol_Animation");
        //    }
        //}
        //else if (moveState == MoveState.Attack)
        //{
        //    timeWalk -= Time.deltaTime;

        //    if (timeWalk <= 0)
        //    {
        //        rb.velocity = new Vector2(0, 0);
        //        moveState = MoveState.Idle;
        //        animatorContoller.Play("Idol_Animation");
        //    }
        //}
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

   public void Walk_Move_Left()
   {
        transform.localScale = Default_State;
        moveState = MoveState.Walk;
        //if (viewSide == ViewSide.Right)
        //{
        transform.localScale = 
            new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
           
        //}
        viewSide = ViewSide.Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Right");
        
    }

   public void Walk_Move_Right()
    {

        transform.localScale = Default_State;
        moveState = MoveState.Walk;
        viewSide = ViewSide.Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Right");
       
    }

   public void Walk_Move_Up()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.OnScreen;
        timeWalk = walkKooldown;
        animatorContoller.Play("Move_Up");
    }

   public void Walk_Move_Down()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.OnMe;
        timeWalk = walkKooldown;
        animatorContoller.Play("Move_Down");
    }

    public void Walk_Move_Down_Right()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Walk;
        viewSide = ViewSide.Down_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Down_Right");
        transform.localScale = Default_State;

    }

    public void Walk_Move_Down_Left()
    {
        transform.localScale = Default_State;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Walk;
        viewSide = ViewSide.Down_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Down_Right");
    }

    public void Walk_Move_Up_Right()
    {
        transform.localScale = Default_State;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Walk;
        viewSide = ViewSide.Up_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Up_Left");
    }

    public void Walk_Move_Up_Left()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Walk;
        viewSide = ViewSide.Up_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Up_Left");
    }

    public void Run_Move_Left()
    {
        transform.localScale = Default_State;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Run;
        viewSide = ViewSide.Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Right");
    }

    public void Run_Move_Right()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Run;
        viewSide = ViewSide.Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Right");
    }

    public void Run_Move_Up()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Run;
        viewSide = ViewSide.OnScreen;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Up");

    }

    public void Run_Move_Down()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Run;
        viewSide = ViewSide.OnMe;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Down");
    }

    public void Run_Move_Down_Right()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Run;
        viewSide = ViewSide.Down_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Down_Right");
    }

    public void Run_Move_Down_Left()
    {
        transform.localScale = Default_State;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Run;
        viewSide = ViewSide.Down_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Down_Right");
    }

    public void Run_Move_Up_Right()
    {
        transform.localScale = Default_State;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Run;
        viewSide = ViewSide.Up_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Up_Left");
    }

    public void Run_Move_Up_Left()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Run;
        viewSide = ViewSide.Up_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Run_Up_Left");
    }

    public void Attack_Left()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Attack;
        viewSide = ViewSide.Left;
        transform.localScale = 
            new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Right");
    }

    public void Attack_Right()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Attack;
        viewSide = ViewSide.Right;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Right");
    }

    public void Attack_Up()
    {
        moveState = MoveState.Attack;
        viewSide = ViewSide.OnScreen;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Up");
    }

    public void Attack_Down() 
    {
        moveState = MoveState.Attack;
        viewSide = ViewSide.OnMe;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Down");
    }

    public void Attack_Up_Left()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Attack;
        viewSide = ViewSide.Up_Left;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Up_Left");
    }

    public void Attack_Down_Right()
    {
        transform.localScale = Default_State;
        moveState = MoveState.Attack;
        viewSide = ViewSide.Down_Right;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Down_Right");
        transform.localScale = Default_State;
    }

    // Some issues with viewing
    public void Attack_Up_Right()
    {
        transform.localScale = Default_State;
        transform.localScale = 
            new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Attack;
        viewSide = ViewSide.Up_Right;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Up_Left");
    }

    public void Attack_Down_Left()
    {
        transform.localScale = Default_State;
        transform.localScale = 
            new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        moveState = MoveState.Attack;
        viewSide = ViewSide.Down_Left;
        timeWalk = attackCooldown;
        animatorContoller.Play("Attack_Down_Right");
    }

    public void DeadAnimation()
    {
        transform.localScale = Default_State;
        animatorContoller.Play("Die_Player");
    }
}
