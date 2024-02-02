using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Tracks : MonoBehaviour
{
    public GameObject SpawnTracks;
    public GameObject Track;
    public Player player;
    public Player.ViewSide viewSide;

    void Start()
    {
       // player = GetComponent<Player>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Sand_Surface")
        {
            Debug.Log("Text: ");
            viewSide = player.viewSide;

            if (viewSide == Player.ViewSide.Down_Left || viewSide == Player.ViewSide.Up_Left || viewSide == Player.ViewSide.Left)
            {
                Instantiate(Track, SpawnTracks.transform.position, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.Down_Right || viewSide == Player.ViewSide.Up_Right || viewSide == Player.ViewSide.Right)
            {
                Instantiate(Track, SpawnTracks.transform.position, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.OnScreen) // от нас
            {
                Instantiate(Track, SpawnTracks.transform.position, transform.rotation);
            }

            else if (viewSide == Player.ViewSide.OnMe)   // к нам
            {
                Instantiate(Track, SpawnTracks.transform.position, transform.rotation);
            }
        }
    }

}
