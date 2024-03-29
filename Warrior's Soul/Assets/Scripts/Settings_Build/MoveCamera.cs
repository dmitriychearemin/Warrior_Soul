using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera: MonoBehaviour
{
    public GameObject player;
    ConditionCamera conditionCamera = ConditionCamera.FolowPlyer;
    [SerializeField] float MaxTimeShaking;
    float TimeShaking = 0;
    enum ConditionCamera
    {
        FolowPlyer,
        Shake
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (conditionCamera == ConditionCamera.FolowPlyer)
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
            conditionCamera = ConditionCamera.FolowPlyer;
        }

    }
    

    void Folow_to_Player()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);

    }


    public void ShakeCamera()
    {
        Vector3 StartPos = new Vector3(player.transform.position.x, player.transform.position.y, -1);
        float X_axis_Offset = Random.Range(-500,500)/ 488;
        float Y_axis_Offset = Random.Range(-500, 500) / 488;
        transform.position = new Vector3(StartPos.x + X_axis_Offset, StartPos.y + Y_axis_Offset,StartPos.z);
        conditionCamera = ConditionCamera.Shake;
       
    }

}
