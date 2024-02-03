using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Spawn_Tracks : MonoBehaviour
{
    public GameObject SpawnTracks;
    public GameObject Track;
    public Player player;

    private bool can_track=true;
    private int cooldown_track = 0;

    private void Update()
    {
        if(can_track == false)
        {
            cooldown_track++;
            if (cooldown_track >= 1500*Time.deltaTime)
            {
                can_track = true;
                cooldown_track = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Sand" && can_track == true)
        {
            can_track = false;
            var viewSide = player.viewSide;
            var cur_pos = new Vector3(SpawnTracks.transform.position.x, SpawnTracks.transform.position.y, 1f);

            //if (viewSide == Player.ViewSide.Down_Left || viewSide == Player.ViewSide.Up_Left 
            //    || viewSide == Player.ViewSide.Left)
            //{
            //    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 180)));
            //}
            //else if (viewSide == Player.ViewSide.Down_Right || viewSide == Player.ViewSide.Up_Right 
            //    || viewSide == Player.ViewSide.Right)
            //{
            //    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 0)));
            //}
            //else if (viewSide == Player.ViewSide.OnScreen) // от нас
            //{
            //    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 90)));
            //}
            //else if (viewSide == Player.ViewSide.OnMe)   // к нам
            //{
            //    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 270)));
            //}
            switch (viewSide)
            {
                case Player.ViewSide.Left:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 180)));
                    break;
                case Player.ViewSide.Right:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 0)));
                    break;
                case Player.ViewSide.OnScreen:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 90)));
                    break;
                case Player.ViewSide.OnMe:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 270)));
                    break;
                case Player.ViewSide.Down_Right:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 315)));
                    break;
                case Player.ViewSide.Up_Right:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 45)));
                    break;
                case Player.ViewSide.Up_Left:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 135)));
                    break;
                case Player.ViewSide.Down_Left:
                    Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 225)));
                    break;
            }
        }
    }
}
