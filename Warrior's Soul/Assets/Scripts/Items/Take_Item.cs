using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Item : MonoBehaviour
{
    public string _name_Item;
    [SerializeField] float _speed_item= 180;
    Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector2.Distance(Player.position, transform.position) <= 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, _speed_item * Time.deltaTime);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
