using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_For_Move_Atack : MonoBehaviour
{

    public Transform PlayerTransform;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Stopping_Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, enemy.speed * Time.deltaTime);
          
        }

        else if (Vector2.Distance(transform.position, PlayerTransform.position) < enemy.Distance_Retreat)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, -enemy.speed * Time.deltaTime);
            
        }

        else if (Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Distance_Retreat && Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Stopping_Distance)
        {
            transform.position = this.transform.position;
        }


    }
}
