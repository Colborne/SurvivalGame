using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Activate", 10f);
    }

    void Activate()
    {
        objectToActivate.SetActive(true);
    }
}
