using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other) 
    {
        StatsManager stats = other.GetComponent<StatsManager>();

        if(stats != null)
        {
            Rigidbody rb = GetComponentInParent<Rigidbody>();
            Debug.Log(rb.velocity.magnitude);
            if(rb.velocity.magnitude > 7f)
                stats.TakeDamage(damage * (int)rb.velocity.magnitude);
        }
    }
}
