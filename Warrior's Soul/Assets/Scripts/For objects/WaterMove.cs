using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    [SerializeField] float Max_lenght_Wave;
    float original_scale_Y;
    float cur_scale_y;
    [SerializeField] float speed_wave = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        original_scale_Y = gameObject.transform.localScale.y;
        cur_scale_y = original_scale_Y;
    }

    // Update is called once per frame
    void Update()
    {
        if(cur_scale_y < original_scale_Y - Max_lenght_Wave || cur_scale_y > original_scale_Y + Max_lenght_Wave)
        {
            speed_wave *= -1;
        }
        cur_scale_y += speed_wave * Time.deltaTime;
        transform.localScale =  new Vector2(transform.localScale.x, cur_scale_y);

    }
}
