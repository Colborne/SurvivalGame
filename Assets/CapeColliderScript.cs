using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeColliderScript : MonoBehaviour
{
    void Start()
    {   
        CapsuleCollider[] col = new CapsuleCollider[1];
        col[0] =  GameObject.Find("Player/Armature/Pelvis/Spine/CapeCollider").GetComponent<CapsuleCollider>();
        GetComponent<Cloth>().capsuleColliders = col;
    }
}
