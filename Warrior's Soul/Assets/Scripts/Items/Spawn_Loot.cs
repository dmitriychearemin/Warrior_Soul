using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputSystem.Android.LowLevel.AndroidGameControllerState;

public class Spawn_Loot : MonoBehaviour
{

    private Loot_Variants _loot_variant;
    private int _current_count_loot;
    private int _level_player = 0;          //получение через геткомпонент из скрипта
    [SerializeField] int _max_count_spawn_item = 3;
    


    private void Start()
    {
        _loot_variant = GameObject.Find("Loot_"+ gameObject.name).GetComponent<Loot_Variants>();
        
    }
    

    void Spawn_Loot_Item()
    {
        _max_count_spawn_item += _level_player;
        _current_count_loot = Random.Range(0,_max_count_spawn_item);

        for(int i=0; i < _current_count_loot; i++)
        {
            int rand = Random.Range(0, _loot_variant._Loot_Items.Length);
            Instantiate(_loot_variant._Loot_Items[rand], transform.position, transform.rotation);
            
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Spawn_Loot_Item();
            Destroy(gameObject);
        }
    }

}
