using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAnimator : MonoBehaviour
{
    public SkinnedMeshRenderer TargetMeshRenderer;
    PlayerManager playerManager;
    public bool isChest;
    private void Awake() 
    {
        playerManager = FindObjectOfType<PlayerManager>();

        if(isChest)
            playerManager.chestModel.SetActive(true);
        else
            playerManager.legsModel.SetActive(true);

        if(isChest)
            TargetMeshRenderer = GameObject.Find("Player/Player Torso").GetComponent<SkinnedMeshRenderer>();
        else
            TargetMeshRenderer = GameObject.Find("Player/Player Legs").GetComponent<SkinnedMeshRenderer>();     
    }

    void Start()
    {
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in TargetMeshRenderer.bones)
            boneMap[bone.gameObject.name] = bone;
 
        SkinnedMeshRenderer myRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
 
        Transform[] newBones = new Transform[myRenderer.bones.Length];
        for (int i = 0; i < myRenderer.bones.Length; ++i)
        {
            GameObject bone = myRenderer.bones[i].gameObject;
            if (!boneMap.TryGetValue(bone.name, out newBones[i]))
            {
                Debug.Log("Unable to map bone \"" + bone.name + "\" to target skeleton.");
                break;
            }
        }
        myRenderer.bones = newBones;
    }
}
