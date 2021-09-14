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
            SoundManager sound = FindObjectOfType<InputManager>().GetComponent<SoundManager>();
            Tree tree = collision.GetComponent<Tree>();
            CameraShake shake = FindObjectOfType<CameraShake>();

            if(resource != null)
            {
                if (stats != null)
                {
                    if(isTool && canBreak == resource.resourceType && toolLevel >= resource.toolRequiredLevel)
                    {

                        if(resource.resourceType == "Ore")
                            sound.PlaySound("Sounds/metalLatch");
                        else if(resource.resourceType == "Tree")
                            sound.PlaySound("Sounds/chop"); 
                        
                        shake.Shake();
                        stats.TakeDamage(damageAmount);
                        for(int i = 0; i < Random.Range(1,3); i++)
                            Instantiate(resource.fx, transform.position + new Vector3(0,1,0), Random.rotation);
                        
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
