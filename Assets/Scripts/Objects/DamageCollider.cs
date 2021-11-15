using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int damageAmount;
    public bool isTool;
    public string canBreak;
    [Range(1,6)]
    public int weaponLevel;
    public float range;
    
    private void Awake() 
    {
        damageCollider = GetComponent<Collider>();   
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
        if(GetComponentInParent<weaponItemLoader>() != null)
            range = GetComponentInParent<weaponItemLoader>().item.attackDamage;
        else
            range = 1f;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision) 
    {
        if(collision.tag == "Hittable")
        {
            ObjectStats stats = collision.GetComponent<ObjectStats>();
            ResourceObject resource = collision.GetComponent<ResourceObject>();
            SoundManager sound = FindObjectOfType<InputManager>().GetComponent<SoundManager>();
            Tree tree = collision.GetComponent<Tree>();
            WeaponItem.DamageType damageType = FindObjectOfType<EquipmentManager>().rightWeapon.damageType;
            StatsManager statsManager = FindObjectOfType<StatsManager>();
            int bonusDamage = 0;

            if(damageType == WeaponItem.DamageType.magic)
                bonusDamage = (int)Mathf.Floor(statsManager.mageBonus);
            else if(damageType == WeaponItem.DamageType.range)
                bonusDamage = (int)Mathf.Floor(statsManager.rangeBonus);
            else
                bonusDamage = (int)Mathf.Floor(statsManager.strengthBonus);

            if(resource != null)
            {
                if (stats != null)
                {
                    if(isTool && canBreak == resource.resourceType && weaponLevel >= resource.toolRequiredLevel)
                    {
                        if(resource.resourceType == "Ore")
                            sound.PlaySound("Sounds/metalLatch");
                        else if(resource.resourceType == "Tree")
                            sound.PlaySound("Sounds/chop"); 
                        
                        int dmg = damageAmount + bonusDamage + (int)Random.Range(Mathf.Floor(-range * weaponLevel), Mathf.Floor(range * weaponLevel));
                        stats.TakeDamage(dmg);
                        
                        if(resource.GetComponent<SaveableObject>().AssetPath == "Rune")
                        {
                            for(int i = 0; i < dmg/10; i++)
                                Instantiate(resource.fx, transform.position + new Vector3(0,1,0), Random.rotation);
                        }
                        else
                        {
                            for(int i = 0; i < Random.Range(1,3); i++)
                                Instantiate(resource.fx, transform.position + new Vector3(0,1,0), Random.rotation);
                        }
                        
                        if(!tree)
                            resource.checkHit();
                    }
                    else
                    {
                        sound.PlaySound("Sounds/cloth4");
                        stats.TakeDamage(0); 
                    }

                    if(tree != null)
                    {   
                        tree.timeSinceInitialization = Time.timeSinceLevelLoad - tree.timer;                
                        if(tree.timeSinceInitialization > 1f)
                            tree.CheckHit(true);
                    }
                }
            }
        } 

        if(collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            WeaponItem.DamageType damageType = FindObjectOfType<EquipmentManager>().rightWeapon.damageType;
            StatsManager statsManager = FindObjectOfType<StatsManager>();
            int bonusDamage = 0;

            if(damageType == WeaponItem.DamageType.magic)
                bonusDamage = (int)Mathf.Floor(statsManager.mageBonus);
            else if(damageType == WeaponItem.DamageType.range)
                bonusDamage = (int)Mathf.Floor(statsManager.rangeBonus);
            else
                bonusDamage = (int)Mathf.Floor(statsManager.strengthBonus);

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damageAmount + bonusDamage + (int)Random.Range(Mathf.Floor(-range * weaponLevel), Mathf.Floor(range * weaponLevel))); //fix this
                for(int i = 0; i < Random.Range(2,4); i++){
                    Instantiate(enemyStats.fx, transform.position + new Vector3(0,1,0), Random.rotation);
                }
            }
        }
    }
}
