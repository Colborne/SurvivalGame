using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int healthLevel = 1;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 1;
    public float maxStamina;
    public float currentStamina;

    public float staminaRegenAmount = 30f;
    private float staminaRegenTimer = 0f;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameObject FloatingText;
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;

    private void Awake() 
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentStamina);
    }

    private int SetMaxHealthFromLevel()
    {
        maxHealth = healthLevel * 100;
        return maxHealth;
    }

    private float SetMaxStaminaFromLevel()
    {
        maxStamina = staminaLevel * 100;
        return maxStamina;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetCurrentHealth(currentHealth);
        animatorManager.PlayTargetAnimation("Damage", true);
        ShowDamage(damage.ToString());

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animatorManager.PlayTargetAnimation("Dying", true);
        }
    }

    public void UseStamina(float cost)
    {
        currentStamina = currentStamina - cost;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if(playerManager.isInteracting || playerLocomotion.isSprinting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;
            if(currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                currentStamina += staminaRegenAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
    }

    void ShowDamage(string text)
    {
        if(FloatingText)
        {
            GameObject prefab = Instantiate(FloatingText, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
            Destroy(prefab, .8f);
        }
    }
}
