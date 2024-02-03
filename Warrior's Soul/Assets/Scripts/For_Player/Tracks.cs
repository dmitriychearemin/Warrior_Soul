using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    int count_cycles = 0;
    //public GameObject player_Rb;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(transform.position.x, transform.position.y, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (count_cycles>=30000* Time.deltaTime)
        {
            Destroy(gameObject);
        }
        count_cycles++;

        /*if(player_Rb.transform.position.x + 50 >= pos.x || player_Rb.transform.position.x - 50 <= pos.x)
        {
            Destroy(gameObject);
        }

        if (player_Rb.transform.position.y + 50 >= pos.y || player_Rb.transform.position.y - 50 <= pos.y)
        {
            Destroy(gameObject);
        }
        */

    }

}
