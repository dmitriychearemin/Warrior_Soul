using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Item : MonoBehaviour
{
    public string name_Item;
    Inventory_Cell cell;
    Transform cell_parent;

    // Start is called before the first frame update
    void Start()
    {
        cell_parent = cell.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Add_New_Cell() // добавление новых €чеек если при подборе элемента такого нет в инвентаре
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bool need_New_Cell = true;
            for (int i = 0; i < cell_parent.childCount; i++)
            {

                cell = cell_parent.GetChild(i).GetComponent<Inventory_Cell>();
                if (cell._namefield.text == this.name)   //нужно сделать сравнение текста и строки
                {
                    cell._count_items_in_cell++;
                    need_New_Cell = false;
                }
            }

            if (need_New_Cell)
            {
                Add_New_Cell();
            }
        
            Destroy(gameObject);

        }
    }

}
