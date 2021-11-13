using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWindowManager : MonoBehaviour
{
    public int iter;
    public GameObject window;
    public ChestManager cm;

    private void Awake() {
        cm = FindObjectOfType<ChestManager>();
        iter = cm.chests.Count;
        window = Instantiate(cm.chestWindow);
        window.transform.SetParent(GameObject.Find("Player UI").transform, false);
        cm.chests.Add(window);
        window.SetActive(false);     
    }
}