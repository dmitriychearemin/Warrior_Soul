using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private static CharacterStats player;

    [Header("UI")]
    public Image HealphBar;
    public Image StaminaBar;
    public Image MagicBar;

    [Header("Quality")]
    [SerializeField]private int maxFPS = 60;

    // Start is called before the first frame update
    void Start()
    {
        CharacterStats.StaminaUpdate += UpdatePlayerStaminaBar;
        CharacterStats.HPUpdate += UpdateHealthBar;
        Application.targetFrameRate = maxFPS;
    }

    private void OnDestroy()
    {
        CharacterStats.StaminaUpdate -= UpdatePlayerStaminaBar;
        CharacterStats.HPUpdate -= UpdateHealthBar;
    }

    public void SetFPS(int fps)
    {
        maxFPS = fps;
        Application.targetFrameRate = maxFPS;
    }

    private void UpdatePlayerStaminaBar(float currentStamina, float maxStamina)
    {
        StaminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        HealphBar.fillAmount = currentHP / maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
