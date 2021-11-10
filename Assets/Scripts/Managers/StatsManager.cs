using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class StatsManager : MonoBehaviour
{
    public float drownTimer = 0f;
    public bool isTakingDamage = false;

    [Header("Health")]
    public int healthLevel = 1;
    public int maxHealth;
    public int currentHealth;

    [Header("Stamina")]
    public int staminaLevel = 1;
    public float maxStamina;
    public float currentStamina;
    public float staminaRegenAmount = 30f;
    private float staminaRegenTimer = 0f;

    [Header("Stats")]
    public float baseDefense = 0;
    public float inventoryWeight = 0;

    [Header("Attack Bonuses")]
    public float mageBonus = 1f;
    public float rangeBonus = 1f;
    public float strengthBonus = 1f;

    [Header("Defense Bonuses")]
    float fireDefense = 1f;
    float iceDefense = 1f;

    [Header("Movement Bonuses")]
    public float jumpBonus = 1f;
    public float baseSpeedBonus = 1f;
    public float swimSpeedBonus = 1f;

    [Header("Components")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameObject FloatingText;
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    PlayerLocomotion playerLocomotion;
    Player player;

    private void Awake() 
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        player = GetComponent<Player>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromLevel();
        maxStamina = SetMaxStaminaFromLevel();

        if(!File.Exists(Application.persistentDataPath  + "/" + PersistentData.name + ".plyr"))
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;

            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
    
    private void Update() 
    { 
        UpdateStats();
    }
    private void FixedUpdate() 
    {
        isTakingDamage = animatorManager.animator.GetBool("isTakingDamage");
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
        animatorManager.animator.SetBool("isTakingDamage", true);
        currentHealth = currentHealth - damage;
        healthBar.SetCurrentHealth(currentHealth);
        if(currentHealth > 0)
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
        if(playerManager.isInteracting || playerLocomotion.isSprinting || playerLocomotion.isSwimming)
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

    void UpdateBonuses()
    {
        float newJump = 1f;
        float newSpeed = 1f;
        float newSwim = 1f;
        float newStrength = 1f;
        float newMage = 1f;
        float newRange = 1f;
        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Instance.inventorySlots[i].isFull)
            {
                if(GameManager.Instance.inventorySlots[i].currentItem.item is EquipmentItem){
                    EquipmentItem eq = GameManager.Instance.inventorySlots[i].currentItem.item as EquipmentItem;
                    newJump *= eq.jumpBonus;
                    newSpeed *= eq.baseSpeedBonus;
                    newSwim *= eq.swimSpeedBonus;
                    newStrength *= eq.strengthBonus;
                    newMage *= eq.mageBonus;
                    newRange *= eq.rangeBonus;
                }
            }
        }

        if(jumpBonus != newJump)
            jumpBonus = newJump;
        
        if(baseSpeedBonus!= newSpeed)
            baseSpeedBonus = newSpeed;
        
        if(swimSpeedBonus != newSwim)
            swimSpeedBonus = newSwim;

        if(strengthBonus != newStrength)
            strengthBonus = newStrength;
        
        if(mageBonus!= newMage)
            mageBonus = newMage;
        
        if(rangeBonus != newRange)
            rangeBonus = newRange;
    }

    void UpdateStats()
    {
        UpdateWeight();
        UpdateDefense();
        UpdateBonuses();
        TextMeshProUGUI[] stats = GameObject.Find("Player UI").GetComponentsInChildren<TextMeshProUGUI>(true);
        stats[0].text = inventoryWeight.ToString();
        stats[1].text = baseDefense.ToString();

        if(currentStamina < 0f)
            currentStamina = 0f;
    }

    public void Drowning()
    {
        drownTimer += Time.deltaTime;
        if(drownTimer >= .5f)
        {
            drownTimer = 0f;
            TakeDamage(1);
        }
    }
}
