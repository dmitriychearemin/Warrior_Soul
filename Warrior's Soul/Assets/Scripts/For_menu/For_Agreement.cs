using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class For_Agreement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button button_YES;
    [SerializeField] Button button_NO;
    Inventory inventory;
    bool agree= false;
   
    public void Click_YES()
    {
        agree = true;
        inventory.Check_On_True_Destroying(agree);
        Destroy(gameObject);
    }

    public void Click_NO()
    {
        agree= false;
        inventory.Check_On_True_Destroying(agree);
        Destroy(gameObject);
    }

    public void Get_Component_Inventory(Inventory inv)
    {
        inventory = inv;
    }

}
