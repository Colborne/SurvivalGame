using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    
    public GameObject FloatingText;
    public GameObject fx;
    public GameObject[] drop;

    EnemyAI enemyAI;
    MobAI mobAI;

    private void Awake() 
    {
        enemyAI = GetComponent<EnemyAI>();
        mobAI = GetComponent<MobAI>();
    }

    void Start()
    {
        if(!File.Exists(Application.persistentDataPath + "/" + PersistentData.name + ".objs"))
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if(enemyAI != null)
            enemyAI.TakeDamage();
        else if (mobAI != null)
            mobAI.TakeDamage();

        currentHealth = currentHealth - damage;
        ShowDamage(damage.ToString());
        if(currentHealth <= 0)
        {
            for(int i = 0; i < Random.Range(1,5); i++)
                Instantiate(drop[Random.Range(0, drop.Length)], transform.position + new Vector3(Random.Range(-.5f,.5f), Random.Range(0f,1f), Random.Range(-.5f,.5f)), Random.rotation);
                
            Destroy(gameObject);
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
