using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation_Player : MonoBehaviour
{

    Transform _Entrance_inside_home;
    Transform _Entrance_outside_home;



    // Start is called before the first frame update
    void Start()
    {
        _Entrance_inside_home = GameObject.Find("Entrance_Iside_Home").GetComponent<Transform>();
        _Entrance_outside_home = GameObject.Find("Entrance_outside_home").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Door_Player_Home")
        {
           
            transform.position = _Entrance_inside_home.position;
        }

        if (collision.name == "Exit_Player_Home")
        {
            Debug.Log("Exit");
            transform.position = _Entrance_outside_home.position;
        }

    }


}
