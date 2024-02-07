using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Rooms_Spawn : MonoBehaviour
{

    public Direction direction;

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    private Rooms_Variants variants;
    private bool Spawned = false;
    private int rand;
    private float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Room").GetComponent<Rooms_Variants>();
        Destroy(gameObject,waitTime);
        Invoke("Spawn_Room", 0.2f);
    }

    void Update()
    {
        
    }

    void Spawn_Room()
    {
        if (Spawned==false) {
            if(direction == Direction.Up)
            {
                rand = Random.Range(0, variants.Rooms_Up.Length);
                Instantiate(variants.Rooms_Up[rand], transform.position, variants.Rooms_Up[rand].transform.rotation);            
            }

            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, variants.Rooms_Down.Length);
                Instantiate(variants.Rooms_Down[rand], transform.position, variants.Rooms_Down[rand].transform.rotation);

            }

            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.Rooms_Left.Length);
                Instantiate(variants.Rooms_Left[rand], transform.position, variants.Rooms_Left[rand].transform.rotation);

            }

            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.Rooms_Right.Length);
                Instantiate(variants.Rooms_Right[rand], transform.position, variants.Rooms_Right[rand].transform.rotation);

            }
            Spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Point_Spawn_Room") && collision.GetComponent<Rooms_Spawn>().Spawned)
        {
            Destroy(gameObject);
        }
            
        if (collision.tag == "Floor1")
        {
            Destroy(gameObject);
        }
    }
}
