using System.Collections;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    private int count_cycles = 0;
    private const float dissapearTime = 5f;

    // Update is called once per frame
    void Update()
    {
        if (count_cycles>=30000* Time.deltaTime)
            StartCoroutine(FadeTracks());

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

    IEnumerator FadeTracks()
    {
        var timer = 0f;
        var sprite = GetComponent<SpriteRenderer>();
        while (timer <= dissapearTime)
        {
            sprite.material.color = new Color(1, 1, 1, Mathf.Lerp(1f, 0f, timer));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}