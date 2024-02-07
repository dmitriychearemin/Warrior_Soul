using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Door_To_Wall : MonoBehaviour
{
    
    public GameObject wall_block_Horizontal;
    public GameObject wall_block_Vertical;
    public bool Horizontal = true;
    bool can_spawn = true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.CompareTag("Block_Wall") && can_spawn == true))
        {
            if (Horizontal == true)
            {
                Instantiate(wall_block_Horizontal, transform.position, Quaternion.identity);
            }
            else {
                Instantiate(wall_block_Vertical, transform.position, Quaternion.EulerRotation(0,0,1.57f));
                
            }
            can_spawn = false;
            Destroy(gameObject);
        }
     
    }
}
