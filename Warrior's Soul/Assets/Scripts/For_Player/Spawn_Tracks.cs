using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Spawn_Tracks : MonoBehaviour
{
    public GameObject SpawnTracks;
    public GameObject Track;
    private Player.ViewSide viewSide;
    public Player player;
<<<<<<< Updated upstream
    Player.ViewSide viewSide;
    int cooldown=0;
    bool can_track=true;
    Vector3 pos;

    void Start()
    {
       player = GetComponent<Player>();
=======

    int cooldown_track = 0;
    bool can_track = true;
    Vector3 cur_pos;

    void Start()
    {
         player = GetComponent<Player>(); 

>>>>>>> Stashed changes
    }

    private void Update()
    {
        if(can_track == false)
        {
            cooldown_track++;
            if (cooldown_track >= 1200*Time.deltaTime)
            {
                can_track = true;
                cooldown_track = 0;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
<<<<<<< Updated upstream
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
=======
        
        if (collision.transform.tag == "Sand" && can_track==true)
        {
            Debug.Log("Rdfsdg");
            can_track = false;
            viewSide = player.viewSide;
            cur_pos = new Vector3(SpawnTracks.transform.position.x, SpawnTracks.transform.position.y, 1f);
            
            if (viewSide == Player.ViewSide.Down_Left || viewSide == Player.ViewSide.Up_Left || viewSide == Player.ViewSide.Left)
            {
                //Vector3 rotate = new Vector3(0, 0, 90);
                Instantiate(Track, cur_pos, transform.rotation);
  
>>>>>>> Stashed changes
            }

            else if (viewSide == Player.ViewSide.Down_Right || viewSide == Player.ViewSide.Up_Right || viewSide == Player.ViewSide.Right)
            {
<<<<<<< Updated upstream
                Instantiate(Track, pos, transform.rotation);
=======

                Instantiate(Track, cur_pos, transform.rotation);
>>>>>>> Stashed changes
            }

            else if (viewSide == Player.ViewSide.OnScreen) // от нас
            {
<<<<<<< Updated upstream
                Instantiate(Track, pos, transform.rotation);
=======
                Instantiate(Track, cur_pos, transform.rotation);
>>>>>>> Stashed changes
            }

            else if (viewSide == Player.ViewSide.OnMe)   // к нам
            {
<<<<<<< Updated upstream
                Instantiate(Track, pos, transform.rotation);
=======
                Instantiate(Track, cur_pos, transform.rotation);
>>>>>>> Stashed changes
            }

        }
    }
}
