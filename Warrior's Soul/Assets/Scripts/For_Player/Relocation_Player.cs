using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relocation_Player : MonoBehaviour
{

    Transform _Entace_inside_home;
    Transform _Entace_outside_home;



    // Start is called before the first frame update
    void Start()
    {
        _Entace_inside_home = GameObject.Find("Entrance_Iside_Home").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Door_Player_Home")
        {
            Debug.Log("dgdfh");
            transform.position = _Entace_inside_home.position;
        }
    }


}
