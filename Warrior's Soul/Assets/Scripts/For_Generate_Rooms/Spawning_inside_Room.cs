using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawning_inside_Room : MonoBehaviour
{

    int Max_Count_Enemies = 5;
    int Min_Count_Enemies = 1;
    [SerializeField]GameObject Spawner_Enemy;
    [SerializeField] GameObject[] Spawner_Barriers;
    List<GameObject> Barriers = new();
    List<GameObject> Enemies = new();
    int Count_Enemy_in_Room;
    int Cur_Count_Enemy = 0;
    int lvl_Player = 0; // потом будем получать из данных



    // Start is called before the first frame update
    void Start()
    {
        Count_Enemy_in_Room = Random.Range(Min_Count_Enemies, Max_Count_Enemies) + lvl_Player;
        Instantiate_Enemy_Spawner();
    }



    // Update is called once per frame
    void Update()
    {
        
        //if(Cur_Count_Enemy <= 0)
        if(Input.GetKeyDown(KeyCode.F))
        {
           
            foreach(var barier  in Barriers)
            {
                Destroy(barier);
            }

            //For_List_Enemy();
        }

        if (Check_Room_On_Enemies() == false)
        {
            foreach (var barier in Barriers)
            {
                Destroy(barier);
            }
        }

    }

    void Instantiate_Enemy_Spawner()
    {
        bool spawn;
        int cur_count_spawner=0;
        int count_trying=0;
        while (cur_count_spawner < Max_Count_Enemies) {
            spawn = false;
            count_trying = 0;
            while (spawn == false)
            {
                count_trying++;
                Vector3 pos = new Vector3(Random.Range(transform.position.x + 20, transform.position.x - 20), Random.Range(transform.position.y + 10, transform.position.y - 10), 0);
                var obj = Instantiate(Spawner_Enemy, transform);
                obj.transform.position = pos;
                Collider2D collider = obj.GetComponent<Collider2D>();
                if(collider.tag == "Dungeons_Objects")
                {
                    spawn = false;
                    Destroy(obj);
                }
                else
                {
                    spawn = true;
                    cur_count_spawner++;
                    Cur_Count_Enemy++;

                }
                if(count_trying > 1000)
                {
                    break;
                }
            }

        }
        
    }


    public void Player_Inside_Room()
    {
        //Active_Enemies

        if(Cur_Count_Enemy > 0)
        {
            for (int i = 0; i < Spawner_Barriers.Length; i++)
            {
                var Barrier = Spawner_Barriers[i].GetComponent<Barrier_Spawner>();
                Barrier.Spawn_Barrier();

            }
        }
        
    }

    public void Add_Barrier_In_List(GameObject barrier)
    {
        Barriers.Add(barrier);
    }

    bool Check_Room_On_Enemies()
    {
        foreach(Transform obj in transform)
        {
            if(obj.CompareTag("Enemy"))
            {
                return true;
            }
        }

        return false;
    }

}
