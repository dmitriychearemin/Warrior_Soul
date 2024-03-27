using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    [SerializeField] float Max_lenght_Wave;
    float original_scale_X;
    float cur_scale_x;
    [SerializeField] float speed_wave = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        original_scale_X = gameObject.transform.localScale.x;
        cur_scale_x = original_scale_X;
    }

    // Update is called once per frame
    void Update()
    {
        if(cur_scale_x < original_scale_X - Max_lenght_Wave || cur_scale_x > original_scale_X + Max_lenght_Wave)
        {
            speed_wave *= -1;
        }
        cur_scale_x += speed_wave * Time.deltaTime;
        transform.localScale =  new Vector2(cur_scale_x, transform.localScale.y);

    }
}
