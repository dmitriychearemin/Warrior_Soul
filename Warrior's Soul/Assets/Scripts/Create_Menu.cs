using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Menu;
    public Transform canvas;
    public bool can_Create = true;
    public GameObject Portal;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    if (Input.GetKey(KeyCode.Mouse0))
       {
           can_Create = true;
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Loby_Portal" && can_Create)
        {
            Instantiate(Menu,canvas);
            can_Create = false;
        }
    }



}
