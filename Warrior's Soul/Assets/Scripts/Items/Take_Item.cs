using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Item : MonoBehaviour
{
    public string _name_Item;
    [SerializeField] float _speed_item= 180;
    Transform Player;
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        ChangeName();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        Vector2 _direction = new Vector2(Random.Range(-2,2), Random.Range(-2, 2));
        _rigidbody.AddForce(new Vector2(Random.Range (-100f, 100f),(Random.Range(-100f, 100f))));
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector2.Distance(Player.position, transform.position) <= 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, _speed_item * Time.deltaTime);
        }

        else
        {
            _rigidbody.velocity = _rigidbody.velocity + new Vector2(-1 * _rigidbody.velocity.x/1.2f, -1 * _rigidbody.velocity.y / 1.2f) * Time.deltaTime;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    void ChangeName()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "").Trim();
    }

}
