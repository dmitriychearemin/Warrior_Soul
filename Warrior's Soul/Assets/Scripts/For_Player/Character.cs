using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: MonoBehaviour
{
    protected new Transform transform;
    protected Vector3 Default_State; 
    
    protected float Speed_Walk;

    protected CharacterStats stats;
    public MoveState MoveState { get; set; } = MoveState.Idle;
    public ViewSide ViewSide { get; set; } = ViewSide.OnMe;

    protected abstract void AnimateMovement();
    protected void DefineAngle(Vector2 origin, Vector2 target, float angleCoeff)
    {
        float angle = (float)Math.Atan2(target.y - origin.y, 
            target.x - origin.x) * (float)(180 / Math.PI);
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
    protected void FlipSprite(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 && verticalInput != 0)
        {
            if (verticalInput > 0)
            {
                if (horizontalInput < 0) // Up_Left
                {
                    transform.localScale = Default_State;
                    //attackCollider.SetLocalPositionAndRotation(new Vector3(-0.59f, 0.89f),
                    //    Quaternion.Euler(new Vector3(0, 0, 50)));
                    ViewSide = ViewSide.Up_Left;
                }
                else // Up_Right
                {
                    transform.localScale = Default_State;
                    transform.localScale =
                        new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    //attackCollider.SetLocalPositionAndRotation(new Vector3(-0.61f, 0.88f),
                    //    Quaternion.Euler(new Vector3(0, 0, 50)));
                    ViewSide = ViewSide.Up_Right;
                }
            }
            else
            {
                if (horizontalInput < 0) // Down_Left
                {
                    transform.localScale = Default_State;
                    transform.localScale =
                        new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    //attackCollider.SetLocalPositionAndRotation(new Vector3(0.74f, -0.73f),
                    //    Quaternion.Euler(new Vector3(0, 0, 50)));
                    //Debug.Log("Down_Left");
                    ViewSide = ViewSide.Down_Left;
                }
                else // Down_right
                {
                    transform.localScale = Default_State;
                    //attackCollider.SetLocalPositionAndRotation(new Vector3(0.986f, -0.65f),
                    //    Quaternion.Euler(new Vector3(0, 0, 50)));
                    //Debug.Log("Down_right");
                    ViewSide = ViewSide.Down_Right;
                }
            }
        }
        else if (verticalInput != 0)
        {
            if (verticalInput > 0) // OnScreen
            {
                //attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, 0.93f),
                //        Quaternion.Euler(new Vector3(0, 0, 0)));
                //Debug.Log("Screen");
                ViewSide = ViewSide.OnScreen;
            }
            else // OnMe
            {
                //attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, -1.08f),
                //        Quaternion.Euler(new Vector3(0, 0, 0)));
                // Debug.Log("OnME");
                ViewSide = ViewSide.OnMe;
            }
        }
        else
        {
            if (horizontalInput > 0) // Right
            {
                transform.localScale = Default_State;
                //attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                //        Quaternion.Euler(new Vector3(0, 0, 90)));
                //Debug.Log("Rigth");
                ViewSide = ViewSide.Right;
            }
            else // Left
            {
                transform.localScale = Default_State;
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                //attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                //        Quaternion.Euler(new Vector3(0, 0, 90)));
                //Debug.Log("Left");
                ViewSide = ViewSide.Left;
            }
        }
        ChangeAnimation();
    }
    protected abstract void ChangeAnimation();

    protected void TakeDamage(float damage) =>
        stats.TakeDamage(damage);
}

public enum MoveState
{
    Idle,
    Walk,
    Run,
    Attack,
    Menu_Open
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
