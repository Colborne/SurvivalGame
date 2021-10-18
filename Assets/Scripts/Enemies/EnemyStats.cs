using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    
    public GameObject FloatingText;
    public GameObject fx;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        ShowDamage(damage.ToString());
        if(currentHealth <= 0)
        {
            for(int i = 0; i < 20; i++)
                Instantiate(fx, transform.position + new Vector3(Random.Range(-.5f,.5f), Random.Range(-.5f,.5f), Random.Range(-.5f,.5f)), Random.rotation);
                
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
