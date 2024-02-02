using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject SpawnBullet;
    int count_cycles = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count_cycles>=10000)
        {
            Destroy(gameObject);
        }
        count_cycles++;
    }

}
