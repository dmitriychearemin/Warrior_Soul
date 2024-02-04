using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Conrol_Button : MonoBehaviour
{
    // Start is called before the first frame update

    Create_Menu menu;
    Player player;

    void Start()
    {
       menu =  GetComponent<Create_Menu>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load_Scene_Raid()
    {
        SceneManager.LoadScene("HP");
    }

    public void Load_Scene_Level()
    {
        SceneManager.LoadScene("Level_Scene");
    }

    public void Exit()
    {
        
        Destroy(gameObject);
        //menu.can_Create = true;
    }


}
