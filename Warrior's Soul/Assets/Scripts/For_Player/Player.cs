using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    float Speed_Walk=250f;
    public float Default_Speed = 250;

    private MoveState moveState = MoveState.Idle;
    public ViewSide viewSide = ViewSide.Right;
    Transform transform;
    Rigidbody2D rb;
    Animator animatorContoller;
    float timeWalk = 0, walkKooldown = 0.08f;
    Vector3 Default_State;


    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorContoller = GetComponent<Animator>();
        Default_State = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (moveState == MoveState.Walk)
        {
            if(viewSide == ViewSide.Up_Right)
            {
                rb.velocity = new Vector2(1,1) * Time.deltaTime * Speed_Walk;
            }

            if (viewSide == ViewSide.Up_Left)
            {
                rb.velocity = new Vector2(-1, 1) * Time.deltaTime * Speed_Walk;
            }

            if (viewSide == ViewSide.Down_Left)
            {
                rb.velocity = new Vector2(-1, -1) * Time.deltaTime * Speed_Walk;
            }

            if (viewSide == ViewSide.Down_Right)
            {
                rb.velocity = new Vector2(1, -1) * Time.deltaTime * Speed_Walk;
            }

            if (viewSide == ViewSide.Right)
            {
                rb.velocity = Vector2.right * Time.deltaTime * Speed_Walk;
            }
            if (viewSide == ViewSide.Left)
            {
                rb.velocity =  Vector2.left * Time.deltaTime * Speed_Walk;
            }
            if(viewSide == ViewSide.OnMe)
            {
                rb.velocity = Vector2.down * Time.deltaTime * Speed_Walk;
            }
            if (viewSide == ViewSide.OnScreen)
            {
                rb.velocity = Vector2.up * Time.deltaTime * Speed_Walk;
            }

            timeWalk -= Time.deltaTime;

            if (timeWalk <= 0)
            {
                rb.velocity = new Vector2(0,0);
                moveState = MoveState.Idle;
                animatorContoller.Play("Idol_Animation");
            }
            
        }

        else if (moveState == MoveState.Run)
        {
            if (viewSide == ViewSide.Up_Right)
            {
                rb.velocity = new Vector2(1, 1) * Time.deltaTime * Speed_Walk*2;
            }

            if (viewSide == ViewSide.Up_Left)
            {
                rb.velocity = new Vector2(-1, 1) * Time.deltaTime * Speed_Walk*2;
            }

            if (viewSide == ViewSide.Down_Left)
            {
                rb.velocity = new Vector2(-1, -1) * Time.deltaTime * Speed_Walk*2;
            }

            if (viewSide == ViewSide.Down_Right)
            {
                rb.velocity = new Vector2(1, -1) * Time.deltaTime * Speed_Walk*2;
            }

            if (viewSide == ViewSide.Right)
            {
                rb.velocity = Vector2.right * Time.deltaTime * Speed_Walk * 2;
            }
            if (viewSide == ViewSide.Left)
            {
                rb.velocity = Vector2.left * Time.deltaTime * Speed_Walk * 2;
            }
            if (viewSide == ViewSide.OnMe)
            {
                rb.velocity = Vector2.down * Time.deltaTime * Speed_Walk * 2;
            }
            if (viewSide == ViewSide.OnScreen)
            {
                rb.velocity = Vector2.up * Time.deltaTime * Speed_Walk * 2;
            }

            timeWalk -= Time.deltaTime;

            if (timeWalk <= 0)
            {
                rb.velocity = new Vector2(0, 0);
                moveState = MoveState.Idle;
                animatorContoller.Play("Idol_Animation");
            }

        }
    }

    public enum MoveState
    {
        Idle,
        Walk,
        Run
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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
           
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



}
