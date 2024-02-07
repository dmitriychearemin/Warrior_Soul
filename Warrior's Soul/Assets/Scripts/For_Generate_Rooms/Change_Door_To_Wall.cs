using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Door_To_Wall : MonoBehaviour
{
    
    public GameObject wall_block;

    private void Start()
    {
        //Destroy(gameObject, 2f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Block_Wall")
        {
            Instantiate(wall_block, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        //else if (collision.tag != "Dunjeons_Doors")
        //{
        //    Instantiate(wall_block, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}
       
    }
}
