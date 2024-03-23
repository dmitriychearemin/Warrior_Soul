using NavMeshPlus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_For_Move_Atack : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    private NavMeshAgent agent;
    private NPC enemy;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        enemy = GetComponent<NPC>();
        surface.BuildNavMeshAsync();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        if (agent.remainingDistance > agent.stoppingDistance)
            enemy.Animate(transform.position, agent.steeringTarget);
        //Debug.Log((float)Math.Atan2(PlayerTransform.position.y - transform.position.y, 
        //    PlayerTransform.position.x - transform.position.x) * (float)(180 / Math.PI));

        //if (Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Stopping_Distance)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, 
        //        enemy.speed * Time.deltaTime);
        //    enemy.Animate(transform.position, PlayerTransform.position);
        //}

        //else if (Vector2.Distance(transform.position, PlayerTransform.position) < enemy.Distance_Retreat)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position,
        //        PlayerTransform.position, -enemy.speed * Time.deltaTime);
        //    enemy.Animate(new Vector2(-transform.position.x, -transform.position.y), 
        //        new Vector2(-PlayerTransform.position.x, -PlayerTransform.position.y));
        //    //Debug.Log((float)Math.Atan2(-PlayerTransform.position.y - -transform.position.y,
        //    //    -PlayerTransform.position.x - -transform.position.x) * (float)(180 / Math.PI));
        //}

        //else if (Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Distance_Retreat 
        //    && Vector2.Distance(transform.position, PlayerTransform.position) > enemy.Stopping_Distance)
        //{
        //    transform.position = transform.position;
        //}


    }
}
