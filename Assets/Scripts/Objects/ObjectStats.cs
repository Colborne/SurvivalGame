using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject FloatingText;

    void Start()
    {
        if(!File.Exists(Application.persistentDataPath  + "/" + PersistentData.name + ".objs"))
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        ShowDamage(damage.ToString());
        if(currentHealth <= 0)
        {
            currentHealth = 0;
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
