using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed_Walk=400f;
    

    private MoveState moveState = MoveState.Idle;
    public ViewSide viewSide = ViewSide.Right;
    Transform transform;
    Rigidbody2D rb;
    Animator animatorContoller;
    float timeWalk = 0, walkKooldown = 0.08f;

    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorContoller = GetComponent<Animator>();

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
    }

    public enum MoveState
    {
        Idle,
        Walk
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

   public void Move_Left()
    {
        moveState = MoveState.Walk;
        //if (viewSide == ViewSide.Right)
        //{
           // transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            viewSide = ViewSide.Left;
        //}
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Left");
    }

   public void Move_Right()
    {
        moveState = MoveState.Walk;
       // if (viewSide == ViewSide.Left)
        //{
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            viewSide = ViewSide.Right;
       // }
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Right");
    }

   public void Move_Up()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.OnScreen;
        timeWalk = walkKooldown;
        animatorContoller.Play("Move_Up");

    }

   public void Move_Down()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.OnMe;
        timeWalk = walkKooldown;
        animatorContoller.Play("Move_Down");
    }

    public void Move_Down_Right()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.Down_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Right");
    }

    public void Move_Down_Left()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.Down_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Left");
    }

    public void Move_Up_Right()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.Up_Right;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Right");
    }

    public void Move_Up_Left()
    {
        moveState = MoveState.Walk;
        viewSide = ViewSide.Up_Left;
        timeWalk = walkKooldown;
        animatorContoller.Play("Walk_Left");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sand_Surface")
        {
            
        }
    }
}
