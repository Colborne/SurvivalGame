using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public enum SpecialDamageType {
        magic,
        ice,
        fire
    }
    public int damage;
    public bool isTree;
    [SerializeField] private SpecialDamageType spDamage;
    private void OnTriggerEnter(Collider other) 
    {
        StatsManager stats = other.GetComponent<StatsManager>();

        if(stats != null && damage > 0)
        {
            if(stats.isTakingDamage == false)
            {
            //DAMAGE EQUATION
            float reduction = 1 - ((stats.baseDefense * .025f) / ( 1 + (stats.baseDefense * .025f)));
            if(isTree)
            {
                //stats.TakeDamage((int)damage);
            }
            else
                stats.TakeDamage((int)Mathf.Floor((damage + Random.Range(-damage/10f, damage/10f)) * reduction));
            }
        }
    }
}