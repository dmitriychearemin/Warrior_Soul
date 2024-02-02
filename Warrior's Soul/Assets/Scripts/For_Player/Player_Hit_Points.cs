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
    private float HP;

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
        if (HP <= 0)
            LoadSceneLose();

        if (Take_Damage == false)
        {
            count_Cycles++;
            if (count_Cycles >= 150)
            {
                Take_Damage = true;
                count_Cycles = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DieSpace"))
            StartCoroutine(DamageSpace());

        if (Take_Damage)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HP -= 35;
                HealphBar.fillAmount = HP / MaxHP;
                Take_Damage = false;
            }
        }
        if (collision.CompareTag("Finish"))
            LoadSceneWin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

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