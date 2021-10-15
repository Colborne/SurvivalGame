using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private void Start() {
        if(FindObjectOfType<DamageCollider>())
            transform.position = FindObjectOfType<DamageCollider>().transform.position;
        else if (FindObjectOfType<Projectile>())
            transform.position = FindObjectOfType<Projectile>().transform.position;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
