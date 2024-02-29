using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CharacterStats : MonoBehaviour
{
    public delegate void OnStaminaUpdate(GameObject obj, float currentStamina);
    public delegate void OnHPUpdate(GameObject obj, float currentHP);

    public event OnStaminaUpdate StaminaUpdate;
    public event OnHPUpdate HPUpdate;

    private Character character;

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
        character = GetComponent<Character>();
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

    public void TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
            HP = 0;
        HPUpdate?.Invoke(gameObject, HP);
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
        //    if (collision.gameObject.CompareTag("Enemy"))
        //    {
        //        HP -= 35;
        //        HPUpdate?.Invoke(gameObject, HP);
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

    private IEnumerator DamageArea()
    {
        while (HP > 0 && damageArea)
        {
            HP -= 5 * Time.deltaTime;
            HPUpdate?.Invoke(gameObject, HP);
            yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine(DamageArea());
    }

    private async Task UpdateStaminaAsync()
    {
        await Task.Yield();
        switch (character.MoveState)
        {
            case MoveState.Idle:
                {
                    if (Stamina < 100)
                        Stamina += replenishmentStamina * Time.deltaTime * 2;
                    else if (Stamina > 100)
                        Stamina = 100;
                    break;
                }
            case MoveState.Walk:
                {
                    if (Stamina < 100 && !InputHandler.RunTriggered)
                        Stamina += replenishmentStamina * Time.deltaTime;
                    else if (Stamina > 100)
                        Stamina = 100;
                    break;
                }
            case MoveState.Attack:
            case MoveState.Run:
                {
                    if (Stamina > 0)
                        Stamina -= consumptionStamina * Time.deltaTime;
                    else
                        Stamina = 0;
                    break;
                }
            default:
                break;
        }
        StaminaUpdate?.Invoke(gameObject, Stamina);
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