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
                // спавн земли
            }

            else if (rand >= 61 && rand <= 80)
            {
                // спавн камня
                Instantiate(Stone);
            }

            else if (rand >= 81 && rand <= 90)
            {
                // спавн железа
                Instantiate(Iron);
            }

            else if (rand >= 91 && rand <= 97)
            {
                // спавн золота
                Instantiate(Gold);
            }

            else if (rand >= 98 && rand <= 100)
            {
                // спавн гема
                Instantiate(Gem);
            }

        }
        

    }

    void Take_Loot()
    {

    }


}
