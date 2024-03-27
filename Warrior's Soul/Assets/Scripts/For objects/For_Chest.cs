using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class For_Chest : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite chest_close;
    public Sprite chest_open;
    public Sprite chest_open_with_loot;
    UnityEngine.Rendering.Universal.Light2D _Light;
    int max_radius = 4;
    SpriteRenderer _spriteRenderer;
    bool openchest = false;
    bool canspawn = true;
    Spawn_Loot _Spawn_Loot;
    float count_cycles=0;

    void Start()
    {
        _Spawn_Loot = gameObject.GetComponent<Spawn_Loot>();
        _Light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = chest_close;
    }

    // Update is called once per frame
    void Update()
    {
        if (openchest && canspawn)
        {
            Changing_Light();
            count_cycles += 1 * Time.deltaTime;
            if (count_cycles > 2)
            {
                
                _Spawn_Loot.Spawn_Loot_Item();
                _spriteRenderer.sprite = chest_open;
                canspawn = false;
                _Light.pointLightOuterAngle =0;
                _Light.pointLightOuterRadius = 0;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(openchest == false)
            {
                _spriteRenderer.sprite = chest_open_with_loot;
                openchest = true;
               
            }
        }
    }

    void Changing_Light()
    {
        _Light.pointLightOuterAngle += Random.value * 100 * Time.deltaTime;
        _Light.pointLightOuterRadius += Random.value * 10 * Time.deltaTime;

    }


}
