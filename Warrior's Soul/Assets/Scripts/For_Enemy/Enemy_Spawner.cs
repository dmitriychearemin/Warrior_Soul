using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    Enemies_Variants _Enemies_Variants;
    Spawning_inside_Room room;


    // Start is called before the first frame update
    void Start()
    {
        _Enemies_Variants = GameObject.Find("Enemies_Variants").GetComponent<Enemies_Variants>();
        room = GetComponentInParent<Spawning_inside_Room>();
        Create_Enemies();
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }

    void Create_Enemies()
    {
        int rand = Random.Range(0, _Enemies_Variants.Enemies.Length);
        var enemy = Instantiate(_Enemies_Variants.Enemies[rand], transform.position, transform.rotation);
        enemy.transform.SetParent(transform.parent);
        room.Add_Enemies_In_List(enemy);

    }

}
