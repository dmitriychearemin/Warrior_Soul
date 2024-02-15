using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColMove : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, 45));
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            //transform.Rotate(new Vector3(0, 0, 45)); 
            transform.SetLocalPositionAndRotation(new Vector3(0.69f, - 1.69f),
                Quaternion.Euler(new Vector3(0,0,45)));
        }
    }


    public void MoveCollider(Player.ViewSide side)
    {
        //switch (side) 
        //{
        //    case Player.ViewSide.Up_Left:
        //        gameObject.transform.position = Vector3.zero;
        //        break;
        //    case Player.ViewSide.Up_Right:
        //        animatorContoller.Play("Run_Up_Left");
        //        break;
        //    case Player.ViewSide.OnMe:
        //        //gameObject.transform.
        //        break;
        //    case Player.ViewSide.Left:
        //        animatorContoller.Play("Run_Right");
        //        break;
        //    case Player.ViewSide.Right:
        //        animatorContoller.Play("Run_Right");
        //        break;
        //    case Player.ViewSide.Down_Left:
        //        animatorContoller.Play("Run_Down_Right");
        //        break;
        //    case Player.ViewSide.Down_Right:
        //        animatorContoller.Play("Run_Down_Right");
        //        break;
        //    case Player.ViewSide.OnScreen:
        //        gameObject.transform.position = Vector3.zero;
        //        break;
        //    default:
        //        break;
        //}
    }
}
