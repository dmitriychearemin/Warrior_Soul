using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Tracks : MonoBehaviour
{
    [SerializeField] private GameObject Sand_Track;
    [SerializeField] private GameObject Grace_Track;
    [SerializeField] private Player player;
    private Character character;

    private int trackCount = 0;
    private readonly List<GameObject> tracks = new();

    private bool can_track = true;
    private float cooldown_track = 0;
    private float lifeTimer = 0f;

    private readonly float lifeTimeTrack = 30f; // Для будущего создания лайфтайма объектов
    private const float dissapearTime = 5f;

    private void Awake()
    {
        character = player.GetComponent<Character>();
    }

    private void Update()
    {
        if(!can_track)
        {
            cooldown_track += Time.deltaTime;
            if (cooldown_track >= 0.43f)
            {
                can_track = true;
                cooldown_track = 0;
            }
        }

        if (trackCount >= 45 || (lifeTimer >= lifeTimeTrack && trackCount > 0))
            TrackDestroy(tracks[0]);
        else if (lifeTimer >= lifeTimeTrack)
            lifeTimer = 0;
        lifeTimer += Time.deltaTime;
    }

    public void TrackDestroy(GameObject obj)
    {
        if (tracks.Contains(obj))
        {
            tracks.Remove(obj);
            StartCoroutine(FadeTracks(obj));
            lifeTimer = 0;
            trackCount--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Sand") && can_track)
        {
            can_track = false;
            var viewSide = character.ViewSide;
            var cur_pos = new Vector3(
                gameObject.transform.position.x, gameObject.transform.position.y, 1f);
            GameObject obj = null;

            switch (viewSide)
            {
                case ViewSide.Left:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 180))));
                    break;
                case ViewSide.Right:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 0))));
                    break;
                case ViewSide.OnScreen:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 90))));
                    break;
                case ViewSide.OnMe:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 270))));
                    break;
                case ViewSide.Down_Right:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 315))));
                    break;
                case ViewSide.Up_Right:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 45))));
                    break;
                case ViewSide.Up_Left:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 135))));
                    break;
                case ViewSide.Down_Left:
                    tracks.Add(
                        obj = Instantiate(Sand_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 225))));
                    break;
            }
            var track = obj.GetComponent<Tracks>();
            track.Parent = this;
            trackCount++;
        }

        if (collision.transform.CompareTag("Grace") && can_track)
        {
            can_track = false;
            var viewSide = character.ViewSide;
            var cur_pos = new Vector3(
                gameObject.transform.position.x, gameObject.transform.position.y, 1f);

            switch (viewSide)
            {
                case ViewSide.Left:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 180))));
                    break;
                case ViewSide.Right:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 0))));
                    break;
                case ViewSide.OnScreen:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 90))));
                    break;
                case ViewSide.OnMe:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 270))));
                    break;
                case ViewSide.Down_Right:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 315))));
                    break;
                case ViewSide.Up_Right:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 45))));
                    break;
                case ViewSide.Up_Left:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 135))));
                    break;
                case ViewSide.Down_Left:
                    tracks.Add(
                        Instantiate(Grace_Track, cur_pos, Quaternion.Euler(new Vector3(0, 0, 225))));
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
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(obj);
        yield break;
    }
}