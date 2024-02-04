using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Keyboard_control : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
       player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HitPoint.GetStamina() > 0)
        {
            //////////////////////////////////////////////
            /// Направление бега

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Up_Left();
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Up_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Down_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Down_Left();
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Up();
            }

            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Left();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Down();
            }

            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                player.Run_Move_Right();
            }

            /////////////////// медленная ходьба

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Up_Left();
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Up_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Down_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Down_Left();
            }

            else if (Input.GetKey(KeyCode.W))
            {
                player.Walk_Move_Up();
            }

            else if (Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Left();
            }

            else if (Input.GetKey(KeyCode.S))
            {
                player.Walk_Move_Down();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Right();
            }
        }
        else
        {
             /////////////////// медленная ходьба

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Up_Left();
            }

            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Up_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Down_Right();
            }

            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Down_Left();
            }

            else if (Input.GetKey(KeyCode.W))
            {
                player.Walk_Move_Up();
            }

            else if (Input.GetKey(KeyCode.A))
            {
                player.Walk_Move_Left();
            }

            else if (Input.GetKey(KeyCode.S))
            {
                player.Walk_Move_Down();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                player.Walk_Move_Right();
            }
        }
    }
}
