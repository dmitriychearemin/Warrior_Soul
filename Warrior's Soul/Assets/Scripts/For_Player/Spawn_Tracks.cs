using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Tracks : MonoBehaviour
{
    public GameObject SpawnTracks;
    public GameObject Track;
    public Player player;
    Player.ViewSide viewSide;
    int cooldown=0;
    bool can_track=true;
    Vector3 pos;

    void Start()
    {
       player = GetComponent<Player>();
    }

    
    void Update()
    {
        if (can_track == false)
        {
            cooldown++;
            if(cooldown >= 1200*Time.deltaTime)
            {
                cooldown = 0;
                can_track = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Sand_Surface" && can_track == true)
        {
            can_track = false;
            viewSide = player.viewSide;
            pos = new Vector3(SpawnTracks.transform.position.x, SpawnTracks.transform.position.y, 1f);
            if (viewSide == Player.ViewSide.Down_Left || viewSide == Player.ViewSide.Up_Left || viewSide == Player.ViewSide.Left)
            {
                Instantiate(Track, pos, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.Down_Right || viewSide == Player.ViewSide.Up_Right || viewSide == Player.ViewSide.Right)
            {
                Instantiate(Track, pos, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.OnScreen) // от нас
            {
                Instantiate(Track, pos, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.OnMe)   // к нам
            {
                Instantiate(Track, pos, transform.rotation);
            }
        }
    }
}
