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
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            player.Move_Up_Left();
        }

        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            player.Move_Up_Right();
        }

        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            player.Move_Down_Right();
        }

        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            player.Move_Down_Left();
        }

        else if (Input.GetKey(KeyCode.W))
        {
            player.Move_Up();
        }

        else if (Input.GetKey(KeyCode.A))
        {
            player.Move_Left();
        }

        else if (Input.GetKey(KeyCode.S))
        {
            player.Move_Down();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            player.Move_Right();
        }
    }


}
