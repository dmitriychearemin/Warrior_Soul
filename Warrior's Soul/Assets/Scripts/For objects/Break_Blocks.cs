using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Block : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;
    public Color change_color;
    private Color curColor;
    private Color clear_color = new Color(1, 1, 1, 1);

    Material defaultMaterial;
    Material Dark_Material;
    private SpriteRenderer spriteBlock;
    private int Healph_Block = 60;
    bool isClear = true;
    bool Can_Break = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        curColor = clear_color;
        spriteBlock = GetComponent<SpriteRenderer>();
        Dark_Material = Resources.Load("Dark_Object", typeof(Material)) as Material;
        defaultMaterial = spriteBlock.material;
    }

    // Update is called once per frame

    private void Update()
    {
        if (isClear == false)
        {
            // curColor = Color.Lerp(curColor, clear_color, Time.deltaTime);
           // spriteBlock.material = Dark_Material;

        }
        //spriteRenderer.color = curColor;

        if (Healph_Block <= 0)
        {
            Destroy(gameObject);
        }
    }
        
    


    public void OnClick()
    {

        Healph_Block -= 20;
    }

    public void OnExit()
    {

        isClear = true;
       // spriteBlock.material = defaultMaterial;
    }

    public void OnEnter()
    {
        // if (Can_Break)
        //{

        // curColor = change_color;
        isClear = false;
        //}
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Can_Break = true;
        }
    }*/
}