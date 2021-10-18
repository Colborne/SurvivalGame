using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public float baseDefense = 0;
    public float inventoryWeight = 0;

    public float mageBonus = 0;
    public float rangeBonus = 0;
    public float strengthBonus = 0;

    float fireDefense;
    float iceDefense;

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
    private void Update() 
    { 
        UpdateStats();
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

    public void Heal(int amount)
    {
        currentHealth = currentHealth + amount;
        if (currentHealth > 100)
            currentHealth = 100;
        healthBar.SetCurrentHealth(currentHealth);
        ShowDamage("+" + amount.ToString());
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

    void UpdateWeight()
    {        
        float newWeight = 0;
        for (int i = 0; i < GameManager.Instance.inventorySlots.Length; i++)
        {
            if ( GameManager.Instance.inventorySlots[i].isFull)
            {
                GameManager.Instance.inventorySlots[i].currentItem.WeightCalculation();
                newWeight +=  GameManager.Instance.inventorySlots[i].currentItem.totalWeight;
            }
        }

        if(inventoryWeight != newWeight)
            inventoryWeight = newWeight;
    }

    void UpdateDefense()
    {
        float newDefense = 0;
        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Instance.inventorySlots[i].isFull)
            {
                if(GameManager.Instance.inventorySlots[i].currentItem.item is EquipmentItem){
                    EquipmentItem eq = GameManager.Instance.inventorySlots[i].currentItem.item as EquipmentItem;
                    newDefense += eq.baseDefense;
                }
            }
        }

        if(baseDefense != newDefense)
            baseDefense = newDefense;
    }

    void UpdateStats()
    {
        UpdateWeight();
        UpdateDefense();
        TextMeshProUGUI[] stats = GameObject.Find("Player UI").GetComponentsInChildren<TextMeshProUGUI>(true);
        stats[0].text = inventoryWeight.ToString();
        stats[1].text = baseDefense.ToString();
    }
}
