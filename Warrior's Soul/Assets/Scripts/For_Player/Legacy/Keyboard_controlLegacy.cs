using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Keyboard_controlLegacy : MonoBehaviour
{
    //Player player;
    //private InputHandler input;

    //private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    //private int angleCoeff = 30;

    //void Awake()
    //{
    //    player = GetComponent<Player>();
    //}

    //void Start()
    //{
    //    input = InputHandler.Instance;
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    if (HitPoint.GetStamina() > 0)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            var mousePos = Input.mousePosition;
    //            switch (GetScreenArea(Input.mousePosition))
    //            {
    //                case Player.ViewSide.Up_Left:
    //                    player.Attack_Up_Left();
    //                    break;
    //                case Player.ViewSide.Up_Right:
    //                    player.Attack_Up_Right();
    //                    break;
    //                case Player.ViewSide.OnMe:
    //                    player.Attack_Up();
    //                    break;
    //                case Player.ViewSide.Left:
    //                    player.Attack_Left();
    //                    break;
    //                case Player.ViewSide.Right:
    //                    player.Attack_Right();
    //                    break;
    //                case Player.ViewSide.Down_Left:
    //                    player.Attack_Down_Left();
    //                    break;
    //                case Player.ViewSide.Down_Right:
    //                    player.Attack_Down_Right();
    //                    break;
    //                case Player.ViewSide.OnScreen:
    //                    player.Attack_Down();
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //        if (Input.GetKey(KeyCode.Delete))
    //            player.DeadAnimation();
    //        //////////////////////////////////////////////
    //        /// Направление бега

    //        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Up_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Up_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Down_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Down_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Up();
    //        }

    //        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Down();
    //        }

    //        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
    //        {
    //            player.Run_Move_Right();
    //        }

    //        /////////////////// медленная ходьба

    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Up_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Up_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Down_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Down_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W))
    //        {
    //            player.Walk_Move_Up();
    //        }

    //        else if (Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.S))
    //        {
    //            player.Walk_Move_Down();
    //        }

    //        else if (Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Right();
    //        }
    //        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
    //        {
    //            player.Attack_Up_Left();
    //        }
    //        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
    //        {
    //            player.Attack_Up_Right();
    //        }
    //        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
    //        {
    //            player.Attack_Down_Left();
    //        }
    //        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
    //        {
    //            player.Attack_Down_Right();
    //        }
    //        else if (Input.GetKey(KeyCode.UpArrow))
    //        {
    //            player.Attack_Up();
    //        }
    //        else if (Input.GetKey(KeyCode.LeftArrow))
    //        {
    //            player.Attack_Left();
    //        }
    //        else if (Input.GetKey(KeyCode.RightArrow))
    //        {
    //            player.Attack_Right();
    //        }
    //        else if (Input.GetKey(KeyCode.DownArrow))
    //        {
    //            player.Attack_Down();
    //        }
    //    }
    //    else
    //    {
    //        /////////////////// медленная ходьба

    //        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Up_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Up_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Down_Right();
    //        }

    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Down_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.W))
    //        {
    //            player.Walk_Move_Up();
    //        }

    //        else if (Input.GetKey(KeyCode.A))
    //        {
    //            player.Walk_Move_Left();
    //        }

    //        else if (Input.GetKey(KeyCode.S))
    //        {
    //            player.Walk_Move_Down();
    //        }

    //        else if (Input.GetKey(KeyCode.D))
    //        {
    //            player.Walk_Move_Right();
    //        }
    //    }
    //}

    //private ViewSide GetScreenArea(Vector3 mousePos)
    //{
    //    float x = mousePos.x;
    //    float y = mousePos.y;

    //    float angle = (float)Math.Atan2(y - halfScreenY, x - halfScreenX) * (float)(180 / Math.PI);
    //    if (angle > 0)
    //    {
    //        if (angle <= 90)
    //        {
    //            if (angle < 45 - angleCoeff)
    //                return Player.ViewSide.Right;
    //            else if (angle > 45 + angleCoeff)
    //                return Player.ViewSide.OnMe;
    //            else
    //                return Player.ViewSide.Up_Right;
    //        }
    //        else
    //        {
    //            if (angle > 135 + angleCoeff)
    //                return Player.ViewSide.Left;
    //            else if (angle < 135 - 10)
    //                return Player.ViewSide.OnMe;
    //            else
    //                return Player.ViewSide.Up_Left;
    //        }
    //    }
    //    else
    //    {
    //        if (angle > -90)
    //        {
    //            if (angle > -45 + angleCoeff)
    //                return Player.ViewSide.Right;
    //            else if (angle < -45 - angleCoeff)
    //                return Player.ViewSide.OnScreen;
    //            else
    //                return Player.ViewSide.Down_Right;
    //        }
    //        else
    //        {
    //            if (angle < -135 - angleCoeff)
    //                return Player.ViewSide.Left;
    //            else if (angle > -135 + angleCoeff)
    //                return Player.ViewSide.OnScreen;
    //            else
    //                return Player.ViewSide.Down_Left;
    //        }
    //    }
    //}
}