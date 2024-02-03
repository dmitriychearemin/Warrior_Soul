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

    [HideInInspector]
    private int trackCount = 0;
    private readonly Queue<GameObject> tracks = new();

    private bool can_track = true;
    private int cooldown_track = 0;

    //private float count_cycles = 0; // Для будущего создания лайфтайма объектов
    private const float dissapearTime = 5f;

    private void Update()
    {
        if(can_track == false)
        {
            cooldown_track++;
            if (cooldown_track >= 1500 * Time.deltaTime)
            {
                can_track = true;
                cooldown_track = 0;
            }
        }

        if (trackCount >= 45)
        {
            StartCoroutine(FadeTracks(tracks.Dequeue()));
            trackCount--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Sand") && can_track)
        {
            can_track = false;
            var viewSide = player.viewSide;
            var cur_pos = new Vector3(
                SpawnTracks.transform.position.x, SpawnTracks.transform.position.y, 1f);

            switch (viewSide)
            {
                case Player.ViewSide.Left:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 180))));
                    break;
                case Player.ViewSide.Right:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 0))));
                    break;
                case Player.ViewSide.OnScreen:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 90))));
                    break;
                case Player.ViewSide.OnMe:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 270))));
                    break;
                case Player.ViewSide.Down_Right:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 315))));
                    break;
                case Player.ViewSide.Up_Right:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 45))));
                    break;
                case Player.ViewSide.Up_Left:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 135))));
                    break;
                case Player.ViewSide.Down_Left:
                    tracks.Enqueue(
                        Instantiate(Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 225))));
                    break;
            }
            trackCount++;
        }
    }
    IEnumerator FadeTracks(GameObject obj)
    {
        var timer = 0f;
        var sprite = obj.GetComponent<SpriteRenderer>();
        while (timer <= dissapearTime)
        {
            sprite.material.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, timer));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
        StopCoroutine(FadeTracks(obj));
    }
}