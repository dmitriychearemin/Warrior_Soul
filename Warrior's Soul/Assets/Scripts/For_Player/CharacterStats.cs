using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class CharacterStats : MonoBehaviour
{
    public delegate void OnStaminaUpdate(float currentStamina, float maxStamina);
    public delegate void OnHPUpdate(float currentHP, float maxHP);

    public static event OnStaminaUpdate StaminaUpdate;
    public static event OnStaminaUpdate HPUpdate;
   
    [Header("Stats")]
    public float MaxHP = 100;
    public float MaxStamina = 100;

    private float consumptionStamina = 35;
    private float replenishmentStamina = 5;

    private bool Take_Damage = true;
    private bool damageArea = false;
    private int count_Cycles = 0;

    public float Stamina { get; private set; }
    public float HP { get; private set; }

    private void Awake()
    {
        HP = MaxHP;
        Stamina = MaxStamina;
    }

    void Start()
    {
        StartCoroutine(StaminaUpdateLoop());
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
            damageArea = true;
            StartCoroutine(DamageArea());
        }

        //if (Take_Damage)
        //{
        //    if(collision.gameObject.CompareTag("Enemy"))
        //    {
        //        HP -= 35;
        //        HPUpdate?.Invoke(HP, MaxHP);
        //        Take_Damage = false;
        //    }
        //}
       // if (collision.CompareTag("Finish"))
            //LoadSceneWin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DieSpace"))
            damageArea = false;
    }

    public float GetHP()
    { return HP; }

    private IEnumerator DamageArea()
    {
        while (HP > 0 && damageArea)
        {
            HP -= 5 * Time.deltaTime;
            HPUpdate?.Invoke(HP, MaxHP);
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(DamageArea());
    }

    private async Task UpdateStaminaAsync()
    {
        await Task.Yield();
        switch (Player.GetMoveState())
        {
            case Player.MoveState.Idle:
                {
                    if (Stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
                    {
                        Stamina += replenishmentStamina * Time.deltaTime * 2;
                        //StaminaBar.fillAmount = Stamina / MaxStamina;
                    }
                    break;
                }
            case Player.MoveState.Walk:
                {
                    if (Stamina < 100 && !Input.GetKey(KeyCode.LeftShift))
                    {
                        Stamina += replenishmentStamina * Time.deltaTime;
                        //StaminaBar.fillAmount = Stamina / MaxStamina;
                    }
                    break;
                }
            case Player.MoveState.Attack:
            case Player.MoveState.Run:
                {
                    if (Stamina > 0)
                    {
                        Stamina -= consumptionStamina * Time.deltaTime;
                        //StaminaBar.fillAmount = Stamina / MaxStamina;
                    }
                    break;
                }
            default:
                break;
        }
        StaminaUpdate?.Invoke(Stamina, MaxStamina);
    }

    private IEnumerator StaminaUpdateLoop()
    {
        while (true)
        {
            yield return UpdateStaminaAsync();
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