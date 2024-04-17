using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class For_Input_Fields : MonoBehaviour
{
    private int number = 0;
    int count_items_in_cell;
    private string text;
    TMP_InputField inputField;
    Inventory_Cell _inventory_Cell;
    bool It_work = false;


    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        
    }

    private void Update()
    {
        if(It_work == true)
        {
            Destroy(gameObject);
        }
        


    }

    public void Set_Cell_For_Separate(GameObject cell)
    {
        _inventory_Cell = cell.GetComponent<Inventory_Cell>();
        int.TryParse(_inventory_Cell._count_items.text, out count_items_in_cell);
        
    }

    public void SetNumber()
    {
        text = inputField.text;
        int.TryParse(text, out number);
        if(number > 0 && number < count_items_in_cell)
        {
            _inventory_Cell.Get_Count_Separate_Item(number);
        }

        else
        {

        }

        It_work = true;

    }

    


}
