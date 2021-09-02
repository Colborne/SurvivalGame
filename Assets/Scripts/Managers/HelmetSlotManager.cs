using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetSlotManager : MonoBehaviour
{
    public HelmetHolderSlot helmetSlot;
    DamageCollider helmetDamageCollider;
    StatsManager stats;
    Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        stats = GetComponentInParent<StatsManager>();
        helmetSlot = GetComponentInChildren<HelmetHolderSlot>();
    }

    public void LoadHelmetOnSlot(HelmetItem helmetItem)
    {
        helmetSlot.LoadHelmetModel(helmetItem);
    }
}