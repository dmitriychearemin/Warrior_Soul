using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier_Spawner : MonoBehaviour
{

    public GameObject barrier_vertical;

    [SerializeField]Barrier_State state = Barrier_State.Left;
    Spawning_inside_Room room;

    enum Barrier_State
    {
        Left,
        Up,
        Down,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        room = GetComponentInParent<Spawning_inside_Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Spawn_Barrier()
    {
        Vector3 Room_Pos = GetComponentInParent<Transform>().position;
        if (state == Barrier_State.Left)
        {
            Quaternion rotate = Quaternion.Euler(0, 0, 0);
            Vector3 pos = new Vector3(Room_Pos.x, Room_Pos.y, 0);
            var barrier = Instantiate(barrier_vertical, pos, rotate);
            room.Add_Barrier_In_List(barrier);
        
        }
        else if(state == Barrier_State.Up)
        {
            Quaternion rotate = Quaternion.Euler(0,0,90);
            Vector3 pos = new Vector3(Room_Pos.x, Room_Pos.y, 0);
            
            var barrier = Instantiate(barrier_vertical, pos, rotate);
            room.Add_Barrier_In_List(barrier);
        }

        else if (state == Barrier_State.Right)
        {
            Quaternion rotate = Quaternion.Euler(0, 0, 0);
            Vector3 pos = new Vector3(Room_Pos.x, Room_Pos.y, 0);
            
            var barrier = Instantiate(barrier_vertical, pos, rotate);
            room.Add_Barrier_In_List(barrier);
        }

        else if (state == Barrier_State.Down)
        {
            Quaternion rotate = Quaternion.Euler(0, 0, 90);
            Vector3 pos = new Vector3(Room_Pos.x, Room_Pos.y, 0);
           
            var barrier = Instantiate(barrier_vertical, pos, rotate);
            room.Add_Barrier_In_List(barrier);
        }
        Destroy(gameObject);
    }

}
