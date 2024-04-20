using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Item : MonoBehaviour
{
    public string _name_Item;
    [SerializeField] float _speed_item= 180;
    Transform Player;
    Rigidbody2D _rigidbody;
    public float Time_living;

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
        if(Time_living > 2)
        {
            if (Vector2.Distance(Player.position, transform.position) <= 1.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, _speed_item * Time.deltaTime);
            }

            else
            {
                _rigidbody.velocity = _rigidbody.velocity + new Vector2(-1 * _rigidbody.velocity.x / 1.2f, -1 * _rigidbody.velocity.y / 1.2f) * Time.deltaTime;
            }
        }
       

        Time_living += 3 * Time.deltaTime;
        
    }



    void ChangeName()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "").Trim();
    }

}
