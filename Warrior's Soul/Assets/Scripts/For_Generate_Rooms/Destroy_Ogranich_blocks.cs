using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Ogranich_blocks : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Floor1") || collision.CompareTag("Break_Block") || collision.CompareTag("Block_Wall") || collision.CompareTag("Block_Ogranichitel"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor1") || collision.CompareTag("Break_Block") || collision.CompareTag("Block_Wall") || collision.CompareTag("Block_Ogranichitel"))
        {
            Destroy(gameObject);
        }
    }
}
