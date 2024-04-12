using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class MoveCamera: MonoBehaviour
{
    public GameObject player;
    public CharacterStats Character;
    ConditionCamera conditionCamera = ConditionCamera.Stay;
    [SerializeField] float MaxTimeShaking;
    float TimeShaking = 0;
    float distance_to_move = 3f;
    float default_speed = 4.5f;
    float cur_speed = 0;
    float max_speed = 6.5f;
    


    enum ConditionCamera
    {
        Stay,
        FolowPlayer,
        Shake
    }

    // Start is called before the first frame update
    void Start()
    {
        Character = player.GetComponent<CharacterStats>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        cur_speed = default_speed;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, player.transform.position) >= distance_to_move)
        {
            conditionCamera = ConditionCamera.FolowPlayer;
        }


        if (conditionCamera == ConditionCamera.FolowPlayer)
        {
            Folow_to_Player();
        }

        else if(conditionCamera == ConditionCamera.Shake)
        {
            ShakeCamera();
            TimeShaking += 1 * Time.deltaTime;

        }

        if(TimeShaking > MaxTimeShaking)
        {
            TimeShaking = 0;
            conditionCamera = ConditionCamera.FolowPlayer;
        }

    }
    

    void Folow_to_Player()
    {
      
        if(Character.character.MoveState == MoveState.Run )
        {
            cur_speed = max_speed;
        }
        else
        {
            cur_speed = default_speed;
        }
        print(cur_speed);
        Vector3 needpos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = Vector3.MoveTowards(transform.position, needpos, cur_speed* Time.deltaTime);
        if(Vector3.Distance(transform.position,player.transform.position)<= -transform.position.z + 0.2f)
        {
            cur_speed = default_speed;
            //conditionCamera = ConditionCamera.Stay;
        }
    }


    public void ShakeCamera()
    {
        Vector3 StartPos = new Vector3(player.transform.position.x, player.transform.position.y, -1);
        float X_axis_Offset = Random.Range(-500,500)/ 488;
        float Y_axis_Offset = Random.Range(-500, 500) / 488;
        transform.position = new Vector3(StartPos.x + X_axis_Offset, StartPos.y + Y_axis_Offset,StartPos.z);
        conditionCamera = ConditionCamera.Shake;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.tag == "Player")
        {
            if(conditionCamera == ConditionCamera.FolowPlayer)
            {
                conditionCamera = ConditionCamera.Stay;
                
            }
        }*/
    }

}
