using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    public int damageAmount;
    public bool isTool;
    public string canBreak;
    public int toolLevel;
    private void Awake() 
    {
        damageCollider = GetComponent<Collider>();   
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
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
        if(collision.tag == "Player")
        {
            StatsManager stats = collision.GetComponent<StatsManager>();

            if (stats != null)
            {
                stats.TakeDamage(damageAmount);
            }
        }

        if(collision.tag == "Hittable")
        {

            ObjectStats stats = collision.GetComponent<ObjectStats>();
            ResourceObject resource = collision.GetComponent<ResourceObject>();
            Tree tree = collision.GetComponent<Tree>();

            if(resource != null)
            {
                Debug.Log("Resource");
                if(isTool && canBreak == resource.resourceType && toolLevel > resource.toolRequiredLevel)
                {
                    if (stats != null)
                    {
                        Debug.Log("Stats");
                        stats.TakeDamage(damageAmount);
                    }

                    if(tree != null)
                    {                   
                        Debug.Log("Tree");
                        if(tree.timeSinceInitialization > 1f)
                            tree.CheckHit(true);
                    }
                }
            }
    
        }
    

        if(collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damageAmount);
            }
        }
    }
}
