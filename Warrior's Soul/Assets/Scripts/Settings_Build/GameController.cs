using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    private CharacterStats playerStats;

    [Header("UI")]
    public Image HealphBar;
    public Image StaminaBar;
    public Image MagicBar;

    [Header("Quality")]
    [SerializeField]private int maxFPS = 60;

    private void Awake()
    {
        playerStats = player.GetComponent<CharacterStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats.StaminaUpdate += UpdateStaminaBar;
        playerStats.HPUpdate += UpdateHealthBar;
        Application.targetFrameRate = maxFPS;
    }

    private void OnDestroy()
    {
        playerStats.StaminaUpdate -= UpdateStaminaBar;
        playerStats.HPUpdate -= UpdateHealthBar;
    }

    public void SetFPS(int fps)
    {
        maxFPS = fps;
        Application.targetFrameRate = maxFPS;
    }

    private void UpdateStaminaBar(GameObject obj, float currentStamina)
    {
        if (ReferenceEquals(player, obj)) 
            StaminaBar.fillAmount = currentStamina / playerStats.MaxStamina;
    }

    private void UpdateHealthBar(GameObject obj, float currentHP)
    {
        if (ReferenceEquals(player, obj))
            HealphBar.fillAmount = currentHP / playerStats.MaxHP;
    }
}
