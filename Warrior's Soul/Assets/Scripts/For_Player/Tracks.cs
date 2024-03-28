using System.Collections;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    public Spawn_Tracks Parent { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
            Parent.TrackDestroy(gameObject);
    }
}