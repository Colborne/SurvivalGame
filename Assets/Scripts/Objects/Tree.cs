using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tree : ResourceObject
{
    public enum Type {
        Tree,
        Log,
        LogHalf,
        Stump
    }

    public enum Wood {
        Oak,
        Maple,
        Mahogany
    }

    [SerializeField] private Type treeType;
    [SerializeField] private Transform fxTreeDestroyed;
    [SerializeField] private Transform fxTreeLogDestroyed;
    [SerializeField] private Transform treeLog;
    [SerializeField] private Transform treeLogHalf;
    [SerializeField] private Transform treeStump;
    [SerializeField] private Wood type;

    public float timeSinceInitialization;
    public float timer;
    ObjectStats objectStats;
    float logYPositionAboveFirstLogHalf = 0f;
    int min, max;
    
    private void Awake() 
    {
        objectStats = GetComponent<ObjectStats>();
        int healthAmount;
        min = 2; max = 4;
        switch (treeType) {
            default:
                case Type.Tree:     healthAmount = 30; break;
                case Type.Log:      healthAmount = 50; break;
                case Type.LogHalf:  healthAmount = 50; break;
                case Type.Stump:    healthAmount = 50; break;
        }

        if(type == Wood.Oak)
        {
            min = 4; max = 10;
            logYPositionAboveFirstLogHalf = 7f;
            switch (treeType) {
                default:
                case Type.Tree:     healthAmount = 90; break;
                case Type.Log:      healthAmount = 75; break;
                case Type.LogHalf:  healthAmount = 50; break;
                case Type.Stump:    healthAmount = 50; break;
            }
        }
        else if(type == Wood.Maple)
        {
            min = 6; max = 12;
            logYPositionAboveFirstLogHalf = 10f;
            switch (treeType) {
                default:
                case Type.Tree:     healthAmount = 200; break;
                case Type.Log:      healthAmount = 150; break;
                case Type.LogHalf:  healthAmount = 100; break;
                case Type.Stump:    healthAmount = 100; break;
            }
        }
        else if(type == Wood.Mahogany)
        {
            min = 10; max = 20;
            logYPositionAboveFirstLogHalf = 15f;
            switch (treeType) {
                default:
                case Type.Tree:     healthAmount = 300; break;
                case Type.Log:      healthAmount = 180; break;
                case Type.LogHalf:  healthAmount = 120; break;
                case Type.Stump:    healthAmount = 120; break;
            }
        }
        objectStats.maxHealth = healthAmount;
        timer = Time.timeSinceLevelLoad;
    }

    private void Dead()
    {
        switch (treeType) 
        {
            default:
            case Type.Tree:
                Instantiate(treeLog, transform.position + transform.up, Quaternion.Euler(Random.Range(-1.5f, +1.5f), 0, Random.Range(-1.5f, +1.5f)));
                Instantiate(treeStump, transform.position, transform.rotation);
                Instantiate(fxTreeDestroyed, transform.position, transform.rotation);
                break;
            case Type.Log:
                Instantiate(treeLogHalf, transform.position, transform.rotation);
                Instantiate(treeLogHalf, transform.position + transform.up * logYPositionAboveFirstLogHalf, transform.rotation);
                break;
            case Type.LogHalf:
                for(int i = 0; i < Random.Range(min,max); i++)
                {
                    var _drop = Instantiate(drop, transform.position + transform.up * logYPositionAboveFirstLogHalf * .75f, Random.rotation);
                    _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
                }    
                break;
            case Type.Stump:
                for(int i = 0; i < Random.Range(Mathf.Floor(min/2),Mathf.Floor(max/2)); i++)
                {
                    var _drop = Instantiate(drop, transform.position + transform.up * 3, Random.rotation);
                    _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
                } 
                break;
        }
        Destroy(gameObject);
    }

    public void CheckHit(bool axe) {
        if(!axe)
            GetComponent<AudioSource>().Play();  

        if(objectStats.currentHealth <= 0)
        {
            Dead();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<ObjectStats>(out ObjectStats treeDamageable)) {
            if (collision.relativeVelocity.magnitude > 1f) {
                int damageAmount = Random.Range(5, 20);
                treeDamageable.TakeDamage(damageAmount);
                CheckHit(false);
            }
        }
    }
}
