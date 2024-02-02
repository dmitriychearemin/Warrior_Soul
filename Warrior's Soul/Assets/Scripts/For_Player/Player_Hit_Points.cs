using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HitPoint : MonoBehaviour
{
    public Image HealphBar;
    public Image HealphBarBackground;
    public float MaxHP = 100;
    public float HP;

    public GameObject Player;
    private bool Take_Damage = true;
    private int count_Cycles = 0;

    void Start()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
       // if (HP <= 0)
=======
        //if (HP <= 0)
>>>>>>> Stashed changes
            //LoadSceneLose();
        Debug.Log($"{count_Cycles} and {Take_Damage}");
        if (Take_Damage == false)
        {
            count_Cycles++;
<<<<<<< Updated upstream
            if (count_Cycles >= 80)
=======
            if (count_Cycles >= 90)
>>>>>>> Stashed changes
            {
                Take_Damage = true;
                count_Cycles = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("DieSpace"))
            StartCoroutine(DamageSpace());*/
<<<<<<< Updated upstream
        //Debug.Log("Text" + count_Cycles);
=======
>>>>>>> Stashed changes
        if (Take_Damage)
        {
            //if (collision.gameObject.CompareTag("Enemy"))
            if(collision.transform.tag == "Enemy")
            {
                HP -= 35;
                HealphBar.fillAmount = HP / MaxHP;
                Take_Damage = false;
            }
        }
       // if (collision.CompareTag("Finish"))
            //LoadSceneWin();
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }*/

    private IEnumerator DamageSpace()
    {
        while (HP > 0)
        {
            HP -= 5 * Time.deltaTime;
            HealphBar.fillAmount = HP / MaxHP;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void LoadSceneLose()
    {
        SceneManager.LoadScene(1);
    }

    void LoadSceneWin()
    {
        SceneManager.LoadScene(2);
    }


}