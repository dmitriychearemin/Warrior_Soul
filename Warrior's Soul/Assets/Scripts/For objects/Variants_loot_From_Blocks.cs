using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Android.LowLevel.AndroidGameControllerState;

public class Variants_loot_From_Blocks: MonoBehaviour
{



    public GameObject Dirt;
    public GameObject Stone;
    public GameObject Iron;
    public GameObject Gold;
    public GameObject Gem;
    

    int rand;
    bool canSpawn = true;
    


    void Spawn_Loot()
    {
        if (canSpawn)
        {
            rand = Random.Range(0, 100);
            if(rand >= 0 && rand <= 60)
            {
                Instantiate(Dirt);
                // ����� �����
            }

            else if (rand >= 61 && rand <= 80)
            {
                // ����� �����
                Instantiate(Stone);
            }

            else if (rand >= 81 && rand <= 90)
            {
                // ����� ������
                Instantiate(Iron);
            }

            else if (rand >= 91 && rand <= 97)
            {
                // ����� ������
                Instantiate(Gold);
            }

            else if (rand >= 98 && rand <= 100)
            {
                // ����� ����
                Instantiate(Gem);
            }

        }
        

    }

    void Take_Loot()
    {

    }


}
