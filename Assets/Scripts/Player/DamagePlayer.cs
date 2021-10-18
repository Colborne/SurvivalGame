using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public bool isTree;
    private void OnTriggerEnter(Collider other) 
    {
        StatsManager stats = other.GetComponent<StatsManager>();

        if(stats != null)
        {
            if(isTree)
            {
                Rigidbody rb = GetComponentInParent<Rigidbody>();
                if(rb.velocity.magnitude > 7f)
                    stats.TakeDamage(damage * (int)rb.velocity.magnitude);
            }
            else
                stats.TakeDamage(damage);
        }
    }
}