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
                if (variants.Counts_Room <= 2)
                {
                    rand = Random.Range(1, variants.Rooms_Up.Length);
                }
                if(variants.Counts_Room >= variants.Max_Rooms)
                {
                    rand = 0;
                }
                Instantiate(variants.Rooms_Up[rand], transform.position, variants.Rooms_Up[rand].transform.rotation);            
            }

            else if (direction == Direction.Down)
            {
                if (variants.Counts_Room <= 2)
                {
                    rand = Random.Range(1, variants.Rooms_Down.Length);
                }
                if (variants.Counts_Room >= variants.Max_Rooms)
                {
                    rand = 0;
                }
                Instantiate(variants.Rooms_Down[rand], transform.position, variants.Rooms_Down[rand].transform.rotation);

            }

            else if (direction == Direction.Left)
            {
                if (variants.Counts_Room <= 2)
                {
                    rand = Random.Range(1, variants.Rooms_Left.Length);
                }
                if (variants.Counts_Room >= variants.Max_Rooms)
                {
                    rand = 0;
                }
                Instantiate(variants.Rooms_Left[rand], transform.position, variants.Rooms_Left[rand].transform.rotation);

            }

            else if (direction == Direction.Right)
            {
                if (variants.Counts_Room <= 2)
                {
                    rand = Random.Range(1, variants.Rooms_Right.Length);
                }
                if (variants.Counts_Room >= variants.Max_Rooms)
                {
                    rand = 0;
                }
                Instantiate(variants.Rooms_Right[rand], transform.position, variants.Rooms_Right[rand].transform.rotation);

            }
            Spawned = true;
            variants.Counts_Room++;
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
