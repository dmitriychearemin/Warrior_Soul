using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HitPoint : MonoBehaviour
{
    [Header("UI")]
    public Image HealphBar;
    public Image StaminaBar;
    public Image MagicBar;

    [Header("Player Stats")]
    public float MaxHP = 100;
    private static float HP;
    public float MaxStamina = 100;
    private static float Stamina;
    private static float consumptionStamina = 35;
    private static float replenishmentStamina = 5;
    //public Player player;

    private bool Take_Damage = true;
    private bool damageSpace = false;
    private int count_Cycles = 0;

    void Start()
    {
        HP = MaxHP;
        Stamina = MaxStamina;
        StartCoroutine(ActionsStamina());
    }

    // Update is called once per frame
    void Update()
    {
        //if (HP <= 0)
           // LoadSceneLose();

        if (Take_Damage == false)
        {
            count_Cycles++;
            if (count_Cycles >= 80)
            {
                Take_Damage = true;
                count_Cycles = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DieSpace"))
        {
            damageSpace = true;
            StartCoroutine(DamageSpace());
        }

        if (Take_Damage)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                HP -= 35;
                HealphBar.fillAmount = HP / MaxHP;
                Take_Damage = false;
            }
        }
       // if (collision.CompareTag("Finish"))
            //LoadSceneWin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DieSpace"))
            damageSpace = false;
    }

    public static float GetHP()
    {
        return HP;
    }

    public static float GetStamina()
    {
        return Stamina;
    }

    private IEnumerator DamageSpace()
    {
        while (HP > 0 && damageSpace)
        {
            HP -= 5 * Time.deltaTime;
            HealphBar.fillAmount = HP / MaxHP;
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(DamageSpace());
    }

    private IEnumerator ActionsStamina()
    {
        while(true) 
        {
            switch (Player.GetMoveState())
            {
                case Player.MoveState.Idle:
                    {
                        if (Stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
                        {
                            Stamina += replenishmentStamina * 4 * Time.deltaTime;
                            StaminaBar.fillAmount = Stamina / MaxStamina;
                        }
                        break;
                    }
                case Player.MoveState.Walk:
                    {
                        if (Stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
                        {
                            Stamina += replenishmentStamina * Time.deltaTime;
                            StaminaBar.fillAmount = Stamina / MaxStamina;
                        }
                        break;
                    }

                case Player.MoveState.Attack:
                    {
                        if (Stamina > 0)
                        {
                            Stamina -= consumptionStamina * Time.deltaTime;
                            StaminaBar.fillAmount = Stamina / MaxStamina;
                            yield return new WaitForSeconds(0.01f);
                        }
                        break;
                    }

                case Player.MoveState.Run:
                    {
                        if (Stamina > 0)
                        {
                            Stamina -= consumptionStamina * Time.deltaTime;
                            StaminaBar.fillAmount = Stamina / MaxStamina;
                            yield return new WaitForSeconds(0.01f);
                        }
                        break;
                    }
                default:
                    break;
            }
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