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

    [SerializeField] private Type treeType;
    [SerializeField] private Transform fxTreeDestroyed;
    [SerializeField] private Transform fxTreeLogDestroyed;
    [SerializeField] private Transform treeLog;
    [SerializeField] private Transform treeLogHalf;
    [SerializeField] private Transform treeStump;
    [SerializeField] private string type;

    public float timeSinceInitialization;
    public float timer;
    ObjectStats objectStats;
    float logYPositionAboveFirstLogHalf = 0f;
    
    private void Awake() 
    {
        objectStats = GetComponent<ObjectStats>();
        int healthAmount;
        
        switch (treeType) {
            default:
                case Type.Tree:     healthAmount = 30; break;
                case Type.Log:      healthAmount = 50; break;
                case Type.LogHalf:  healthAmount = 50; break;
                case Type.Stump:    healthAmount = 50; break;
        }

        if(type == "Oak")
        {
            logYPositionAboveFirstLogHalf = 7f;
            switch (treeType) {
                default:
                case Type.Tree:     healthAmount = 90; break;
                case Type.Log:      healthAmount = 75; break;
                case Type.LogHalf:  healthAmount = 50; break;
                case Type.Stump:    healthAmount = 50; break;
            }
        }
        else if(type == "Maple")
        {
            logYPositionAboveFirstLogHalf = 10f;
            switch (treeType) {
                default:
                case Type.Tree:     healthAmount = 200; break;
                case Type.Log:      healthAmount = 150; break;
                case Type.LogHalf:  healthAmount = 100; break;
                case Type.Stump:    healthAmount = 100; break;
            }
        }
        objectStats.maxHealth = healthAmount;
    }
    private void Start() {
        timer = Time.timeSinceLevelLoad;
    }
    void Update () 
    {
        timeSinceInitialization = Time.timeSinceLevelLoad - timer;
    }
    private void Dead()
    {
        switch (treeType) 
        {
            default:
            case Type.Tree:
                Instantiate(treeLog, transform.position + transform.up, Quaternion.Euler(Random.Range(-1.5f, +1.5f), 0, Random.Range(-1.5f, +1.5f)));
                Instantiate(treeStump, transform.position, transform.rotation);
                break;
            case Type.Log:
                Instantiate(treeLogHalf, transform.position, transform.rotation);
                Instantiate(treeLogHalf, transform.position + transform.up * logYPositionAboveFirstLogHalf, transform.rotation);
                break;
            case Type.LogHalf:
                for(int i = 0; i < Random.Range(4,10); i++)
                {
                    var _drop = Instantiate(drop, transform.position + transform.up * logYPositionAboveFirstLogHalf * .75f, Random.rotation);
                    _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
                }    
                break;
            case Type.Stump:
                for(int i = 0; i < Random.Range(2,4); i++)
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
