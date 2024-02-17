using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Blocks_Spawn : MonoBehaviour
{


    public GameObject block_Room;
    private bool Spawned = false;
    private float waitTime = 3f;
    Rooms_Variants variant;

    private void Start()
    {
        //Destroy(gameObject,waitTime);
        Invoke("Spawn_Blocks", 0.6f);

    }

    void Update()
    {
        
    }

    void Spawn_Blocks()
    {
        if (Spawned==false) {
            //Instantiate(block_Room,transform.position, transform.rotation);
            Spawned = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point_Spawn_Room"))
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Floor1" || collision.tag =="Break_Block" )
        {
            Destroy(gameObject);
        }
    }
}
