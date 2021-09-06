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

    public float timeSinceInitialization;
    public float timer;
    ObjectStats objectStats;
    private void Awake() {
        objectStats = GetComponent<ObjectStats>();
        int healthAmount;

        switch (treeType) {
            default:
            case Type.Tree:     healthAmount = 30; break;
            case Type.Log:      healthAmount = 50; break;
            case Type.LogHalf:  healthAmount = 50; break;
            case Type.Stump:    healthAmount = 50; break;
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
        switch (treeType) {
            default:
            case Type.Tree:
                Instantiate(fxTreeDestroyed, transform.position - new Vector3(0,12,0), transform.rotation);
                Vector3 treeLogOffset = transform.up - new Vector3(0,12,0);
                Instantiate(treeLog, transform.position + treeLogOffset, Quaternion.Euler(Random.Range(-1.5f, +1.5f), 0, Random.Range(-1.5f, +1.5f)));

                Instantiate(treeStump, transform.position - new Vector3(0,11,0), transform.rotation);
                break;
            case Type.Log:

                Instantiate(fxTreeLogDestroyed, transform.position, transform.rotation);

                float logYPositionAboveStump = .5f;
                treeLogOffset = transform.up * logYPositionAboveStump;
                Instantiate(treeLogHalf, transform.position, transform.rotation);

                float logYPositionAboveFirstLogHalf = 9.35f;
                treeLogOffset = transform.up * logYPositionAboveFirstLogHalf;
                Instantiate(treeLogHalf, transform.position + treeLogOffset, transform.rotation);
                break;
            case Type.LogHalf:
                Instantiate(fxTreeLogDestroyed, transform.position, transform.rotation);

                for(int i = 0; i < Random.Range(2f,6f); i++)
                {
                    var _drop = Instantiate(drop, transform.position + new Vector3(Random.Range(-.5f,.5f),Random.Range(-.5f,.5f),Random.Range(-.5f,.5f)), Random.rotation);
                    _drop.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 20f);
                }
                
                break;
            case Type.Stump:
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
